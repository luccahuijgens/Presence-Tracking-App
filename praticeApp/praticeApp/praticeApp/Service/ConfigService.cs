using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public class ConfigService
    { 

        public String GetStudentToken()
        {
            String ret = new String(new char[] { });
            ret = ConfigFileReader.GetToken();
            return ret;
        }

        public void WriteStudentToken(String newToken)
        {
            ConfigFileReader.setToken(newToken);
        }

        public String GetTrackingState()
        {
            return (ConfigFileReader.GetScanState());
        }

        public void setTrackingState(Boolean newState)
        {
            ConfigFileReader.setScanState(newState);
        }

        public void setTrackingTime(String time)
        {
            ConfigFileReader.setScanTime(time);
        }

        public String gettrackingTime()
        {
            return ConfigFileReader.GetScanTime();
        }

    }
}
