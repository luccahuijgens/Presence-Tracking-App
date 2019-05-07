using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    public abstract class FeedItem
    {
        public int ID { get; set; }
        public String Subject { get; set; }
        public String FeedItemType { get; set; }
        public String Header { get; set; }
    }
}
