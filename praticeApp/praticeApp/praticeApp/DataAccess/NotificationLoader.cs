using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json.Linq;
using praticeApp.Domain;

namespace praticeApp.DataAccess
{
    public class NotificationLoader : BaseLoader, INotificationLoader
    {
        public NotificationLoader() { }

        private Notification convertJson(JObject notification)
        {
            int ID = (int)notification["id"];
            DateTime Date = DateTime.Parse((String)notification["attributes"]["date"]);
            string Subject = (String)notification["attributes"]["subject"];
            string Content = (String)notification["attributes"]["content"];
            return new Notification { FeedItemType = "Notification", ID = ID, Date = Date, Subject = Subject, Content = Content, Header = Content };
        }

        public List<Notification> GetNotifications(string studentToken)
        {
            List<Notification> NotificationList = new List<Notification>();
            //string url = "https://beacon.aattendance.nl/api/v2/students";
            string url = "https://5cd16a84d4a78300147bea4c.mockapi.io/notifications";
            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + studentToken);
                try
                {

                    using (System.IO.Stream s = GetConnection(url).GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            string jsonResponse = sr.ReadToEnd();
                            JArray notificationarray = JArray.Parse(jsonResponse);
                            foreach (JObject notification in notificationarray.Children<JObject>())
                            {
                                Notification notobject = convertJson(notification);
                                NotificationList.Add(notobject);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
                return NotificationList;
        }
    }
}
