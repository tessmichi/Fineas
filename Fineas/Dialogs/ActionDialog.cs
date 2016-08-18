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
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;

    // TODO: store default responses somewhere that's called only once

    [Serializable]
    public class ActionDialog : IDialog<string>
    {
        private string timeframeChoice = string.Empty;
        private string dataTypeChoice = string.Empty;
        private string dataItemChoice = string.Empty;

        List<FinanceItem> currentItems = new List<FinanceItem>();

        // TODO: fix code to not need this globally
        //[NonSerialized]
        //private Activity activityMan;

        public const string INSTRUCTIONS = "Try one of my commands: help, logout, login, who, query, refresh.";

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

                if (string.Equals(message.Text, "help", StringComparison.OrdinalIgnoreCase))
                {
                    // Give list of options
                    // Post response and end this dialog
                    await context.PostAsync(string.Format("Ask me a question about your budget, and I will do my best to give you the answer you need. {0}", INSTRUCTIONS));
                    context.Wait(MessageReceivedAsync);
                }
                else if (string.Equals(message.Text, "logout", StringComparison.OrdinalIgnoreCase))
                {
                    // Make sure nothing is "cached"
                    DataRetriever.DeleteAllData();

                    // Logout and end this dialog
                    await context.Logout();
                    context.Wait(MessageReceivedAsync);
                }
                else if (string.Equals(message.Text, "login", StringComparison.OrdinalIgnoreCase))
                {
                    // Check to see if user token is filled, meaning they're logged in
                    string userToken = await context.GetAccessToken(AuthSettings.Scopes);
                    if (string.IsNullOrEmpty(userToken))
                    {
                        // Take user to login thread
                        await context.Forward(new AzureAuthDialog(AuthSettings.Scopes), ResumeAfterAuth, message, CancellationToken.None);
                    }
                    else
                    {
                        // Can't login if already logged in
                        // Post response and end this dialog
                        await context.PostAsync("You are already logged in.");
                        context.Wait(MessageReceivedAsync);
                    }
                }
                else if (string.Equals(message.Text, "who", StringComparison.OrdinalIgnoreCase))
                {
                    // context is handled inside this method
                    await TryGiveUsername(context, message.Text);
                }
                else if (string.Equals(message.Text, "query", StringComparison.OrdinalIgnoreCase))
                {
                    // context is handled inside this method
                    await TryQueryDatabase(context, message);
                    //GetSwitch(context, message);
                }
                else if (string.Equals(message.Text, "refresh", StringComparison.OrdinalIgnoreCase))
                {
                    await Refresh(context, message.ToString(), true);

                    // End this dialog
                    context.Wait(MessageReceivedAsync);
                }
                else if (IsPreviousButton(message.Text))
                {
                    // That request is out of date, try starting a new query
                    await context.PostAsync(string.Format("Sorry, that question is outdated. Try querying again.", INSTRUCTIONS));
                    context.Wait(MessageReceivedAsync);
                }
                else
                {
                    await PrintCards(context, (Activity)message);

                    // TODO: LUIS GOES HERE

                    //Activity activity = (Activity)message;

                    //IConversationUpdateActivity update = activity;
                    //using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
                    //{
                    //    var client = scope.Resolve<IConnectorClient>();
                    //    await client.Conversations.ReplyToActivityAsync(GetSwitch(context));
                    //}

                    // Tell user we don't know what to do here, and end this dialog
                    await context.PostAsync(string.Format("Sorry, '{0}' is not a choice. {1}", message.Text, INSTRUCTIONS));
                    context.Wait(MessageReceivedAsync);
                }
            }
            catch (Exception e)
            {
                // Tell user something broke, and end this dialog
                await context.PostAsync(string.Format("My life is in shambles. Something crashed. Hint: {0}", e.Message));
                context.Wait(MessageReceivedAsync);
            }
        }

        private void GetSwitch(IDialogContext context, IMessageActivity message)
        {
            var toBot = from mess in Chain.PostToChain() select mess.Text;

            var logic =
                toBot
                .Switch
                (
                    new RegexCase<string>(new Regex("^hello"), (cont, text) =>
                    {
                        return "world!";
                    }),
                    new Case<string, string>((txt) => txt == "world", (cont, text) =>
                    {
                        return "!";
                    }),
                    new DefaultCase<string, string>((cont, text) =>
                    {
                        return text;
                    }
                )
            );

            var toUser = logic.PostToUser();

            context.Call(toUser, ResumeNothing);
        }

        private async Task ResumeNothing(IDialogContext context, IAwaitable<string> result)
        {
            await context.PostAsync("Coolio");
            context.Wait(MessageReceivedAsync);
        }

        private bool IsPreviousButton(string text)
        {
            // This should check the response as existing in every possible list of responses.

            return (new List<string>(DataRetriever.TimeframeOptions).Contains(text)
                || new List<string>(DataRetriever.DataTypeOptions).Contains(text)
                || DataRetriever.DataItemOptions.Contains(text));
        }

        private async Task Refresh(IDialogContext context, string message, bool alertWhenDone = false)
        {
            // Warn user there will be some lag
            await context.PostAsync("Hold on, refreshing against the database.");

            // Check current user and refresh data based off their credentials
            User user = await GetUser(context, message);
            DataRetriever.GetAllData(user.alias);

            // Post response
            if (alertWhenDone)
            {
                await context.PostAsync("Ok, done refreshing!");
            }
        }

        private async Task TryGiveUsername(IDialogContext context, string message)
        {
            // Get current user
            User user = await GetUser(context, message);
            
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
        
        private async Task TryQueryDatabase(IDialogContext context, IMessageActivity message)
        {
            // Check if the user is from @microsoft.com or else kick their butt
            User user = await GetUser(context, message.Text);
            //activityMan = (Activity)message;
            if (user.VerifyUser())
            {
                await EnsureHaveData(context, message.Text);

                // If haven't refreshed in the last x minutes, then ask for verification
                if (DataRetriever.LastRefresh < DateTime.Now.AddMinutes(DataRetriever.AMT_MIN_CHECK_REFRESH))
                {
                    PromptDialog.Confirm(context, ResumeIfVerifyQuery, string.Format("The last time this data was refreshed was {0}. Is that ok?", DataRetriever.LastRefresh.ToShortTimeString()), "Let's try that again.", 2, PromptStyle.Auto);
                }
                else
                {
                    // If we have refreshed in the last x minutes, then go right to the questions
                    PromptDialog.Choice(context, ResumeAfterItemChoice, DataRetriever.DataItemOptions, "What expense category do you want?", "Let's try that again.", 2, PromptStyle.Auto);
                }

                //await PrintReceipt(context, (Activity)message);
            }
            else
            {
                // ..kick their butt
                // Post response and end this dialog
                await context.PostAsync("You shouldn't be in here.");
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task EnsureHaveData(IDialogContext context, string message)
        {
            // Make sure we do have data cached
            if (DataRetriever.TimeframeOptions.Length == 0)
            {
                await Refresh(context, message.ToString());
            }
        }

        private async Task ResumeAfterAuth(IDialogContext context, IAwaitable<string> result)
        {
            var message = await result;

            // Get current user and refresh data based on their credentials
            User user = await GetUser(context, message);
            DataRetriever.GetAllData(user.alias);

            // Tell user they are authenticated
            // Post response and end this dialog
            await context.PostAsync(message);
            await context.PostAsync("If you want me to log you off, just say \"logout\".");
            context.Wait(MessageReceivedAsync);
        }
        
        private async Task<User> GetUser(IDialogContext context, string message, bool login = false)
        {
            // TODO: check context and see if the token matches the one from the message

            // Check to see if there is a user logged in
            string userToken = await context.GetAccessToken(AuthSettings.Scopes);

            // If no user, force a login (only when specified)
            if (login)
                while (string.IsNullOrEmpty(userToken))
                {
                    await context.Forward(new AzureAuthDialog(AuthSettings.Scopes), ResumeAfterAuth, message, CancellationToken.None);
                    userToken = await context.GetAccessToken(AuthSettings.Scopes);
                }

            // Create user object from the token given on login
            return new User(userToken);
        }

        #region Query Chain

        private async Task ResumeIfVerifyQuery(IDialogContext context, IAwaitable<bool> result)
        {
            bool message = await result;

            if (!message)
            {
                await Refresh(context, message.ToString());
            }

            await EnsureHaveData(context, timeframeChoice);

            // Start dialog to get user's filters for query
            PromptDialog.Choice(context, ResumeAfterItemChoice, DataRetriever.DataItemOptions, "What expense category do you want?", "Let's try that again.", 2, PromptStyle.Auto);
        }

        private async Task ResumeAfterItemChoice(IDialogContext context, IAwaitable<string> result)
        {
            // Given data item choice, get time choice
            dataItemChoice = await result;
            
            await EnsureHaveData(context, timeframeChoice);

            PromptDialog.Choice(context, ResumeAfterTimeChoice, new List<string>(DataRetriever.TimeframeOptions), "What timeframe do you want?", "Let's try that again.", 2, PromptStyle.Auto);
        }

        private async Task ResumeAfterTimeChoice(IDialogContext context, IAwaitable<string> result)
        {
            // Given timeframe choice, get data type choice
            timeframeChoice = await result;

            await EnsureHaveData(context, timeframeChoice);

            await RunQuery(context, dataItemChoice);
        }

        private async Task RunQuery(IDialogContext context, string message)
        {
            // Verify current user
            User user = await GetUser(context, message);

            // Run lync on 'cached' data (stored in DataRetriever)
            currentItems = DataRetriever.QueryFromData(timeframeChoice, dataItemChoice, user.alias, DateTime.Now.AddMonths(-2));

            await PrintText(context);
        }

        private async Task PrintText(IDialogContext context)
        {
            // Print out all the returned data objects
            StringBuilder str = new StringBuilder();

            if (currentItems.Count == 0)
            {
                str.Append(string.Format("There doesn't seem to be any data here for {0}!", dataItemChoice));
            }
            else foreach (FinanceItem item in currentItems)
            {
                str.Append(string.Format("{0}\r\n", item.ToString()));
            }

            // Post response and end this dialog
            await context.PostAsync(str.ToString());
            context.Wait(MessageReceivedAsync);
        }

        private async Task PrintCards(IDialogContext context, Activity message)
        {
            if (currentItems.Count == 0)
            {
                await context.PostAsync(string.Format("There doesn't seem to be any data here for {0}!", dataItemChoice));
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                Activity replyToConversation = message.CreateReply("I found the following data");
                replyToConversation.Recipient = message.From;
                replyToConversation.Type = "message";
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                replyToConversation.Attachments = new List<Attachment>();
                
                foreach (FinanceItem item in currentItems)
                {
                    List<CardImage> cardImages = new List<CardImage>();
                    List<CardAction> cardButtons = new List<CardAction>();
                    HeroCard plCard = new HeroCard()
                    {
                        Title = item.ShortString(),
                        Subtitle = item.ToString(),
                        Images = cardImages,
                        Buttons = cardButtons
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    replyToConversation.Attachments.Add(plAttachment);
                }
                replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                
                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
                {
                    var client = scope.Resolve<IConnectorClient>();
                    var reply = await client.Conversations.SendToConversationAsync(replyToConversation);
                }
            }
        }

        private async Task PrintReceipt(IDialogContext context, Activity message)
        {
            if (currentItems.Count == 0)
            {
                await context.PostAsync(string.Format("There doesn't seem to be any data here for {0}!", dataItemChoice));
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                Activity replyToConversation = message.CreateReply("I found the following data");
                replyToConversation.Recipient = message.From;
                replyToConversation.Type = "message";
                replyToConversation.Attachments = new List<Attachment>();
                List<CardImage> cardImages = new List<CardImage>();
                List<CardAction> cardButtons = new List<CardAction>();
                List<ReceiptItem> receiptList = new List<ReceiptItem>();

                foreach (FinanceItem item in currentItems)
                {
                    ReceiptItem lineItem = new ReceiptItem()
                    {
                        Title = item.ShortString(),
                        Subtitle = item.ToString(),
                        Text = "",
                        Image = null,
                        Price = "",
                        Quantity = "",
                        Tap = null
                    };
                    receiptList.Add(lineItem);
                }

                ReceiptCard plCard = new ReceiptCard()
                {
                    Title = "Data you requested",
                    Buttons = cardButtons,
                    Items = receiptList,
                    Total = string.Empty,
                    Tax = string.Empty
                };
                Attachment plAttachment = plCard.ToAttachment();
                replyToConversation.Attachments.Add(plAttachment);

                using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
                {
                    var client = scope.Resolve<IConnectorClient>();
                    var reply = await client.Conversations.SendToConversationAsync(replyToConversation);
                }
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
