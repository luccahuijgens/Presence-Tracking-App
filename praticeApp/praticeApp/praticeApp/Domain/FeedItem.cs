using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    public abstract class FeedItem
    {
        public int ID { get; set; }
        public String Title { get; set; }
        public String Body { get; set; }
        public String Subject { get; set; }
        public String Sender { get; set; }
        public DateTime Date { get; set; }
        public String FeedType { get; set; }
    }
}
