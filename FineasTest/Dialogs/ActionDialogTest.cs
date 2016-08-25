using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineasTest.Dialogs
{
    [TestClass]
    class ActionDialogTest
    {
        #region MessageReceivedAsync

        [TestMethod]
        public void TestMessageReceivedAsyncTagUsed()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncMessageNull()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncMessageEmpty()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncMessageVeryLong()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentRefresh()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentHelp()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentLoginLoddegIn()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentLoginNotLoggedIn()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentLogoutWithData()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentLogoutWithoutData()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentLogoutNotLoggedIn()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentWho()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentQueryNoData()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentQueryJustLineItem()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentQueryJustTime()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncIntentQueryAllData()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncPreviousButton()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsyncNothing()
        {
        }

        [TestMethod]
        public void TestMessageReceivedAsync()
        {
        }

        #endregion MessageReceivedAsync

        #region IsPreviousButton

        [TestMethod]
        public void TestIsPreviousButtonNo()
        {
        }

        [TestMethod]
        public void TestIsPreviousButtonTime()
        {
        }

        [TestMethod]
        public void TestIsPreviousButtonDataType()
        {
        }

        [TestMethod]
        public void TestIsPreviousButtonDescription()
        {
        }

        #endregion IsPreviousButton

        #region RefreshAsync

        [TestMethod]
        public void TestRefreshAsyncValid()
        {
        }

        [TestMethod]
        public void TestRefreshAsyncNoDataFound()
        {
        }

        [TestMethod]
        public void TestRefreshAsyncInvalidUser()
        {
        }

        [TestMethod]
        public void TestRefreshAsyncNoUser()
        {
        }

        #endregion RefreshAsync

        #region TryGiveUsernameAsync

        [TestMethod]
        public void TestTryGiveUsernameAsyncValid()
        {
        }

        [TestMethod]
        public void TestTryGiveUsernameAsyncUserNull()
        {
        }

        [TestMethod]
        public void TestTryGiveUsernameAsyncUserInvalid()
        {
        }

        [TestMethod]
        public void TestTryGiveUsernameAsyncMessageEmpty()
        {
        }

        #endregion TryGiveUsernameAsync

        #region TryQueryDatabaseAsync

        [TestMethod]
        public void TestTryQueryDatabaseAsyncInvalid()
        {
        }

        [TestMethod]
        public void TestTryQueryDatabaseAsyncValidHaveData()
        {
        }

        [TestMethod]
        public void TestTryQueryDatabaseAsyncValidNoData()
        {
        }

        [TestMethod]
        public void TestTryQueryDatabaseAsyncValidRefreshYes()
        {
        }

        [TestMethod]
        public void TestTryQueryDatabaseAsyncValidRefreshNo()
        {
        }

        #endregion TryQueryDatabaseAsync

        #region EnsureHaveDataAsync

        [TestMethod]
        public void TestEnsureHaveDataAsyncTrue()
        {
        }

        [TestMethod]
        public void TestEnsureHaveDataAsyncRefreshSuccess()
        {
        }

        [TestMethod]
        public void TestEnsureHaveDataAsyncRefreshFailure()
        {
        }

        #endregion EnsureHaveDataAsync

        #region ResumeAfterAuthAsync

        [TestMethod]
        public void TestResumeAfterAuthAsyncValidUserGetData()
        {
        }

        [TestMethod]
        public void TestResumeAfterAuthAsyncValidUserNoData()
        {
        }

        [TestMethod]
        public void TestResumeAfterAuthAsyncInvalidUser()
        {
        }

        [TestMethod]
        public void TestResumeAfterAuthAsyncNoUser()
        {
        }

        #endregion ResumeAfterAuthAsync

        #region GetUserAsync

        [TestMethod]
        public void TestGetUserAsyncValid()
        {
        }

        [TestMethod]
        public void TestGetUserAsyncInvalid()
        {
        }

        [TestMethod]
        public void TestGetUserAsyncNull()
        {
        }

        [TestMethod]
        public void TestGetUserAsyncCantParseToken()
        {
        }

        [TestMethod]
        public void TestGetUserAsyncForcedLoginValid()
        {
        }

        [TestMethod]
        public void TestGetUserAsyncForcedLoginInvalid()
        {
        }

        [TestMethod]
        public void TestGetUserAsyncForcedLoginNull()
        {
        }

        #endregion GetUserAsync

        #region ResumeIfVerifyRefresh

        [TestMethod]
        public void TestResumeIfVerifyRefreshChooseRefreshSuccess()
        {
        }

        [TestMethod]
        public void TestResumeIfVerifyRefreshChooseRefreshFailure()
        {
        }

        [TestMethod]
        public void TestResumeIfVerifyRefreshNoRefreshSuccess()
        {
        }

        [TestMethod]
        public void TestResumeIfVerifyRefreshNoRefreshFailure()
        {
        }

        #endregion ResumeIfVerifyRefresh

        #region GetUnspecifiedResponsesAsync

        [TestMethod]
        public void TestGetUnspecifiedResponsesAsyncLineItemSet()
        {
        }

        [TestMethod]
        public void TestGetUnspecifiedResponsesAsyncTimeSet()
        {
        }

        [TestMethod]
        public void TestGetUnspecifiedResponsesAsyncBothSet()
        {
        }

        [TestMethod]
        public void TestGetUnspecifiedResponsesAsyncNeitherSet()
        {
        }

        [TestMethod]
        public void TestGetUnspecifiedResponsesAsyncNoData()
        {
        }

        [TestMethod]
        public void TestGetUnspecifiedResponsesAsyncNoLogin()
        {
        }

        #endregion GetUnspecifiedResponsesAsync

        #region SaveLineItemChoice

        [TestMethod]
        public void TestSaveLineItemChoiceInvalidKey()
        {
        }

        [TestMethod]
        public void TestSaveLineItemChoiceHaveData()
        {
        }

        [TestMethod]
        public void TestSaveLineItemChoiceNoData()
        {
        }

        #endregion SaveLineItemChoice

        #region SaveTimeChoice

        [TestMethod]
        public void TestSaveTimeChoiceInvalidOption()
        {
        }

        [TestMethod]
        public void TestSaveTimeChoiceHaveData()
        {
        }

        [TestMethod]
        public void TestSaveTimeChoiceNoData()
        {
        }

        #endregion SaveTimeChoice

        #region RunQuery

        [TestMethod]
        public void TestRunQueryInvalidUser()
        {
        }

        [TestMethod]
        public void TestRunQueryValidAll()
        {
        }

        [TestMethod]
        public void TestRunQueryNoOptionsSaved()
        {
        }

        [TestMethod]
        public void TestRunQueryGotData()
        {
        }

        [TestMethod]
        public void TestRunQueryNoData()
        {
        }

        #endregion RunQuery

        #region PrintCards

        [TestMethod]
        public void TestPrintCardsNoItems()
        {
        }

        [TestMethod]
        public void TestPrintCardsItemMissingSummary()
        {
        }

        [TestMethod]
        public void TestPrintCardsAllValid()
        {
        }

        [TestMethod]
        public void TestPrintCardsItemsModFour0()
        {
        }

        [TestMethod]
        public void TestPrintCardsItemsModFour1()
        {
        }

        [TestMethod]
        public void TestPrintCardsItemsModFour2()
        {
        }

        [TestMethod]
        public void TestPrintCardsItemsModFour3()
        {
        }

        #endregion PrintCards
    }
}
