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
            DateTime Date = DateTime.Parse((String)notification["attributes"]["date"]);
            string Subject = (String)notification["attributes"]["subject"];
            string Content = (String)notification["attributes"]["content"];
            return new Notification {FeedItemType="Notification",ID = ID, Date = Date, Subject = Subject,Content=Content,Header=Content};
        }
        public List<Notification> getNotifications()
        {
            List<Notification> NotificationList = new List<Notification>();
            string url = "https://5cd16a84d4a78300147bea4c.mockapi.io/notifications";


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
