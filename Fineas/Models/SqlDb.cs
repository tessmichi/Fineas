// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Models
{
    using System;

    [Serializable]
    public static class SqlDb
    {
        // Everything is pretty obfuscated here.
        // private variables help obscure private static information which is stored in the proper format.
        // public variables represent everything as a string so the code using these objects is simpler.

        private static string _server;
        private static string _initialCatalog;
        private static bool _persistSecurityInfo;
        private static string _userId;
        private static string _password;
        private static bool _multipleActiveResultSets;
        private static bool _encrypt;
        private static bool _trustCertificate;
        private static int _connectionTimeout;
        private static string _connectionString;

        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public static string InitialCatalog
        {
            get
            {
                return _initialCatalog;
            }
            set
            {
                _initialCatalog = value;
            }
        }

        public static string PersistSecurityInfo
        {
            get
            {
                return _persistSecurityInfo.ToString();
            }
            set
            {
                _persistSecurityInfo = Convert.ToBoolean(value);
            }
        }

        public static string UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        public static string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public static string MultipleActiveResultSets
        {
            get
            {
                return Convert.ToString(_multipleActiveResultSets);
            }
            set
            {
                _multipleActiveResultSets = Convert.ToBoolean(value);
            }
        }

        public static string Encrypt
        {
            get
            {
                return Convert.ToString(_encrypt);
            }
            set
            {
                _encrypt = Convert.ToBoolean(value);
            }
        }

        public static string TrustCertificate
        {
            get
            {
                return Convert.ToString(_trustCertificate);
            }
            set
            {
                _trustCertificate = Convert.ToBoolean(value);
            }
        }

        public static string ConnectionTimeout
        {
            get
            {
                return Convert.ToString(_connectionTimeout);
            }
            set
            {
                _connectionTimeout = Convert.ToInt32(value);
            }
        }

        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public static string GetConnectionString()
        {
            return string.Format(ConnectionString, Server, InitialCatalog, PersistSecurityInfo, UserId, Password, MultipleActiveResultSets, Encrypt, TrustCertificate, ConnectionTimeout);
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
