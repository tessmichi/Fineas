// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Tests.Controllers
{
    using Fineas.Controllers;
    using Microsoft.IdentityModel.Protocols;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class DataRetriverTest
    {
        public async Task TestLastRefresh()
        {
            DateTime now = DateTime.Now;
            DataRetriever.GetAllData(ConfigurationManager.AppSettings["Alias"]);
            Assert.IsTrue(DataRetriever.LastRefresh >= now);
        }

        //#region QueryFromData

        //[TestMethod]
        //public async Task TestQueryFromDataValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataTimeInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataItemInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataAliasInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataDateInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataAllInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataTimeNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataItemNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataAliasNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataDateNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestQueryFromDataAllNull()
        //{
        //}

        //#endregion QueryFromData

        //#region DeleteAllData

        //[TestMethod]
        //public async Task TestDeleteAllDataEmpty()
        //{
        //}

        //[TestMethod]
        //public async Task TestDeleteAllDataFull()
        //{
        //}

        //#endregion DeleteAllData

        //#region GetAllData

        //[TestMethod]
        //public async Task TestGetAllDataValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetAllDataConnectionInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetAllDataAliasInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetAllDataAliasNull()
        //{
        //}

        //#endregion GetAllData

        //#region HaveData

        //[TestMethod]
        //public async Task TestHaveDataValid()
        //{
        //    //Assert.IsTrue(DataRetriever.HaveData());
        //}

        //[TestMethod]
        //public async Task TestHaveDataPartialEmpty()
        //{
        //}

        //[TestMethod]
        //public async Task TestHaveDataAllEmpty()
        //{
        //}

        //#endregion HaveData

        //#region SetMetaOptions

        //[TestMethod]
        //public async Task TestRunSetMetaOptions()
        //{
        //}

        //#endregion SetMetaOptions

        //#region SetTimeOptions

        //[TestMethod]
        //public async Task TestRunSetTimeOptions()
        //{
        //}

        //#endregion SetTimeOptions

        //#region SetItemDetails

        //[TestMethod]
        //public async Task TestSetItemDetailsValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsDataPopulated()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsNullOrEmptyConnectionString()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsInvalidConnectionString()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsNullOrEmptyQuery()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsInvalidQuery()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsNoDescriptions()
        //{
        //}

        //[TestMethod]
        //public async Task TestSetItemDetailsNoDataHasNoDescription()
        //{
        //}

        //#endregion SetItemDetails

        //#region GetDataForTable

        //[TestMethod]
        //public async Task TestGetDataForTableValid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableDataPopulated()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableNullOrEmptyConnectionString()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableInvalidConnectionString()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableNullOrEmptyQuery()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableInvalidQuery()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableNoData()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableAliasInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableAliasNull()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableTableInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestGetDataForTableTableNull()
        //{
        //}

        //#endregion GetDataForTable
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
