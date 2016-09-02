// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas
{
    using Controllers;
    using Models;
    using System.Configuration;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            AuthBot.Models.AuthSettings.Mode = ConfigurationManager.AppSettings["ActiveDirectory.Mode"];
            AuthBot.Models.AuthSettings.EndpointUrl = ConfigurationManager.AppSettings["ActiveDirectory.EndpointUrl"];
            AuthBot.Models.AuthSettings.Tenant = ConfigurationManager.AppSettings["ActiveDirectory.Tenant"];
            AuthBot.Models.AuthSettings.RedirectUrl = ConfigurationManager.AppSettings["ActiveDirectory.RedirectUrl"];
            AuthBot.Models.AuthSettings.ClientId = ConfigurationManager.AppSettings["ActiveDirectory.ClientId"];
            AuthBot.Models.AuthSettings.ClientSecret = ConfigurationManager.AppSettings["ActiveDirectory.ClientSecret"];
            AuthBot.Models.AuthSettings.Scopes = ConfigurationManager.AppSettings["ActiveDirectory.Scopes"].Split(',');
            
            SqlDb.Server = ConfigurationManager.AppSettings["Db.Server"];
            SqlDb.InitialCatalog = ConfigurationManager.AppSettings["Db.InitialCatalog"];
            SqlDb.PersistSecurityInfo = ConfigurationManager.AppSettings["Db.PersistSecurityInfo"];
            SqlDb.UserId = ConfigurationManager.AppSettings["Db.UserId"];
            SqlDb.Password = ConfigurationManager.AppSettings["Db.Password"];
            SqlDb.MultipleActiveResultSets = ConfigurationManager.AppSettings["Db.MultipleActiveResultSets"];
            SqlDb.Encrypt = ConfigurationManager.AppSettings["Db.Encrypt"];
            SqlDb.TrustCertificate = ConfigurationManager.AppSettings["Db.TrustServerCertificate"];
            SqlDb.ConnectionTimeout = ConfigurationManager.AppSettings["Db.ConnectionTimeout"];
            SqlDb.ConnectionString = ConfigurationManager.AppSettings["Db.ConnectionString"];

            LuisHelper.LuisModelId = ConfigurationManager.AppSettings["Luis.ModelId"];
            LuisHelper.LuisApiKey = ConfigurationManager.AppSettings["Luis.ApiKey"];

            Models.User.CORP_DOMAIN = ConfigurationManager.AppSettings["CorpDomain"];

            FinanceItem.SetProperties();
            SetTimeOptions();
        }

        private static void SetTimeOptions()
        {
            // TODO: not hardcode
            var timeframes = ConfigurationManager.AppSettings["TimeFrames"];

            foreach (string options in timeframes.Split(';'))
            {
                string key = options.Split(':')[0];
                string[] alternatives = options.Split(':')[1].Split(',');
                DataRetriever.TimeframeOptions.Add(key, alternatives);
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