// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Tests.Models
{
    using Fineas.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Configuration;
    using System.Threading.Tasks;

    [TestClass]
    public class UserTest
    {
        #region User

        //[TestMethod]
        //public void TestUserDefault()
        //{
        //    User user = new User();
        //}

        [TestMethod]
        public async Task TestUserTokenValid()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = ConfigurationManager.AppSettings["ValidToken"];
            User user = new User(token);

            Assert.IsNotNull(user);
            //Assert.AreNotEqual(user, User.Unknown);
            Assert.IsFalse(user.Equals(User.Unknown));

            Assert.AreEqual(token,
                user.Token);
            Assert.AreEqual(ConfigurationManager.AppSettings["GivenName"],
                user.GivenName);
            Assert.AreEqual(ConfigurationManager.AppSettings["FamilyName"],
                user.FamilyName);
            Assert.AreEqual(Convert.ToBoolean(ConfigurationManager.AppSettings["InCorp"]),
                user.InCorp);
            Assert.AreEqual(ConfigurationManager.AppSettings["IpAddress"],
                user.IpAddress);
            Assert.AreEqual(ConfigurationManager.AppSettings["Platform"],
                user.Platform);
            Assert.AreEqual(ConfigurationManager.AppSettings["Upn"],
                user.Upn);
            Assert.AreEqual(ConfigurationManager.AppSettings["Alias"],
                user.Alias);
        }

        [TestMethod]
        public async Task TestUserTokenInvalid()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = ConfigurationManager.AppSettings["InvalidToken"];
            User user = new User(token);

            Assert.IsNotNull(user);
            //Assert.AreEqual(user, User.Unknown);
            Assert.IsFalse(user.Equals(User.Unknown));
            Assert.AreNotEqual(user.Token, User.Unknown.Token);

            Assert.AreEqual(token, user.Token);
            Assert.AreEqual(string.Empty, user.GivenName);
            Assert.AreEqual(string.Empty, user.FamilyName);
            Assert.AreEqual(false, user.InCorp);
            Assert.AreEqual(string.Empty, user.IpAddress);
            Assert.AreEqual(string.Empty, user.Platform);
            Assert.AreEqual(string.Empty, user.Upn);
            Assert.AreEqual(string.Empty, user.Alias);
        }

        [TestMethod]
        public async Task TestUserTokenEmpty()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = string.Empty;
            User user = new User(token);

            Assert.IsNotNull(user);
            //Assert.AreEqual(user, User.Unknown);
            Assert.IsTrue(user.Equals(User.Unknown));

            Assert.AreEqual(token, user.Token);
            Assert.AreEqual(string.Empty, user.GivenName);
            Assert.AreEqual(string.Empty, user.FamilyName);
            Assert.AreEqual(false, user.InCorp);
            Assert.AreEqual(string.Empty, user.IpAddress);
            Assert.AreEqual(string.Empty, user.Platform);
            Assert.AreEqual(string.Empty, user.Upn);
            Assert.AreEqual(string.Empty, user.Alias);
        }

        [TestMethod]
        public async Task TestUserTokenNull()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = null;
            User user = new User(token);

            Assert.IsNotNull(user);
            //Assert.AreEqual(user, User.Unknown);
            Assert.IsTrue(user.Equals(User.Unknown));

            Assert.AreEqual(string.Empty, user.Token);
            Assert.AreEqual(string.Empty, user.GivenName);
            Assert.AreEqual(string.Empty, user.FamilyName);
            Assert.AreEqual(false, user.InCorp);
            Assert.AreEqual(string.Empty, user.IpAddress);
            Assert.AreEqual(string.Empty, user.Platform);
            Assert.AreEqual(string.Empty, user.Upn);
            Assert.AreEqual(string.Empty, user.Alias);
        }

        #endregion User

        #region DecodeJWTToken

        //[TestMethod]
        //public void TestDecodeJWTTokenValid()
        //{

        //}

        //[TestMethod]
        //public void TestDecodeJWTTokenInvalid()
        //{

        //}

        //[TestMethod]
        //public void TestDecodeJWTTokenEmpty()
        //{

        //}

        //[TestMethod]
        //public void TestDecodeJWTTokenNull()
        //{

        //}

        #endregion DecodeJWTToken

        #region VerifyUser

        [TestMethod]
        public async Task TestVerifyUserValid()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = ConfigurationManager.AppSettings["ValidToken"];
            User user = new User(token);

            Assert.IsTrue(user.VerifyUser());
        }

        [TestMethod]
        public async Task TestVerifyUserInvalid()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = ConfigurationManager.AppSettings["InvalidToken"];
            User user = new User(token);

            Assert.IsFalse(user.VerifyUser());
        }

        [TestMethod]
        public async Task TestVerifyUserShortUpn()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = ConfigurationManager.AppSettings["ValidToken"];
            User user = new User(token);
            user.Upn = "";

            Assert.IsFalse(user.VerifyUser());
        }

        [TestMethod]
        public async Task TestVerifyUserEmpty()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = string.Empty;
            User user = new User(token);

            Assert.IsFalse(user.VerifyUser());
        }

        [TestMethod]
        public async Task TestVerifyUserNull()
        {
            User.CORP_DOMAIN = "@microsoft.com";
            string token = null;
            User user = new User(token);

            Assert.IsFalse(user.VerifyUser());
        }

        #endregion VerifyUser

        #region Clear

        //[TestMethod]
        //public void TestClear()
        //{
        //    string token = ConfigurationManager.AppSettings["ValidToken"];
        //    User user = new User(token);

        //    Assert.IsNotNull(user);
        //    Assert.AreNotEqual(user, User.Unknown);

        //    user.Clear();

        //    Assert.IsNotNull(user);
        //    Assert.AreEqual(user, User.Unknown);
        //}

        #endregion Clear
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
