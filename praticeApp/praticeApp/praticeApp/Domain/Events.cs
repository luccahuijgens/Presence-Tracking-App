using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    class EventAttributes
    {
        public String name { get; set; }
        public String starttime { get; set; }
        public String endtime { get; set; }
        public String classrooms { get; set; }
        public String lecturers { get; set; }
        public bool attended { get; set; }
    }

    class EventsData
    {
        public String type { get; set; }
        public String id { get; set; }

        public EventAttributes attributes;
    }

    class Events
    {
        public IList<EventsData> data { get; set; }
    }
}
