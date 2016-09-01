// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Tests.Models
{
    using Fineas.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class SqlDbTest
    {
        #region Getters and Setters - Valid Input

        [TestMethod]
        public async Task TestServer()
        {
            string test = "Server";
            SqlDb.Server = test;
            Assert.AreEqual(SqlDb.Server, test);
        }

        [TestMethod]
        public async Task TestInitialCatalog()
        {
            string test = "InitialCatalog";
            SqlDb.InitialCatalog = test;
            Assert.AreEqual(SqlDb.InitialCatalog, test);
        }

        [TestMethod]
        public async Task TestPersistSecurityInfoBool()
        {
            string test = "true";
            SqlDb.PersistSecurityInfo = test;
            string testBool = Convert.ToBoolean(test).ToString();
            Assert.AreEqual(SqlDb.PersistSecurityInfo, testBool);
        }

        [TestMethod]
        public async Task TestUserId()
        {
            string test = "UserId";
            SqlDb.UserId = test;
            Assert.AreEqual(SqlDb.UserId, test);
        }

        [TestMethod]
        public async Task TestPassword()
        {
            string test = "Password";
            SqlDb.Password = test;
            Assert.AreEqual(SqlDb.Password, test);
        }
        
        [TestMethod]
        public async Task TestMultipleActiveResultSetsBool()
        {
            string test = "true";
            SqlDb.MultipleActiveResultSets = test;
            string testBool = Convert.ToBoolean(test).ToString();
            Assert.AreEqual(SqlDb.MultipleActiveResultSets, testBool);
        }

        [TestMethod]
        public async Task TestEncryptBool()
        {
            string test = "true";
            SqlDb.Encrypt = test;
            string testBool = Convert.ToBoolean(test).ToString();
            Assert.AreEqual(SqlDb.Encrypt, testBool);
        }

        [TestMethod]
        public async Task TestTrustCertificateBool()
        {
            string test = "true";
            SqlDb.TrustCertificate = test;
            string testBool = Convert.ToBoolean(test).ToString();
            Assert.AreEqual(SqlDb.TrustCertificate, testBool);
        }

        [TestMethod]
        public async Task TestConnectionTimeoutNumber()
        {
            string test = "50000";
            SqlDb.ConnectionTimeout = test;
            Assert.AreEqual(SqlDb.ConnectionTimeout, test);
        }

        [TestMethod]
        public async Task TestConnectionString()
        {
            string test = "ConnectionString";
            SqlDb.ConnectionString = test;
            Assert.AreEqual(SqlDb.ConnectionString, test);
        }

        #endregion Getters and Setters - Valid Input

        #region Getters and Setters - Invalid Input

        [TestMethod]
        public async Task TestPersistSecurityInfoString()
        {
            string test = "PersistSecurityInfo";
            bool testBool = true;
            SqlDb.PersistSecurityInfo = test;
            Assert.IsNotNull(SqlDb.PersistSecurityInfo);
            Assert.AreEqual(SqlDb.PersistSecurityInfo, testBool.ToString());
        }

        [TestMethod]
        public async Task TestMultipleActiveRulesetsString()
        {
            string test = "MultipleActiveResultSets";
            bool testBool = true;
            SqlDb.MultipleActiveResultSets = test;
            Assert.IsNotNull(SqlDb.MultipleActiveResultSets);
            Assert.AreEqual(SqlDb.MultipleActiveResultSets, testBool.ToString());
        }

        [TestMethod]
        public async Task TestEncryptString()
        {
            string test = "Encrypt";
            bool testBool = true;
            SqlDb.Encrypt = test;
            Assert.IsNotNull(SqlDb.Encrypt);
            Assert.AreEqual(SqlDb.Encrypt, testBool.ToString());
        }

        [TestMethod]
        public async Task TestTrustCertificateString()
        {
            string test = "TrustCertificate";
            bool testBool = true;
            SqlDb.TrustCertificate = test;
            Assert.IsNotNull(SqlDb.TrustCertificate);
            Assert.AreEqual(SqlDb.TrustCertificate, testBool.ToString());
        }

        [TestMethod]
        public async Task TestConnectionTimeoutString()
        {
            string test = "ConnectionTimeout";
            SqlDb.ConnectionTimeout = test;
            Assert.IsNotNull(SqlDb.ConnectionTimeout);
            Assert.AreEqual(SqlDb.ConnectionTimeout, int.MinValue.ToString());
        }

        #endregion Getters and Setters - Invalid Input

        #region Getters and Setters - Null Input

        [TestMethod]
        public async Task TestServerNull()
        {
            string test = null;
            SqlDb.Server = test;
            Assert.IsNotNull(SqlDb.Server);
            Assert.AreEqual(SqlDb.Server, string.Empty);
        }

        [TestMethod]
        public async Task TestInitialCatalogNull()
        {
            string test = null;
            SqlDb.InitialCatalog = test;
            Assert.IsNotNull(SqlDb.InitialCatalog);
            Assert.AreEqual(SqlDb.InitialCatalog, string.Empty);
        }

        [TestMethod]
        public async Task TestPersistSecurityInfoNull()
        {
            string test = null;
            SqlDb.PersistSecurityInfo = test;
            Assert.IsNotNull(SqlDb.PersistSecurityInfo);
            Assert.AreEqual(SqlDb.PersistSecurityInfo, true.ToString());
        }

        [TestMethod]
        public async Task TestUserIdNull()
        {
            string test = null;
            SqlDb.UserId = test;
            Assert.IsNotNull(SqlDb.UserId);
            Assert.AreEqual(SqlDb.UserId, string.Empty);
        }

        [TestMethod]
        public async Task TestPasswordNull()
        {
            string test = null;
            SqlDb.Password = test;
            Assert.IsNotNull(SqlDb.Password);
            Assert.AreEqual(SqlDb.Password, string.Empty);
        }

        [TestMethod]
        public async Task TestMultipleActiveResultSetsNull()
        {
            string test = null;
            SqlDb.MultipleActiveResultSets = test;
            Assert.IsNotNull(SqlDb.MultipleActiveResultSets);
            Assert.AreEqual(SqlDb.MultipleActiveResultSets, true.ToString());
        }

        [TestMethod]
        public async Task TestEncryptNull()
        {
            string test = null;
            SqlDb.Encrypt = test;
            Assert.IsNotNull(SqlDb.Encrypt);
            Assert.AreEqual(SqlDb.Encrypt, true.ToString());
        }

        [TestMethod]
        public async Task TestTrustCertificateNull()
        {
            string test = null;
            SqlDb.TrustCertificate = test;
            Assert.IsNotNull(SqlDb.TrustCertificate);
            Assert.AreEqual(SqlDb.TrustCertificate, true.ToString());
        }

        [TestMethod]
        public async Task TestConnectionTimeoutNull()
        {
            string test = null;
            SqlDb.ConnectionTimeout = test;
            Assert.IsNotNull(SqlDb.ConnectionTimeout);
            Assert.AreEqual(SqlDb.ConnectionTimeout, int.MinValue.ToString());
        }

        [TestMethod]
        public async Task TestConnectionStringNull()
        {
            string test = null;
            SqlDb.ConnectionString = test;
            Assert.IsNotNull(SqlDb.ConnectionString);
            Assert.AreEqual(SqlDb.ConnectionString, string.Empty);
        }

        #endregion Getters and Setters - Null Input

        #region GetConnectionString

        [TestMethod]
        public async Task TestGetConnectionString()
        {
            TestServer();
            TestInitialCatalog();
            TestPersistSecurityInfoBool();
            TestUserId();
            TestPassword();
            TestMultipleActiveResultSetsBool();
            TestEncryptBool();
            TestTrustCertificateBool();
            TestConnectionTimeoutNumber();
            TestConnectionString();

            Assert.IsNotNull(SqlDb.GetConnectionString());
        }

        #endregion GetConnectionString
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
