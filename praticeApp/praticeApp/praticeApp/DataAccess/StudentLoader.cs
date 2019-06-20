using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.DataAccess
{
    class StudentLoader : BaseLoader

    {
        public StudentLoader() { }


        public String GetStudentNameWithToken(String token)
        {
            String studentName = null;
            string url = "https://beacon.aattendance.nl/api/v2/students";
            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + token);
                try
                {
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {

                            string jsonResponse = sr.ReadToEnd();
                            JObject jobject = JObject.Parse(jsonResponse);
                            var StudentArray = jobject["object"];
                            var Student = StudentArray[0];
                            return((String)Student["name"]);
          
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return studentName;
                }
            }

            else
            {
                return studentName;
            }

        }

    }
}
