// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Models
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    [Serializable]
    public class FinanceItem
    {
        // TODO: make sure the website showing this api is safe
        // TODO: web.config to hide website from any user that isnt the bot
        // web exploit check tools online against my url
        
        private static List<FieldInfo> _properties = new List<FieldInfo>();

        public static FinanceItem Unknown = new FinanceItem();
        
        public string Team = string.Empty;
        public string Fiscal_Month = string.Empty;
        public string Fiscal_Quarter = string.Empty;
        public string Fiscal_Year = string.Empty;
        public string Line_Item = string.Empty;

        public string Actual = string.Empty;
        public string Forecast = string.Empty;

        // this one is not in the database so the "setters" for month and quarter set this variable
        public string Actual_Year
        {
            get
            {
                string yearRaw = string.Format("January, 20{0}", Fiscal_Year.Substring(2));
                var year = DateTime.Parse(yearRaw).AddYears(1).Year.ToString();

                if (!string.IsNullOrEmpty(Fiscal_Month.Trim()))
                {
                    var month = DateTime.Parse(Fiscal_Month);
                    return month.Month > 6 ?
                        year :
                        string.Format("20{0}", Fiscal_Year.Substring(2));
                }
                else
                {
                    return Convert.ToInt32(Fiscal_Quarter.Substring(Fiscal_Quarter.Length-1)) <= 2 ?
                        year :
                        string.Format("20{0}", Fiscal_Year.Substring(2));
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

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append(string.Format("For line item {0}, {1} {2} for {3}.",
                Line_Item,
                Team,
                (Actual != string.Empty ? string.Format("spent ${0}", NormalizeDollar(Actual)) : string.Format("was allocated ${0}", NormalizeDollar(Forecast))),
                (Fiscal_Month != string.Empty ? Fiscal_Month : Fiscal_Quarter)));

            return build.ToString();
        }

        private string NormalizeDollar(string num)
        {
            return Convert.ToDouble(num).ToString("0.00");
        }

        public string ShortString()
        {
            StringBuilder build = new StringBuilder();
            build.Append(string.Format("{0}'s {1} Report for {2}",
                Team,
                (Actual != string.Empty ? "Actual" : "Forecast"),
                Line_Item));

            return build.ToString();
        }

        public string DataString()
        {
            StringBuilder build = new StringBuilder();

            FieldInfo[] props = typeof(FinanceItem).GetFields();

            foreach (FieldInfo prop in props)
            {
                var value = prop.GetValue(this);
                if (!prop.Name.ToUpper().Contains("PROP") && value.ToString().Length > 0)
                    build.Append(string.Format("{0}: {1}; ", prop.Name, prop.GetValue(this)));
            }

            return build.ToString();
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
