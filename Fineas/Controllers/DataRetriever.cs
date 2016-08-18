// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Controllers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    [Serializable]
    public class DataRetriever
    {
        #region Attributes to Refresh

        private static DateTime lastRefresh = DateTime.MinValue;

        // TODO: sql parameters
        private static string qryGeneral = "select a.*, b.[Dx Team] from {0} a, FTEList_Team b where a.TEAM = b.TEAM AND upper(b.Email) = upper('{1}')";
        private static string qryLineItems = "select distinct [line item] from vw_BotSummaryByMonthActuals"; // TODO: this shouldn't be hardcoded to a table
        private static string[] tables = {  "vw_BotSummaryByMonthActuals",
                                            "vw_BotSummaryByMonthForecast",
                                            "vw_BotSummaryByQtrActuals",
                                            "vw_BotSummaryByQtrForecast"};

        private static Dictionary<string, List<FinanceItem>> _info = new Dictionary<string, List<FinanceItem>>();

        public const int AMT_MIN_CHECK_REFRESH = -5;

        public static DateTime LastRefresh
        {
            get
            {
                return lastRefresh;
            }
        }

        public static string[] TimeframeOptions = new string[0];
        public static string[] DataTypeOptions = new string[0];
        public static List<string> DataItemOptions = new List<string>();
        
        #endregion Attributes to Refresh

        public static List<FinanceItem> QueryFromData(string time, string item, string alias, DateTime date)
        {
            List<string> tableOptions = (from tbl in tables
                                         where tbl.ToUpper().Contains((time.ToUpper() == "MTD") ? "MONTH" : "QTR")
                                         select tbl).ToList();

            List<FinanceItem> res = new List<FinanceItem>();

            // TODO: ensure there is only one table in tableOptions
            foreach (string table in tableOptions)
            {
                try
                {
                    List<FinanceItem> temp = new List<FinanceItem>();

                    temp = (from dataRow in _info[table]
                           where dataRow.Line_Item.Trim() == item
                           where dataRow.Actual_Year == date.Year.ToString()
                           select dataRow).ToList();

                    if (time.Equals("MTD", StringComparison.OrdinalIgnoreCase))
                    {
                        temp = (from dataRow in temp
                                where DateTime.Parse(dataRow.Fiscal_Month).Month == date.Month
                               select dataRow).ToList();
                    }
                    else if (time.Equals("QTD", StringComparison.OrdinalIgnoreCase))
                    {
                        var quarter = (((date.Month - 1) / 3) + 3);
                        quarter = (quarter > 4) ? quarter % 4 : quarter;

                        temp = (from dataRow in temp
                               where Convert.ToInt32(dataRow.Fiscal_Quarter.Substring(dataRow.Fiscal_Quarter.Length - 1)) == quarter
                               select dataRow).ToList();
                    }

                    res.AddRange(temp);
                }
                catch (KeyNotFoundException e)
                {
                    // TODO: here is where we tell the user to try the query again.
                }
                catch (Exception e)
                {

                }
            }
            
            return res.ToList();
        }

        public static void DeleteAllData()
        {
            _info.Clear();
            TimeframeOptions = new string[0];
            DataTypeOptions = new string[0];
            DataItemOptions.Clear();
        }

        public static void GetAllData(string alias)
        {
            DeleteAllData();

            lastRefresh = DateTime.Now;
            foreach (string table in tables)
            {
                GetDataForTable(table, alias);
            }

            SetOptions();
        }

        #region Methods to Refresh / Get

        private static void SetOptions()
        {
            SetTimeOptions();
            SetTypeOptions();
            SetItemOptions();
        }
        
        private static void SetTimeOptions()
        {
            // TODO: not hardcode

            string[] ops = { "MTD", "QTD" };
            TimeframeOptions = ops;
        }

        private static void SetTypeOptions()
        {
            // TODO: not hardcode

            string[] ops = { "Forecast", "Actual" };
            DataTypeOptions = ops;
        }

        private static void SetItemOptions()
        {
            DataTable sqlTable = new DataTable();
            List<string> options = new List<string>();

            using (SqlConnection connection = new SqlConnection(SqlDb.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand(qryLineItems, connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            FinanceItem item = new FinanceItem();
                            
                            string value = reader.GetValue(0).ToString().Trim();
                            options.Add(value);
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }

            DataItemOptions = options;
        }

        private static void GetDataForTable(string table, string alias)
        {
            DataTable sqlTable = new DataTable();
            _info.Add(table, new List<FinanceItem>());
            List<string> colsToIgnore = new List<string>();

            using (SqlConnection connection = new SqlConnection(SqlDb.GetConnectionString()))
            {
                string qry = qryGeneral;
                using (SqlCommand cmd = new SqlCommand(string.Format(qry, table, alias.ToUpper()), connection))
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {
                            FinanceItem item = new FinanceItem();

                            // Iterate names of fields in object and set field based on matching column
                            foreach (FieldInfo col in FinanceItem.Properties)
                            {
                                if (!colsToIgnore.Contains(col.Name)) // This should increase speed a bit.
                                {
                                    try
                                    {
                                        string colNameConverted = col.Name.Replace("_", " ");
                                        int index = reader.GetOrdinal(colNameConverted);

                                        if (index >= 0 && index < reader.FieldCount)
                                        {
                                            string value = reader.GetValue(index).ToString().Trim();
                                            col.SetValue(item, value);
                                        }
                                    }
                                    catch (IndexOutOfRangeException e)
                                    {
                                        colsToIgnore.Add(col.Name);
                                        Console.WriteLine(string.Format("Looks like one of our columns wasn't found: {0}", col));
                                    }
                                }
                            }

                            _info[table].Add(item);
                        }
                    }
                    finally
                    {
                        // Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }

        #endregion Methods to Refresh / Get
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
