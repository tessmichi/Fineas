//// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

//// I got this from AzureBot, author: Thiago Almeida
//// https://github.com/Microsoft/AzureBot/tree/master/AzureBot.Tests

//namespace Fineas.Tests.TestingTools
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading.Tasks;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    internal class TestRunner
//    {
//        internal static async Task RunTestCase(BotTestCase testCase)
//        {
//            await RunTestCases(new List<BotTestCase> { testCase }, new List<BotTestCase>());
//        }

//        internal static async Task RunTestCases(IList<BotTestCase> steps, BotTestCase completionTestCase = null, int completionChecks = 1)
//        {
//            await RunTestCases(steps, new List<BotTestCase> { completionTestCase }, completionChecks);
//        }

//        internal static async Task RunTestCases(IList<BotTestCase> steps, IList<BotTestCase> completionTestCases = null, int completionChecks = 1, bool strictCheck = true)
//        {
//            if (completionTestCases != null && completionTestCases.Count > 1 && completionTestCases.Count < completionChecks)
//            {
//                Assert.Fail($"There are completion test cases missing. Completion Test Cases: {completionTestCases.Count} for {completionChecks} completionChecks");
//            }

//            foreach (var step in steps)
//            {
//                await AssemblyHelper.BotHelper.SendMessageNoReply(step.Action);

//                Action<IList<string>> action = (replies) =>
//                {
//                    var match = replies.FirstOrDefault(stringToCheck => stringToCheck.Contains(step.ExpectedReply));
//                    Assert.IsTrue(match != null, step.ErrorMessageHandler(step.Action, step.ExpectedReply, string.Join(", ", replies)));
//                    step.Verified?.Invoke(replies.LastOrDefault());
//                };
//                await AssemblyHelper.BotHelper.WaitForLongRunningOperations(action, 1);
//            }

//            if (completionTestCases != null && completionTestCases.Any())
//            {
//                Action<IList<string>> action = (replies) =>
//                {
//                    var singleCompletionTestCase = completionTestCases.Count == 1;

//                    for (int i = 0; i < replies.Count(); i++)
//                    {
//                        if (!strictCheck && completionChecks > replies.Count())
//                        {
//                            var skip = completionChecks - replies.Count();

//                            completionTestCases = completionTestCases.Skip(skip).ToList();
//                        }

//                        var completionIndex = singleCompletionTestCase ? 0 : i;
//                        var completionTestCase = completionTestCases[completionIndex];

//                        Assert.IsTrue(
//                            replies[i].Contains(completionTestCase.ExpectedReply),
//                            completionTestCase.ErrorMessageHandler(completionTestCase.Action, completionTestCase.ExpectedReply, replies[i]));

//                        if (completionTestCase.Verified != null)
//                        {
//                            completionTestCase.Verified(replies[i]);
//                        }
//                    }
//                };

//                await AssemblyHelper.BotHelper.WaitForLongRunningOperations(action, completionChecks);
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
