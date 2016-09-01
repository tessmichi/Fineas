// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Tests.Dialogs
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class ActionDialogTest
    {
        //#region MessageReceivedAsync

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncTagUsed()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncMessageNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncMessageEmpty()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncMessageVeryLong()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentRefresh()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentHelp()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentLoginLoddegIn()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentLoginNotLoggedIn()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentLogoutWithData()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentLogoutWithoutData()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentLogoutNotLoggedIn()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentWho()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentQueryNoData()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentQueryJustLineItem()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentQueryJustTime()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncIntentQueryAllData()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncPreviousButton()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsyncNothing()
        //{
        //}

        //[TestMethod]
        //public async Task TestMessageReceivedAsync()
        //{
        //}

        //#endregion MessageReceivedAsync

        //#region IsPreviousButton

        //[TestMethod]
        //public async Task TestIsPreviousButtonNo()
        //{
        //}

        //[TestMethod]
        //public async Task TestIsPreviousButtonTime()
        //{
        //}

        //[TestMethod]
        //public async Task TestIsPreviousButtonDataType()
        //{
        //}

        //[TestMethod]
        //public async Task TestIsPreviousButtonDescription()
        //{
        //}

        //#endregion IsPreviousButton

        //#region RefreshAsync

        //[TestMethod]
        //public async Task TestRefreshAsyncValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestRefreshAsyncNoDataFound()
        //{
        //}

        //[TestMethod]
        //public async Task TestRefreshAsyncInvalidUser()
        //{
        //}

        //[TestMethod]
        //public async Task TestRefreshAsyncNoUser()
        //{
        //}

        //#endregion RefreshAsync

        //#region TryGiveUsernameAsync

        //[TestMethod]
        //public async Task TestTryGiveUsernameAsyncValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryGiveUsernameAsyncUserNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryGiveUsernameAsyncUserInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryGiveUsernameAsyncMessageEmpty()
        //{
        //}

        //#endregion TryGiveUsernameAsync

        //#region TryQueryDatabaseAsync

        //[TestMethod]
        //public async Task TestTryQueryDatabaseAsyncInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryQueryDatabaseAsyncValidHaveData()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryQueryDatabaseAsyncValidNoData()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryQueryDatabaseAsyncValidRefreshYes()
        //{
        //}

        //[TestMethod]
        //public async Task TestTryQueryDatabaseAsyncValidRefreshNo()
        //{
        //}

        //#endregion TryQueryDatabaseAsync

        //#region EnsureHaveDataAsync

        //[TestMethod]
        //public async Task TestEnsureHaveDataAsyncTrue()
        //{
        //}

        //[TestMethod]
        //public async Task TestEnsureHaveDataAsyncRefreshSuccess()
        //{
        //}

        //[TestMethod]
        //public async Task TestEnsureHaveDataAsyncRefreshFailure()
        //{
        //}

        //#endregion EnsureHaveDataAsync

        //#region ResumeAfterAuthAsync

        //[TestMethod]
        //public async Task TestResumeAfterAuthAsyncValidUserGetData()
        //{
        //}

        //[TestMethod]
        //public async Task TestResumeAfterAuthAsyncValidUserNoData()
        //{
        //}

        //[TestMethod]
        //public async Task TestResumeAfterAuthAsyncInvalidUser()
        //{
        //}

        //[TestMethod]
        //public async Task TestResumeAfterAuthAsyncNoUser()
        //{
        //}

        //#endregion ResumeAfterAuthAsync

        //#region GetUserAsync

        //[TestMethod]
        //public async Task TestGetUserAsyncValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUserAsyncInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUserAsyncNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUserAsyncCantParseToken()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUserAsyncForcedLoginValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUserAsyncForcedLoginInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUserAsyncForcedLoginNull()
        //{
        //}

        //#endregion GetUserAsync

        //#region ResumeIfVerifyRefresh

        //[TestMethod]
        //public async Task TestResumeIfVerifyRefreshChooseRefreshSuccess()
        //{
        //}

        //[TestMethod]
        //public async Task TestResumeIfVerifyRefreshChooseRefreshFailure()
        //{
        //}

        //[TestMethod]
        //public async Task TestResumeIfVerifyRefreshNoRefreshSuccess()
        //{
        //}

        //[TestMethod]
        //public async Task TestResumeIfVerifyRefreshNoRefreshFailure()
        //{
        //}

        //#endregion ResumeIfVerifyRefresh

        //#region GetUnspecifiedResponsesAsync

        //[TestMethod]
        //public async Task TestGetUnspecifiedResponsesAsyncLineItemSet()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUnspecifiedResponsesAsyncTimeSet()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUnspecifiedResponsesAsyncBothSet()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUnspecifiedResponsesAsyncNeitherSet()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUnspecifiedResponsesAsyncNoData()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetUnspecifiedResponsesAsyncNoLogin()
        //{
        //}

        //#endregion GetUnspecifiedResponsesAsync

        //#region SaveLineItemChoice

        //[TestMethod]
        //public async Task TestSaveLineItemChoiceInvalidKey()
        //{
        //}

        //[TestMethod]
        //public async Task TestSaveLineItemChoiceHaveData()
        //{
        //}

        //[TestMethod]
        //public async Task TestSaveLineItemChoiceNoData()
        //{
        //}

        //#endregion SaveLineItemChoice

        //#region SaveTimeChoice

        //[TestMethod]
        //public async Task TestSaveTimeChoiceInvalidOption()
        //{
        //}

        //[TestMethod]
        //public async Task TestSaveTimeChoiceHaveData()
        //{
        //}

        //[TestMethod]
        //public async Task TestSaveTimeChoiceNoData()
        //{
        //}

        //#endregion SaveTimeChoice

        //#region RunQuery

        //[TestMethod]
        //public async Task TestRunQueryInvalidUser()
        //{
        //}

        //[TestMethod]
        //public async Task TestRunQueryValidAll()
        //{
        //}

        //[TestMethod]
        //public async Task TestRunQueryNoOptionsSaved()
        //{
        //}

        //[TestMethod]
        //public async Task TestRunQueryGotData()
        //{
        //}

        //[TestMethod]
        //public async Task TestRunQueryNoData()
        //{
        //}

        //#endregion RunQuery

        //#region PrintCards

        //[TestMethod]
        //public async Task TestPrintCardsNoItems()
        //{
        //}

        //[TestMethod]
        //public async Task TestPrintCardsItemMissingSummary()
        //{
        //}

        //[TestMethod]
        //public async Task TestPrintCardsAllValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestPrintCardsItemsModFour0()
        //{
        //}

        //[TestMethod]
        //public async Task TestPrintCardsItemsModFour1()
        //{
        //}

        //[TestMethod]
        //public async Task TestPrintCardsItemsModFour2()
        //{
        //}

        //[TestMethod]
        //public async Task TestPrintCardsItemsModFour3()
        //{
        //}

        //#endregion PrintCards
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
