using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace praticeApp.DataAccess
{
    class ConfigAcces
    {
        private String filename;

        public ConfigAcces()
        {
            this.filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "config.txt");
        }

        public String loadConfigTokenOutFile()
        {
           return File.ReadAllText(getfileName());
        }

        public void writeConfigTokenInFile(String token)
        {
            File.WriteAllText(getfileName(),token);
        }

        public String getfileName()
        {
            return filename;
        }
    }
}
