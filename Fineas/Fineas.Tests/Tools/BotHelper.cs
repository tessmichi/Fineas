﻿//// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

//// I got this from AzureBot, author: Thiago Almeida
//// https://github.com/Microsoft/AzureBot/tree/master/AzureBot.Tests

//namespace Fineas.Tests.TestingTools
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using Microsoft.Bot.Connector.DirectLine;
//    using Microsoft.Bot.Connector.DirectLine.Models;

//    public class BotHelper : IDisposable
//    {
//        private string watermark;
//        private string microsoftAppId;
//        private string fromUser;
//        private string botId;
//        private DirectLineClient directLineClient;
//        private Conversation conversation;

//        private bool disposed = false;

//        public BotHelper(string directLineToken, string microsoftAppId, string fromUser, string BotId)
//        {
//            this.microsoftAppId = microsoftAppId;
//            this.fromUser = fromUser;
//            this.botId = BotId;
//            this.directLineClient = new DirectLineClient(directLineToken);
//            this.conversation = this.directLineClient.Conversations.NewConversation();
//        }

//        public async Task<string> SendMessage(string msg)
//        {
//            await this.SendMessageNoReply(msg);
//            return await this.LastMessageFromBot();
//        }

//        public async Task SendMessageNoReply(string msg)
//        {
//            // Passing in a value in FromProperty makes the bot 'remember' that it's the same user
//            // and loads the user context that will have been set up previously outside the tests
//            Message message = new Message { FromProperty = this.fromUser, Text = msg };
//            await this.directLineClient.Conversations.PostMessageAsync(this.conversation.ConversationId, message);
//        }

//        public async Task<string> LastMessageFromBot()
//        {
//            var botMessages = await this.AllBotMessagesSinceWatermark();
//            return botMessages.Last();
//        }

//        public async Task WaitForLongRunningOperations(Action<IList<string>> resultHandler, int operationsToWait, int delayBetweenPoolingInSeconds = 4)
//        {
//            var currentWatermark = this.watermark;
//            var messages = await this.AllBotMessagesSinceWatermark(currentWatermark).ConfigureAwait(false);
//            var iterations = 0;
//            var maxIterations = (5 * 60) / delayBetweenPoolingInSeconds;

//            while (iterations < maxIterations && messages.Count < operationsToWait)
//            {
//                await Task.Delay(TimeSpan.FromSeconds(delayBetweenPoolingInSeconds)).ConfigureAwait(false);
//                messages = await this.AllBotMessagesSinceWatermark(currentWatermark);
//                iterations++;
//            }

//            resultHandler(messages);
//        }

//        public void Dispose()
//        {
//            this.Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (this.disposed)
//            {
//                return;
//            }

//            if (disposing)
//            {
//                this.directLineClient.Dispose();
//            }

//            this.disposed = true;
//        }

//        private async Task<IList<string>> AllBotMessagesSinceWatermark(string specificWatermark = null)
//        {
//            var messages = await this.AllMessagesSinceWatermark(specificWatermark);
//            var messagesText = from x in messages
//                               where x.FromProperty == this.botId
//                               select x.Text.Trim();
//            return messagesText.ToList();
//        }

//        private async Task<IList<Message>> AllMessagesSinceWatermark(string specificWatermark = null)
//        {
//            specificWatermark = string.IsNullOrEmpty(specificWatermark) ? this.watermark : specificWatermark;
//            MessageSet messageSet = await this.directLineClient.Conversations.GetMessagesAsync(this.conversation.ConversationId, specificWatermark);
//            this.watermark = messageSet?.Watermark;
//            return messageSet.Messages;
//        }
//    }
//}


///*

//MIT License

//Copyright (c) 2016 Tess

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

//*/
