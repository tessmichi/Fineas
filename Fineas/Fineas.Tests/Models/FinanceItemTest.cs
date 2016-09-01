// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Tests.Models
{
    using Fineas.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    [TestClass]
    public class FinanceItemTest
    {
        #region Actual_Year

        [TestMethod]
        public async Task TestActualYearWithMonthUnder6()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Month = "January, 2016";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, "2016");
        }

        [TestMethod]
        public async Task TestActualYearWithMonthOver6()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Month = "November, 2016";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, "2017");
        }

        [TestMethod]
        public async Task TestActualYearNoMonthQ1()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "1";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, "2017");
        }

        [TestMethod]
        public async Task TestActualYearNoMonthQ2()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "2";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, "2017");
        }

        [TestMethod]
        public async Task TestActualYearNoMonthQ3()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "3";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, "2016");
        }

        [TestMethod]
        public async Task TestActualYearNoMonthQ4()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "4";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, "2016");
        }

        [TestMethod]
        public async Task TestActualYearInvalidMonth()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Month = "xyz";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, string.Empty);
        }

        [TestMethod]
        public async Task TestActualYearInvalidQuarterUnder()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "0";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, string.Empty);
        }

        [TestMethod]
        public async Task TestActualYearInvalidQuarterOver()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "5";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, string.Empty);
        }

        [TestMethod]
        public async Task TestActualYearInvalidQuarter()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = "!";
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, string.Empty);
        }

        [TestMethod]
        public async Task TestActualYearEmptyQuarter()
        {
            FinanceItem item = new FinanceItem();
            
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, string.Empty);
        }

        [TestMethod]
        public async Task TestActualYearNullQuarter()
        {
            FinanceItem item = new FinanceItem();

            item.Fiscal_Quarter = null;
            item.Fiscal_Year = "2016";

            Assert.AreEqual(item.Actual_Year, string.Empty);
        }

        #endregion Actual_Year

        #region FinanceItem

        [TestMethod]
        public async Task TestFinanceItemCreateEmpty()
        {
            FinanceItem item = new FinanceItem();

            Assert.IsNotNull(item);
            //Assert.AreEqual(item, FinanceItem.Unknown);
        }

        #endregion FinanceItem

        #region GetSummaries

        [TestMethod]
        public async Task TestGetSummariesNoMonth()
        {
            FinanceItem item = new FinanceItem();

            item.Team = "TED";
            item.Fiscal_Quarter = "FY16-Q4";
            item.Fiscal_Year = "2016";
            item.Line_Item = "Morale";

            item.Actual = "31864";
            item.Forecast = "33794";
            item.VTF = "1929.8571";

            Dictionary<string, string> summaries = new Dictionary<string, string>();
            string time = (item.Fiscal_Month != string.Empty ? item.Fiscal_Month : item.Fiscal_Quarter);
            summaries.Add(
                "TED's Forecast Report for Morale",
                "For line item Morale, TED was allocated $33,794.00 for FY16-Q4.");
            summaries.Add(
                "TED's Actual Report for Morale",
                "For line item Morale, TED spent $31,864.00 for FY16-Q4.");
            summaries.Add(
                "TED's Variance to Forecast Report for Morale",
                "For line item Morale, TED varies to forecast by $1,929.86 for FY16-Q4.");

            foreach (string key in summaries.Keys)
            {
                Assert.AreEqual(item.GetSummaries()[key], summaries[key]);
            }

            Assert.AreEqual(item.GetSummaries().Count, summaries.Count);
        }

        [TestMethod]
        public async Task TestGetSummariesNoQuarter()
        {
            FinanceItem item = new FinanceItem();

            item.Team = "TED";
            item.Fiscal_Month = "June, 2016";
            item.Fiscal_Year = "2016";
            item.Line_Item = "Morale";

            item.Actual = "20435";
            item.Forecast = "11231";
            item.VTF = "-9204.0969";

            Dictionary<string, string> summaries = new Dictionary<string, string>();
            string time = (item.Fiscal_Month != string.Empty ? item.Fiscal_Month : item.Fiscal_Quarter);
            summaries.Add(
                "TED's Forecast Report for Morale",
                "For line item Morale, TED was allocated $11,231.00 for June, 2016.");
            summaries.Add(
                "TED's Actual Report for Morale",
                "For line item Morale, TED spent $20,435.00 for June, 2016.");
            summaries.Add(
                "TED's Variance to Forecast Report for Morale",
                "For line item Morale, TED varies to forecast by $-9,204.10 for June, 2016.");

            foreach (string key in summaries.Keys)
            {
                Assert.AreEqual(item.GetSummaries()[key], summaries[key]);
            }

            Assert.AreEqual(item.GetSummaries().Count, summaries.Count);
        }

        [TestMethod]
        public async Task TestGetSummariesUnknown()
        {
            Assert.IsTrue(FinanceItem.Unknown.GetSummaries().Count == 0);
        }

        #endregion GetSummaries

        #region NormalizeDollar

        //[TestMethod]
        //public async Task TestNormalizeDollar()
        //{
        //}

        //[TestMethod]
        //public async Task TestNormalizeDollarInputNull()
        //{
        //    string dollar = null;
        //}

        //[TestMethod]
        //public async Task TestNormalizeDollarInputInvalid()
        //{
        //}

        //[TestMethod]
        //public async Task TestNormalizeDollarInputTooLarge()
        //{
        //}

        //[TestMethod]
        //public async Task TestNormalizeDollarInputEmpty()
        //{
        //}

        #endregion NormalizeDollar

        #region SetProperties

        [TestMethod]
        public async Task TestSetProperties()
        {
            List<FieldInfo> props = typeof(FinanceItem).GetFields().ToList();

            FinanceItem.SetProperties();

            foreach (FieldInfo prop in FinanceItem.Properties)
            {
                Assert.IsTrue(
                    props.Where(col => col.Name == prop.Name).ToList().Count
                    > 0);
            }
            foreach (FieldInfo prop in props)
            {
                Assert.IsTrue(
                    FinanceItem.Properties.Where(col => col.Name == prop.Name).ToList().Count
                    > 0);
            }
        }

        #endregion SetProperties
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
