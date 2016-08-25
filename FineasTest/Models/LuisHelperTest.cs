using Fineas.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineasTest.Models
{
    [TestClass]
    class LuisHelperTest
    {
        #region ParseUserInput

        [TestMethod]
        public void TestParseUserInputValid()
        {
        }

        [TestMethod]
        public void TestParseUserInputPartial()
        {
        }

        [TestMethod]
        public void TestParseUserInputInvalid()
        {
        }

        [TestMethod]
        public void TestParseUserInputEmpty()
        {
        }

        [TestMethod]
        public void TestParseUserInputNull()
        {
        }

        #endregion ParseUserInput

        #region LuisResponse

        [TestMethod]
        public void TestLuisResponse()
        {
            Assert.IsNotNull(new LuisResponse());
        }

        #endregion LuisResponse

        #region LuisIntent

        [TestMethod]
        public void TestLuisIntent()
        {
            Assert.IsNotNull(new LuisIntent());
        }

        #endregion LuisIntent

        #region LuisEntity

        [TestMethod]
        public void TestLuisEntity()
        {
            Assert.IsNotNull(new LuisEntity());
        }

        #endregion LuisEntity
    }
}
