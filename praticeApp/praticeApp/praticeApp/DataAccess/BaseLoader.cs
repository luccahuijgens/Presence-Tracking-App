using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.DataAccess
{
    public abstract class BaseLoader
    {
        public BaseLoader()
        {

        }
        protected System.Net.WebResponse GetConnection(string url)
        {
            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("X-API-KEY", "459KrmhgSItMD0xBPX2KnThfsjUQXEMsh44P6YVu");
                return webRequest.GetResponse();
            }
            else { return null; }
        }
    }
}
