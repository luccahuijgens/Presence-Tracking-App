using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
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

        public List<Notification> GetNotifications(String token)
        {
            List<Notification> NotificationList = new List<Notification>();
            string url = "https://beacon.aattendance.nl/api/v2/notifications";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            //request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode != HttpStatusCode.OK)
                    return NotificationList;

                var responseString = new StreamReader(response.GetResponseStream());
                string jsonResponse = responseString.ReadToEnd();
                JObject Notificationobject = JObject.Parse(jsonResponse);
                foreach (JObject Notification in Notificationobject["data"].Children())
                {
                    Notification notobject = convertJson(Notification);
                    NotificationList.Add(notobject);
                }
                return NotificationList;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());

                return NotificationList;
            }

            

            

            
        }
    }
}
