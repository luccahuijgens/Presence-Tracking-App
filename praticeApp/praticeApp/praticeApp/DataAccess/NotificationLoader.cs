using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using praticeApp.Domain;

namespace praticeApp.DataAccess
{
    public class NotificationLoader : BaseLoader
    {
        public NotificationLoader() { }

        private Notification convertJson(JObject notification)
        {
            int ID = (int)notification["id"];
            DateTime Date = DateTime.Parse((String)notification["date"]);
            string Sender = (String)notification["sender"];
            string Subject = (String)notification["subject"];
            string Title = (String)notification["title"];
            string Body = (String)notification["body"];
            return new Notification { FeedType="Notification" ,ID = ID, Date = Date, Sender = Sender, Subject = Subject, Title = Title, Body = Body };
        }
        public List<Notification> getNotifications()
        {
            List<Notification> NotificationList = new List<Notification>();
            string url = "http://5ca5f65f3a08260014278eaf.mockapi.io/notifications";


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
            return NotificationList;
        }
    }
}
