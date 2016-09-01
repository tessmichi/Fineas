//// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

//// I got this from AzureBot, author: Thiago Almeida
//// https://github.com/Microsoft/AzureBot/tree/master/AzureBot.Tests

//namespace Fineas.Tests.TestingTools
//{
//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    public static class ContextExtensions
//    {
//        public static string GetSubscription(this TestContext context)
//        {
//            return context.Properties["Subscription"].ToString();
//        }

//        public static string GetVirtualMachine(this TestContext context)
//        {
//            return context.Properties["VirtualMachine"].ToString();
//        }

//        public static string GetResourceGroup(this TestContext context)
//        {
//            return context.Properties["ResourceGroup"].ToString();
//        }

//        public static string GetRunbookWithDescription(this TestContext context)
//        {
//            return context.Properties["RunbookWithDescription"].ToString();
//        }

//        public static string GetRunbookDescription(this TestContext context)
//        {
//            return context.Properties["RunbookDescription"].ToString();
//        }

//        public static string GetRunbookWithoutDescription(this TestContext context)
//        {
//            return context.Properties["RunbookWithoutDescription"].ToString();
//        }

//        public static string GetRunbookInMultipleAutomationAccounts(this TestContext context)
//        {
//            return context.Properties["RunbookInMultipleAutomationAccounts"].ToString();
//        }

//        public static string GetRunbookNotPublished(this TestContext context)
//        {
//            return context.Properties["RunbookNotPublished"].ToString();
//        }

//        public static string GetRunbookThatFails(this TestContext context)
//        {
//            return context.Properties["RunbookThatFails"].ToString();
//        }

//        public static string GetRunbookWithParameters(this TestContext context)
//        {
//            return context.Properties["RunbookWithParameters"].ToString();
//        }

//        public static string GetJobOutput(this TestContext context)
//        {
//            return context.Properties["RunbookOutput"].ToString();
//        }

//        public static string GetAutomationAcccount(this TestContext context)
//        {
//            return context.Properties["AutomationAccount"].ToString();
//        }

//        public static string GetAlternativeSubscription(this TestContext context)
//        {
//            return context.Properties["AlternativeSubscription"].ToString();
//        }

//        public static bool DeallocateResourcesOnCleanup(this TestContext context)
//        {
//            bool deallocate = true;

//            bool.TryParse(context.Properties["DeallocateResourcesOnCleanup"].ToString(), out deallocate);

//            return deallocate;
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
