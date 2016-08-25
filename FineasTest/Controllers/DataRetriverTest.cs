using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fineas.Controllers;

namespace FineasTest.Controllers
{
    [TestClass]
    class DataRetriverTest
    {
        #region QueryFromData

        [TestMethod]
        public void TestQueryFromDataValid()
        {
        }

        [TestMethod]
        public void TestQueryFromDataTimeInvalid()
        {
        }

        [TestMethod]
        public void TestQueryFromDataItemInvalid()
        {
        }

        [TestMethod]
        public void TestQueryFromDataAliasInvalid()
        {
        }

        [TestMethod]
        public void TestQueryFromDataDateInvalid()
        {
        }

        [TestMethod]
        public void TestQueryFromDataAllInvalid()
        {
        }

        [TestMethod]
        public void TestQueryFromDataTimeNull()
        {
        }

        [TestMethod]
        public void TestQueryFromDataItemNull()
        {
        }

        [TestMethod]
        public void TestQueryFromDataAliasNull()
        {
        }

        [TestMethod]
        public void TestQueryFromDataDateNull()
        {
        }

        [TestMethod]
        public void TestQueryFromDataAllNull()
        {
        }

        #endregion QueryFromData

        #region DeleteAllData

        [TestMethod]
        public void TestDeleteAllDataEmpty()
        {
        }

        [TestMethod]
        public void TestDeleteAllDataFull()
        {
        }

        #endregion DeleteAllData

        #region GetAllData

        [TestMethod]
        public void TestGetAllDataValid()
        {
        }

        [TestMethod]
        public void TestGetAllDataConnectionInvalid()
        {
        }

        [TestMethod]
        public void TestGetAllDataAliasInvalid()
        {
        }

        [TestMethod]
        public void TestGetAllDataAliasNull()
        {
        }

        #endregion GetAllData

        #region HaveData

        [TestMethod]
        public void TestHaveDataValid()
        {
            //Assert.IsTrue(DataRetriever.HaveData());
        }

        [TestMethod]
        public void TestHaveDataPartialEmpty()
        {
        }

        [TestMethod]
        public void TestHaveDataAllEmpty()
        {
        }

        #endregion HaveData

        #region SetMetaOptions

        [TestMethod]
        public void TestRunSetMetaOptions()
        {
        }

        #endregion SetMetaOptions

        #region SetTimeOptions

        [TestMethod]
        public void TestRunSetTimeOptions()
        {
        }

        #endregion SetTimeOptions

        #region SetItemDetails

        [TestMethod]
        public void TestSetItemDetailsValid()
        {
        }

        [TestMethod]
        public void TestSetItemDetailsDataPopulated()
        {
        }
        
        [TestMethod]
        public void TestSetItemDetailsNullOrEmptyConnectionString()
        {
        }

        [TestMethod]
        public void TestSetItemDetailsInvalidConnectionString()
        {
        }

        [TestMethod]
        public void TestSetItemDetailsNullOrEmptyQuery()
        {
        }

        [TestMethod]
        public void TestSetItemDetailsInvalidQuery()
        {
        }
        
        [TestMethod]
        public void TestSetItemDetailsNoDescriptions()
        {
        }

        [TestMethod]
        public void TestSetItemDetailsNoDataHasNoDescription()
        {
        }

        #endregion SetItemDetails

        #region GetDataForTable

        [TestMethod]
        public void TestGetDataForTableValid()
        {
        }

        [TestMethod]
        public void TestGetDataForTableDataPopulated()
        {
        }

        [TestMethod]
        public void TestGetDataForTableNullOrEmptyConnectionString()
        {
        }

        [TestMethod]
        public void TestGetDataForTableInvalidConnectionString()
        {
        }

        [TestMethod]
        public void TestGetDataForTableNullOrEmptyQuery()
        {
        }

        [TestMethod]
        public void TestGetDataForTableInvalidQuery()
        {
        }

        [TestMethod]
        public void TestGetDataForTableNoData()
        {
        }

        [TestMethod]
        public void TestGetDataForTableAliasInvalid()
        {
        }

        [TestMethod]
        public void TestGetDataForTableAliasNull()
        {
        }

        [TestMethod]
        public void TestGetDataForTableTableInvalid()
        {
        }

        [TestMethod]
        public void TestGetDataForTableTableNull()
        {
        }

        #endregion GetDataForTable
    }
}
