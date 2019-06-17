using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public static class ConfigService
    {
        private static ConfigAccess configLoader = new ConfigAccess();

        public static String GetStudentToken()
        {
            String ret = new String(new char[] { });

            configLoader.LoadConfigTokenOutFile(ref ret);

            return ret;
        }

        public static void WriteStudentToken(String token)
        {
            configLoader.WriteConfigTokenInFile(token);
        }

    }
}
