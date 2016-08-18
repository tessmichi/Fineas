// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Models
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Text;

    [Serializable]
    public class User
    {
        public string token = string.Empty;
        public string given_name = string.Empty;
        public string family_name = string.Empty;
        public bool in_corp = false;
        public string ipaddr = string.Empty;
        public string platf = string.Empty;
        public string upn = string.Empty;
        public string alias = string.Empty;

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
            token = tkn;

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
                    
                    given_name = result.GetValue("given_name").ToString();
                    family_name = result.GetValue("family_name").ToString();
                    in_corp = Convert.ToBoolean(result.GetValue("in_corp"));
                    ipaddr = result.GetValue("ipaddr").ToString();
                    platf = result.GetValue("platf").ToString();
                    upn = result.GetValue("upn").ToString();
                    alias = upn.Substring(0, upn.IndexOf('@')).ToUpper();
                }
                catch (FormatException e)
                {
                    Clear();
                }
            }
        }

        public bool VerifyUser()
        {
            // TODO: could be in microsoft but not on our domain
            string checkEmail = CORP_DOMAIN;
            int index = upn.Length - checkEmail.Length >= 0 ? upn.Length - checkEmail.Length : 0;
            string userEmail = upn.Substring(index);
            return string.Equals(userEmail, checkEmail, StringComparison.OrdinalIgnoreCase);
            // Use this instead of EndsWith because need to ignore case
        }

        private void Clear()
        {
            // TODO: use reflection

            token = string.Empty;
            given_name = string.Empty;
            family_name = string.Empty;
            in_corp = false;
            ipaddr = string.Empty;
            platf = string.Empty;
            upn = string.Empty;
            alias = string.Empty;
        }
        
        public bool Equals(User other)
        {
            // TODO: use reflection

            return ( token == other.token &&
                given_name == other.given_name &&
                family_name == other.family_name &&
                in_corp == other.in_corp &&
                ipaddr == other.ipaddr &&
                platf == other.platf &&
                upn == other.upn &&
                alias == other.alias);
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
