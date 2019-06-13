using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public static class ConfigService
    {
        private static ConfigAcces configloader = new ConfigAcces();

        public static String getStudentToken()
        {
            return configloader.loadConfigTokenOutFile();
        }

        public static void writeStudentToken(String token)
        {
            configloader.writeConfigTokenInFile(token);
        }

    }
}
