
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.WebUI.HtmlHelpers
{
    public class AppSettings
    {
        public string getAppSetting(string key)
        {
            string appSettingKeyValue = System.Configuration.ConfigurationManager.AppSettings[key];
            return appSettingKeyValue;
        }
    }
}