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

        private static string _server = string.Empty;
        private static string _initialCatalog = string.Empty;
        private static bool _persistSecurityInfo = false;
        private static string _userId = string.Empty;
        private static string _password = string.Empty;
        private static bool _multipleActiveResultSets = false;
        private static bool _encrypt = true;
        private static bool _trustCertificate = false;
        private static int _connectionTimeout = 30;
        private static string _connectionString = string.Empty;

        public static string Server
        {
            get
            {
                return _server == null ? "" : _server;
            }
            set
            {
                _server = value == null ? "" : value;
            }
        }

        public static string InitialCatalog
        {
            get
            {
                return _initialCatalog == null ? "" : _initialCatalog;
            }
            set
            {
                _initialCatalog = value == null ? "" : value;
            }
        }

        public static string PersistSecurityInfo
        {
            get
            {
                return _persistSecurityInfo.ToString() == null ? "" : _persistSecurityInfo.ToString();
            }
            set
            {
                _persistSecurityInfo = value == null ? false : Convert.ToBoolean(value);
            }
        }

        public static string UserId
        {
            get
            {
                return _userId == null ? "" : _userId;
            }
            set
            {
                _userId = value == null ? "" : value;
            }
        }

        public static string Password
        {
            get
            {
                return _password == null ? "" : _password;
            }
            set
            {
                _password = value == null ? "" : value;
            }
        }

        public static string MultipleActiveResultSets
        {
            get
            {
                return Convert.ToString(_multipleActiveResultSets) == null ? "" : Convert.ToString(_multipleActiveResultSets);
            }
            set
            {
                _multipleActiveResultSets = value == null ? false : Convert.ToBoolean(value);
            }
        }

        public static string Encrypt
        {
            get
            {
                return Convert.ToString(_encrypt) == null ? "" : Convert.ToString(_encrypt);
            }
            set
            {
                _encrypt = value == null ? true : Convert.ToBoolean(value);
            }
        }

        public static string TrustCertificate
        {
            get
            {
                return Convert.ToString(_trustCertificate) == null ? "" : Convert.ToString(_trustCertificate);
            }
            set
            {
                _trustCertificate = value == null ? false : Convert.ToBoolean(value);
            }
        }

        public static string ConnectionTimeout
        {
            get
            {
                return Convert.ToString(_connectionTimeout) == null ? "" : Convert.ToString(_connectionTimeout);
            }
            set
            {
                _connectionTimeout = value == null ? 30 : Convert.ToInt32(value);
            }
        }

        public static string ConnectionString
        {
            get
            {
                return _connectionString == null ? "" : _connectionString;
            }
            set
            {
                _connectionString = value == null ? "" : value;
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
