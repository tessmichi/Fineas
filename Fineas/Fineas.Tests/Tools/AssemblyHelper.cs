﻿//// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

//// I got this from AzureBot, author: Thiago Almeida
//// https://github.com/Microsoft/AzureBot/tree/master/AzureBot.Tests

//namespace Fineas.Tests.TestingTools
//{
//    using System;
//    using System.Collections.Generic;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    [TestClass]
//    public class AssemblyHelper
//    {
//        private static BotHelper botHelper;
//        public static TestContext testContext { get; set; }

//        internal static BotHelper BotHelper
//        {
//            get { return botHelper; }
//        }

//        // Will run once before all of the tests in the project. We start assuming the user is already logged in to Azure,
//        // which should  be done separately via the AzureBot.ConsoleConversation or some other means. 
//        [AssemblyInitialize]
//        public static void SetUp(TestContext context)
//        {
//            testContext = context;
//            string directLineToken = context.Properties["DirectLineToken"].ToString();
//            string microsoftAppId = context.Properties["MicrosoftAppId"].ToString();
//            string fromUser = context.Properties["FromUser"].ToString();
//            string botId = context.Properties["BotId"].ToString();

//            botHelper = new BotHelper(directLineToken, microsoftAppId, fromUser, botId);

//            var subscription = context.GetSubscription();

//            var testCase = new BotTestCase()
//            {
//                Action = $"select subscription {subscription}",
//                ExpectedReply = $"Setting {subscription} as the current subscription. What would you like to do next?",
//            };

//            TestRunner.RunTestCase(testCase).Wait();
//        }

//        // Will run after all the tests have finished
//        [AssemblyCleanup]
//        public static void CleanUp()
//        {
//            if (testContext.DeallocateResourcesOnCleanup())
//            {
//                var step1 = new BotTestCase()
//                {
//                    Action = "stop all vms",
//                    ExpectedReply = "You are trying to stop the following virtual machines",
//                };

//                var step2 = new BotTestCase()
//                {
//                    Action = "Yes",
//                    ExpectedReply = "Stopping the following virtual machines",
//                };

//                var completionTestCase = new BotTestCase()
//                {
//                    ExpectedReply = $"virtual machine was stopped successfully.",
//                };

//                var steps = new List<BotTestCase> { step1, step2 };

//                TestRunner.RunTestCases(steps, completionTestCase, 2).Wait();
//            }

//            if (botHelper != null)
//            {
//                botHelper.Dispose();
//            }
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
