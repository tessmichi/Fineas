// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Models
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Text;

    [Serializable]
    public class User
    {
        // TODO: this shouldn't all be exposed or publically available for setting
        public string Token = string.Empty;
        public string GivenName = string.Empty;
        public string FamilyName = string.Empty;
        public bool InCorp = false;
        public string IpAddress = string.Empty;
        public string Platform = string.Empty;
        public string Upn = string.Empty;
        public string Alias = string.Empty;

        public static User Unknown = new User();
        public static string CORP_DOMAIN = "";

        private User()
        {

        }

        public User(string token)
        {
            DecodeJWTToken(token);
        }

        private void DecodeJWTToken(string tkn)
        {
            Token = tkn;

            if (string.IsNullOrEmpty(tkn))
            {
                Clear();
            }
            else
            {
                try
                {
                    var startIndex = tkn.IndexOf('.') + 1;
                    var length = tkn.LastIndexOf('.') - startIndex;
                    if (length < 0 || length + startIndex > tkn.Length)
                    {
                        return;
                    }
                    var jwtToken = tkn.Substring(startIndex, length).Trim();

                    while (jwtToken.Length % 4 != 0)
                    {
                        jwtToken += "=";
                    }
                    var decodedToken = Convert.FromBase64String(jwtToken);

                    var utf8Token = Encoding.UTF8.GetString(decodedToken);
                    JObject result = JObject.Parse(utf8Token);

                    GivenName = result.GetValue("given_name").ToString();
                    FamilyName = result.GetValue("family_name").ToString();
                    InCorp = Convert.ToBoolean(result.GetValue("in_corp"));
                    IpAddress = result.GetValue("ipaddr").ToString();
                    Platform = result.GetValue("platf").ToString();
                    Upn = result.GetValue("upn").ToString();
                    Alias = Upn.Substring(0, Upn.IndexOf('@')).ToUpper();
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Clear();
                }
            }
        }

        public bool VerifyUser()
        {
            // TODO: should support multiple domains at a time
            string checkEmail = CORP_DOMAIN;
            int index = Upn.Length - checkEmail.Length >= 0 ? Upn.Length - checkEmail.Length : 0;
            string userEmail = Upn.Substring(index);
            return string.Equals(userEmail, checkEmail, StringComparison.OrdinalIgnoreCase);
            // Use this instead of EndsWith because need to ignore case
        }

        private void Clear()
        {
            // TODO: use reflection

            Token = string.Empty;
            GivenName = string.Empty;
            FamilyName = string.Empty;
            InCorp = false;
            IpAddress = string.Empty;
            Platform = string.Empty;
            Upn = string.Empty;
            Alias = string.Empty;
        }

        public bool Equals(User other)
        {
            // TODO: use reflection

            return (Token == other.Token &&
                GivenName == other.GivenName &&
                FamilyName == other.FamilyName &&
                InCorp == other.InCorp &&
                IpAddress == other.IpAddress &&
                Platform == other.Platform &&
                Upn == other.Upn &&
                Alias == other.Alias);
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
