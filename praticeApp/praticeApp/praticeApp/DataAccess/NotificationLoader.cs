using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                var httpClient = new HttpClient();
                string url = "https://beacon.aattendance.nl/api/v2/notifications";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (response.StatusCode != HttpStatusCode.OK)
                    return NotificationList;

                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Converting");
                JObject Notificationobject = JObject.Parse(jsonResponse);
                foreach (JObject Notification in Notificationobject["data"].Children())
                {
                    Notification notobject = convertJson(Notification);
                    NotificationList.Add(notobject);
                }
            }catch(Exception e)
            {
                Debug.WriteLine("Notifications failed.");
                Debug.WriteLine(e.ToString());
            }
                return NotificationList;
            }
    }
}
