using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace praticeApp.DataAccess
{
    public class ConfigLoader
    {
        private String filename;

        public ConfigLoader()
        {
  
            this.filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "config.txt");
            if (!CheckConfigFile())
            {
                ResetToDefault();
            }
        }
        public String ReadConfigFile()
        {
            return (File.ReadAllText(GetConfigFilePathName()));
        }

        public String GetConfigFilePathName()
        {
            return filename;
        }

        public Boolean UpdateConfigFile(String newJSONBody)
        {
            try
            {
                File.WriteAllText(GetConfigFilePathName(), newJSONBody);
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ConfigFile update action failed with: " + e.Message);
                return false;
            }
        }

        public Boolean DeleteConfigFile()
        {
            try
            {
                File.Delete(GetConfigFilePathName());
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ConfigFile delete action failed with: " + e.Message);
                return false;
            }
        }

        public Boolean ResetToDefault()
        {
            JObject defaultConfigFile = new JObject();
            defaultConfigFile.Add("token", "");
            defaultConfigFile.Add("scanState", "false");
            defaultConfigFile.Add("scanTime", "");
            defaultConfigFile.Add("maxQRScans", "");
            return (UpdateConfigFile(defaultConfigFile.ToString()));
        }

        public Boolean CheckConfigFile()
        {
            try
            {
                if (File.Exists(GetConfigFilePathName()))
                {
                    Debug.WriteLine("Found ConfigFile....");
                    if (new FileInfo(GetConfigFilePathName()).Length == 0)
                    {

                        Debug.WriteLine("Empty ConfigFile.");
                        DeleteConfigFile(); //For fixing a bug with old versions of the configloader.
                        return false;
                    }
                    else
                    {
                        JObject jobject = JObject.Parse(ReadConfigFile()); //Test parse to ensure that the file is not corrupted
                        Debug.WriteLine("ConfigFile valid!");
                        return true;
                    }
                }
                else
                {
                    Debug.WriteLine("No ConfigFile found.");
                    return false;
                }

            }
            catch
            {
                Debug.WriteLine("ConfigFile Corrupted!");
                return false;
            }
        }
    }
}

    
