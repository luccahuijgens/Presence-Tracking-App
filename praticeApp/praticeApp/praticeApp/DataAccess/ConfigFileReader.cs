using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.DataAccess
{
    public static class ConfigFileReader
    {
        private static ConfigLoader loader = new ConfigLoader();

        public static ConfigLoader GetConfigFileLoader()
        {
            return loader;
        }

        public static String GetToken()
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            return((String)jobject["token"]);
        }

        public static String GetScanState()
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            return ((String)jobject["scanState"]);
        }

        public static String GetScanTime()
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            return ((String)jobject["scanTime"]);
        }

        public static String GetMaxQRScans()
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            return ((String)jobject["maxQRScans"]);
        }

        public static Boolean setToken(String newToken)
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            jobject["token"] = newToken;
            return loader.UpdateConfigFile(jobject.ToString());
        }

        public static Boolean setScanState(Boolean newScanState)
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            jobject["scanState"] = newScanState;
            return (loader.UpdateConfigFile(jobject.ToString()));
        }

        public static Boolean setScanTime(String newScanTime)
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            jobject["scanTime"] = newScanTime;
            return(loader.UpdateConfigFile(jobject.ToString()));
        }

        public static Boolean setMaxQRScans(String newMaxQRScans)
        {
            JObject jobject = JObject.Parse(GetConfigFileLoader().ReadConfigFile());
            jobject["scanTime"] = newMaxQRScans;
            return (loader.UpdateConfigFile(jobject.ToString()));
        }
    }
}
