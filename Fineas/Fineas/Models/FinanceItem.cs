// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;
    using System.Text;

    [Serializable]
    public class FinanceItem
    {
        // TODO: make sure the website showing this api is safe
        // TODO: web.config to hide website from any user that isnt the bot
        // web exploit check tools online against my url

        // ***************************************************
        // ***************************************************
        // *  DO NOT CHANGE NAMES OF ANY OF THESE PROPERTIES *
        // *  AS THEY MATCH EXACTLY WITH THE DATABASE        *
        // ***************************************************
        // ***************************************************

        private static List<FieldInfo> _properties = new List<FieldInfo>();
        private static string SHORT_STRING = "{0}'s {1} Report for {2}";
        private static string LONG_STRING = "For line item {0}, {1} {2}{3}.";

        public static FinanceItem Unknown = new FinanceItem();

        // TODO: these should not have public sets. Should take in data row into a constructor

        public string Team = string.Empty;
        public string Fiscal_Month = string.Empty;
        public string Fiscal_Quarter = string.Empty;
        public string Fiscal_Year = string.Empty;
        public string Line_Item = string.Empty;

        public string Actual = string.Empty;
        public string Forecast = string.Empty;
        public string VTF = string.Empty;

        // this one is not in the database so the "setters" for month and quarter set this variable
        public string Actual_Year
        {
            get
            {
                try
                {
                    DateTime yearCurrent = Convert.ToDateTime(string.Format("January, 20{0}", Fiscal_Year.Substring(2)));
                    DateTime yearNext = yearCurrent.AddYears(1);

                    if (!string.IsNullOrEmpty(Fiscal_Month.Trim()))
                    {
                        DateTime month = DateTime.Parse(Fiscal_Month);
                        return month.Month > 6 ?
                            yearNext.Year.ToString() :
                            yearCurrent.Year.ToString();
                    }
                    else if (!string.IsNullOrEmpty(Fiscal_Quarter.Trim()))
                    {
                        int quarter;
                        if (int.TryParse(Fiscal_Quarter.Substring(Fiscal_Quarter.Length - 1), out quarter))
                            return quarter <= 2 && quarter >= 1 ?
                                yearNext.Year.ToString() :
                                quarter <= 4 && quarter >= 3 ?
                                    yearCurrent.Year.ToString() :
                                    string.Empty;
                    }
                    return string.Empty;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    return string.Empty;
                }
            }
        }

        public static List<FieldInfo> Properties
        {
            get
            {
                return _properties;
            }
        }

        public FinanceItem()
        {

        }

        // Returns a dictionary of summaries.
        // Has one entry per summary type: Forecast, Actual, Variance
        // Each entry contains the words used to describe that entry along with its value
        // <short, long>
        public Dictionary<string, string> GetSummaries()
        {
            Dictionary<string, string> summaries = new Dictionary<string, string>();

            string time = Fiscal_Month != string.Empty || Fiscal_Quarter != string.Empty ?
                string.Format(" for {0}", (Fiscal_Month != string.Empty ? Fiscal_Month : Fiscal_Quarter)) :
                string.Empty;

            // Forecast
            summaries.Add(
                string.Format(SHORT_STRING,
                    Team,
                    "Forecast",
                    Line_Item),
                Forecast == string.Empty ?
                    $"Could not parse database entry{time}." :
                    string.Format(LONG_STRING,
                        Line_Item,
                        Team,
                        string.Format("was allocated {0}", NormalizeDollar(Forecast)),
                        time));

            // Actual
            summaries.Add(
                string.Format(SHORT_STRING,
                    Team,
                    "Actual",
                    Line_Item),
                Actual == string.Empty ?
                    $"Could not parse database entry{time}." :
                    string.Format(LONG_STRING,
                        Line_Item,
                        Team,
                        string.Format("spent {0}", NormalizeDollar(Actual)),
                        time));

            // Variance (to forecast)
            summaries.Add(
                string.Format(SHORT_STRING,
                    Team,
                    "Variance to Forecast",
                    Line_Item),
                VTF == string.Empty ?
                    $"Could not parse database entry {time}." :
                    string.Format(LONG_STRING,
                        Line_Item,
                        Team,
                        string.Format("varies to forecast by {0}", NormalizeDollar(VTF)),
                        time));

            return summaries;
        }

        private string NormalizeDollar(string num)
        {
            decimal ret = 0.0M;

            if (!decimal.TryParse(num, NumberStyles.Any, CultureInfo.InvariantCulture, out ret))
            {
                // This prints out the unformatted number, raw from the database
                return $"${num}";
            }

            return $"${ret.ToString("#,##0.00")}";
        }

        public static void SetProperties()
        {
            FieldInfo[] props = typeof(FinanceItem).GetFields();

            foreach (FieldInfo prop in props)
            {
                _properties.Add(prop);
            }
        }
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
