using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace praticeApp.DataAccess
{
    class StudentLoader : BaseLoader

    {
        public StudentLoader() { }


        public String GetStudentNameWithToken(String token)
        {
       
            String studentName = null;
            try
            {
                string url = "https://beacon.aattendance.nl/api/v2/students";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream());
                string jsonResponse = responseString.ReadToEnd();
                
                JObject jobject = JObject.Parse(jsonResponse);
                var StudentArray = jobject["data"];
                var Student = StudentArray[0];
               
                return ((String)Student["id"]);

            }
            catch (Exception e)
            {
                Debug.WriteLine("HTTPWebRequest exception: " + e.ToString());
                return studentName;
            }
        }
    }
}