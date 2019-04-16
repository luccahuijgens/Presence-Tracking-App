using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    public class Notification : FeedItem
    {
        public String Subject { get; set; }
        public List<String> Tags{get;set;}
        public String Sender { get; set; }
        public DateTime Date { get; set; }

    }
}
