// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Dialogs
{
    using AuthBot;
    using AuthBot.Dialogs;
    using AuthBot.Models;
    using Autofac;
    using Controllers;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Dialogs.Internals;
    using Microsoft.Bot.Connector;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    // TODO: store default responses somewhere that's called only once

    [Serializable]
    public class ActionDialog : IDialog<string>
    {
        private string dataItemChoice = string.Empty;
        private DateTime timeRange = DateTime.Now;

        private List<FinanceItem> currentItems = new List<FinanceItem>();
        
        public const string INSTRUCTIONS = "Try one of my commands: help, logout, login, who, query, refresh. Or try something else!";
        private const int MAX_CARDS_CAROUSEL = 4;

        private const string EXPENSE_ENTITY = "ExpenseCategory";
        private const string TIME_ENTITY = "TimePeriod";


        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> msg)
        {
            try
            {
                IMessageActivity message = await msg;
                string tag = "@Fineas";
                message.Text = message.Text.IndexOf(tag) >= 0 ?
                    message.Text.Remove(message.Text.IndexOf(tag), tag.Length).Trim() :
                    message.Text.Trim();

                var luisResponse = await LuisHelper.ParseUserInput(message.Text);
                var bestIntent = luisResponse.Intents.Aggregate((currMax, x) => (currMax == null || x.Score > currMax.Score ? x : currMax));

                switch(bestIntent.Intent)
                {
                    case "Refresh":
                        // Refresh cached data
                        await RefreshAsync(context, true);

                        // End this dialog
                        context.Wait(MessageReceivedAsync);
                        break;

                    case "Help":
                        // Give list of options
                        // Post response and end this dialog
                        await context.PostAsync(string.Format("Ask me a question about your budget, and I will do my best to give you the answer you need. {0}", INSTRUCTIONS));
                        context.Wait(MessageReceivedAsync);
                        break;

                    case "Log In":
                        // Check to see if user token is filled, meaning they're logged in
                        string userToken = await context.GetAccessToken(AuthSettings.Scopes);
                        if (string.IsNullOrEmpty(userToken))
                        {
                            // Take user to login thread
                            await context.Forward(new AzureAuthDialog(AuthSettings.Scopes), ResumeAfterAuthAsync, message, CancellationToken.None);
                        }
                        else
                        {
                            // Can't login if already logged in
                            // Post response and end this dialog
                            await context.PostAsync("You are already logged in.");
                            context.Wait(MessageReceivedAsync);
                        }
                        break;

                    case "Log Out":
                        // Make sure nothing is "cached"
                        DataRetriever.DeleteAllData();

                        // Logout and end this dialog
                        await context.Logout();
                        context.Wait(MessageReceivedAsync);
                        break;

                    case "Who":
                        // context is handled inside this method
                        await TryGiveUsernameAsync(context, message.Text);
                        break;

                    case "Query":
                        // Extract entities and store in context
                        LuisEntity expenseEntity = luisResponse.Entities.FirstOrDefault((e) => e.Type == EXPENSE_ENTITY);
                        LuisEntity timeEntity = luisResponse.Entities.FirstOrDefault((e) => e.Type == TIME_ENTITY);
                        if (expenseEntity != null)
                        {
                            context.PrivateConversationData.SetValue<string>(EXPENSE_ENTITY, expenseEntity.Entity);
                        }
                        if (timeEntity != null)
                        {
                            context.PrivateConversationData.SetValue<string>(TIME_ENTITY, timeEntity.Entity);
                        }

                        timeRange = DateTime.Parse("June, 2016");

                        // context is handled inside this method
                        await TryQueryDatabaseAsync(context, message);
                        break;

                    case "None":
                        if(IsPreviousButton(message.Text))
                        {
                            // In this scenario, the user has selected a button they are not currently in the dialog for
                            await context.PostAsync(string.Format("Sorry, that question is outdated. Try querying again.", INSTRUCTIONS));
                            context.Wait(MessageReceivedAsync);
                        }
                        else
                        {
                            // Tell user we don't know what to do here, and end this dialog
                            await context.PostAsync(string.Format("Sorry, '{0}' is not a choice. {1}", message.Text, INSTRUCTIONS));
                            context.Wait(MessageReceivedAsync);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                // Tell user something broke, and end this dialog
                await context.PostAsync(string.Format("My life is in shambles. Something crashed. Hint: {0}", e.Message));
                context.Wait(MessageReceivedAsync);
            }
        }
        
        private bool IsPreviousButton(string text)
        {
            // This should check the response as existing in every possible list of responses.
            // TODO: i think at some states, these might not be filled in and so we get false negatives.
            // need a way to handle this in the message sent to the user.

            return (new List<string>(DataRetriever.TimeframeOptions).Contains(text)
                || new List<string>(DataRetriever.DataTypeOptions).Contains(text)
                || DataRetriever.LineItemDescriptions.ContainsKey(text));
        }

        private async Task RefreshAsync(IDialogContext context, bool alertWhenDone = false)
        {
            // Warn user there will be some lag
            await context.PostAsync("Hold on, refreshing against the database.");

            // Check current user and refresh data based off their credentials
            User user = await GetUserAsync(context);
            DataRetriever.GetAllData(user.alias);

            // Post response
            if (alertWhenDone)
            {
                await context.PostAsync("Ok, done refreshing!");
            }
        }

        private async Task TryGiveUsernameAsync(IDialogContext context, string message)
        {
            // Get current user
            User user = await GetUserAsync(context, message);
            
            // Make response saying who user is
            string res = string.IsNullOrEmpty(user.token) ?
                "We don't know who you are, try logging in!" :
                    string.IsNullOrEmpty(user.upn) ?
                    "You're logged in as an account type I don't recognize. Try another one!" :
                    string.Format("You are logged in as {0}, {1}", user.upn.ToLower(), user.given_name);
            
            // Post response and end this dialog
            await context.PostAsync(res);
            context.Wait(MessageReceivedAsync);
        }
        
        private async Task TryQueryDatabaseAsync(IDialogContext context, IMessageActivity message)
        {
            // Check if the user is from @microsoft.com or else kick their butt
            User user = await GetUserAsync(context, message.Text);
            if (user.VerifyUser())
            {
                if (await EnsureHaveDataAsync(context))
                {
                    // If haven't refreshed in the last x minutes, then ask for verification
                    if (DataRetriever.LastRefresh < DateTime.Now.AddMinutes(DataRetriever.AMT_MIN_CHECK_REFRESH))
                    {
                        PromptDialog.Confirm(context, ResumeIfVerifyRefresh, string.Format("The last time this data was refreshed was {0}. Use old data?", DataRetriever.LastRefresh.ToShortTimeString()), "Let's try that again.", 2, PromptStyle.Auto);
                    }
                    else
                    {
                        // If we have refreshed in the last x minutes, then go right to the questions
                        await GetLineItemChoiceAsync(context);

                        //CallQueryDialog(context);
                    }
                }
                else
                    context.Wait(MessageReceivedAsync);
            }
            else
            {
                // ..kick their butt
                // Post response and end this dialog
                // TODO: add funny image here
                await context.PostAsync("You shouldn't be in here.");
                context.Wait(MessageReceivedAsync);
            }
        }

        private void CallQueryDialog(IDialogContext context)
        {
            string expenseCategory = null;
            string timePeriod = null;
            context.PrivateConversationData.TryGetValue<string>(EXPENSE_ENTITY, out expenseCategory);
            context.PrivateConversationData.TryGetValue<string>(TIME_ENTITY, out timePeriod);
            context.Call(QueryDialog.BuildDialog(expenseCategory, timePeriod), RunQuery);
        }

        private async Task<bool> EnsureHaveDataAsync(IDialogContext context)
        {
            // Make sure we do have data cached
            if (!DataRetriever.HaveData())
            {
                await context.PostAsync("Looks like my data is gone.");
                await RefreshAsync(context);
            }

            if (!DataRetriever.HaveData())
            {
                await context.PostAsync("Couldn't find data...Please try again later.");
            }

            return DataRetriever.HaveData();
        }

        private async Task ResumeAfterAuthAsync(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result;

            // Get current user and refresh data based on their credentials
            User user = await GetUserAsync(context, message);
            DataRetriever.GetAllData(user.alias);

            // Tell user they are authenticated
            // Post response and end this dialog
            await context.PostAsync(message);
            await context.PostAsync("If you want me to log you off, just say \"logout\".");
            context.Wait(MessageReceivedAsync);
        }
        
        private async Task<User> GetUserAsync(IDialogContext context, string message = "", bool login = false)
        {
            // TODO: check context and see if the token matches the one from the message

            // Check to see if there is a user logged in
            string userToken = await context.GetAccessToken(AuthSettings.Scopes);

            // If no user, force a login (only when specified)
            if (login)
                while (string.IsNullOrEmpty(userToken))
                {
                    await context.Forward(new AzureAuthDialog(AuthSettings.Scopes), ResumeAfterAuthAsync, message, CancellationToken.None);
                    userToken = await context.GetAccessToken(AuthSettings.Scopes);
                }

            // Create user object from the token given on login
            return new User(userToken);
        }

        #region Query Chain

        private async Task ResumeIfVerifyRefresh(IDialogContext context, IAwaitable<bool> result)
        {
            // Get response on whether or not user is ok with old data
            bool message = await result;

            // If they are not ok with old data, refresh it
            if (!message)
            {
                await RefreshAsync(context);
            }

            // Make sure data is filled
            if (await EnsureHaveDataAsync(context))
                // Start dialog to get user's filters for query
                await GetLineItemChoiceAsync(context);
                //CallQueryDialog(context);
            else
                context.Wait(MessageReceivedAsync);
        }

        private async Task GetLineItemChoiceAsync(IDialogContext context)
        {
            for (int i = 0; i < DataRetriever.LineItemDescriptions.Keys.Count; i+=4)
            {
                List<string> keys = new List<string>(DataRetriever.LineItemDescriptions.Keys)
                    .GetRange(i, Math.Min(MAX_CARDS_CAROUSEL, DataRetriever.LineItemDescriptions.Keys.Count-i));

                // Make a new message since we cannot access the original message sent from the user
                Activity message = (Activity)context.MakeMessage();

                // Design this message as a reply to the user
                Activity replyToConversation = message.CreateReply(
                    i == 0 ?
                    "Which expense category do you want to use?" :
                    "I found some more options you can choose from!");
                replyToConversation.Recipient = message.From;
                replyToConversation.Type = "message";

                // This will have a list of information which we want to display as a carousel of hero cards
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                replyToConversation.Attachments = new List<Attachment>();

                // For each line item and each line item description, print a hero card
                foreach (string key in keys)
                {
                    //await context.PostAsync($"Doing key = {key}");
                    List<CardImage> cardImages = new List<CardImage>();
                    List<CardAction> cardButtons = new List<CardAction>();

                    // Add the button
                    CardAction button = new CardAction()
                    {
                        Type = "imBack",
                        Title = "Choose",
                        Value = key
                    };
                    cardButtons.Add(button);

                    HeroCard plCard = new HeroCard()
                    {
                        Title = key,
                        Subtitle = DataRetriever.LineItemDescriptions[key],
                        Images = cardImages,
                        Buttons = cardButtons
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);
                }
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                // Post options to user so they can select one
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
                {
                    var client = scope.Resolve<IConnectorClient>();
                    var reply = await client.Conversations.SendToConversationAsync(replyToConversation);
                }
            }
            
            // Read response from user
            PromptDialog.Text(context, GetLineItemChoice, "Please choose one");
        }

        private async Task GetLineItemChoice(IDialogContext context, IAwaitable<string> result)
        {
            // Given timeframe choice, get data type choice
            string choice = await result;

            if (!DataRetriever.LineItemDescriptions.ContainsKey(choice))
            {
                await context.PostAsync(string.Format("Sorry, '{0}' makes no sense to me right now. Let's start from scratch! {1}", choice, INSTRUCTIONS));
                context.Wait(MessageReceivedAsync);

                return;
            }

            // Save the option the user selected
            dataItemChoice = choice;

            // Now ask which timeframe choice they want
            await GetTimeFrameChoice(context);
        }

        private async Task GetTimeFrameChoice(IDialogContext context)
        {
            if (await EnsureHaveDataAsync(context))
                PromptDialog.Choice(context, ResumeAfterTimeChoice, new List<string>(DataRetriever.TimeframeOptions), "What timeframe do you want?", "Let's try that again.", 2, PromptStyle.Auto);
            else
                context.Wait(MessageReceivedAsync);
        }

        private async Task ResumeAfterTimeChoice(IDialogContext context, IAwaitable<string> result)
        {
            // Given timeframe choice, get data type choice
            string timeframeChoice = await result;

            if (await EnsureHaveDataAsync(context))
                await RunQuery(context, timeframeChoice);
            else
                context.Wait(MessageReceivedAsync);
        }

        private async Task RunQuery(IDialogContext context, string timeframeChoice)
        {
            // Verify current user
            User user = await GetUserAsync(context);

            // Run lync on 'cached' data (stored in DataRetriever)
            currentItems = DataRetriever.QueryFromData(timeframeChoice, dataItemChoice, user.alias, timeRange);

            await PrintCards(context, dataItemChoice);
        }

        private async Task RunQuery(IDialogContext context, IAwaitable<QueryForm> formAwaitable)
        {
            var formState = await formAwaitable;

            // Verify current user
            User user = await GetUserAsync(context);

            if (await EnsureHaveDataAsync(context))
            {
                // Run lync on 'cached' data (stored in DataRetriever)
                currentItems = DataRetriever.QueryFromData(formState.TimePeriod, formState.ExpenseCategory, user.alias, timeRange);

                await PrintCards(context, formState.ExpenseCategory);

                // Clear databag data
                context.PrivateConversationData.RemoveValue(EXPENSE_ENTITY);
                context.PrivateConversationData.RemoveValue(TIME_ENTITY);
            }
            else
                context.Wait(MessageReceivedAsync);
        }
        
        private async Task PrintCards(IDialogContext context, string lineItem)
        {
            Activity message = (Activity)context.MakeMessage();

            if (currentItems.Count == 0)
            {
                await context.PostAsync(string.Format("There doesn't seem to be any data here for {0} within {1}!", lineItem, timeRange.ToShortDateString()));
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                Activity replyToConversation = message.CreateReply(
                    $"I found the following data, last refreshed {DataRetriever.LastRefresh.ToString("hh:mm MM/dd/yyyy")}");
                replyToConversation.Recipient = message.From;
                replyToConversation.Type = "message";
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                replyToConversation.Attachments = new List<Attachment>();
                
                foreach (FinanceItem item in currentItems)
                {
                    foreach (KeyValuePair<string,string> summary in item.GetSummaries())
                    {
                        List<CardImage> cardImages = new List<CardImage>();
                        List<CardAction> cardButtons = new List<CardAction>();
                        HeroCard plCard = new HeroCard()
                        {
                            Title = summary.Key,
                            Subtitle = summary.Value,
                            Images = cardImages,
                            Buttons = cardButtons
                        };
                        Attachment plAttachment = plCard.ToAttachment();
                        replyToConversation.Attachments.Add(plAttachment);
                    }
                }
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;

                // Post response and end this dialog
                //await context.PostAsync(replyToConversation);
                //context.Wait(MessageReceivedAsync);
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
                {
                    var client = scope.Resolve<IConnectorClient>();
                    var reply = await client.Conversations.SendToConversationAsync(replyToConversation);
                }
                context.Wait(MessageReceivedAsync);
            }
        }

        #endregion Query Chain
    }
}

/*

MIT License

Copyright (c) 2016 Tess

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
