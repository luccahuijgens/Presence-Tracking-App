using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace praticeApp.DataAccess
{
    class ConfigLoader
    {
        private readonly String filename;

        public ConfigLoader()
        {
            this.filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "config.txt");
        }

        public bool LoadConfigTokenOutFile(ref String token)
        {
            if (File.Exists(GetFileName()))
            {
                token = File.ReadAllText(GetFileName());
                return true;
            }

            return false;
        }

        public void WriteConfigTokenInFile(String token)
        {
            File.WriteAllText(GetFileName(), token);
        }

        public String GetFileName()
        {
            return filename;
        }
    }
}
