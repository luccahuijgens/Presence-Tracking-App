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
    }
}
