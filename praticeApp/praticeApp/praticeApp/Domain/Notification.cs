using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    public class Notification : FeedItem
    {
        public DateTime Date { get; set; }
        public String Content { get; set; }

        public string dateToString()
        {
            if (DateTime.Now.ToString("MM/dd/yyyy").Equals(Date.ToString("MM/dd/yyyy")))
            {
                return Date.ToString("HH:mm");
            }
            else if (DateTime.Today - Date.Date == TimeSpan.FromDays(1))
            {
                return "Yesterday";
            }
            else if (DateTime.Today.Year != Date.Year)
            {
                return Date.ToString("dd/MM/yyyy");
            }
            else
            {
                return Date.ToString("dd MMM");
            }
        }
    }
}
