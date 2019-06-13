using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public class ConfigService
    {
        private ConfigAccess configLoader = new ConfigAccess();

        public String GetStudentToken()
        {
            String ret = new String(new char[] { });

            configLoader.LoadConfigTokenOutFile(ref ret);

            return ret;
        }

        public void WriteStudentToken(String token)
        {
            configLoader.WriteConfigTokenInFile(token);
        }

    }
}
