using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        public bool GetStartTime(ref DateTime dt)
        {
            CultureInfo nlNL = new CultureInfo("nl-NL");

            return DateTime.TryParseExact(starttime, "yyyy-MM-dd HH:mm:ss", nlNL, DateTimeStyles.None, out dt);
        }

        public bool GetEndTime(ref DateTime dt)
        {
            CultureInfo nlNL = new CultureInfo("nl-NL");

            return DateTime.TryParseExact(endtime, "yyyy-MM-dd HH:mm:ss", nlNL, DateTimeStyles.None, out dt);
        }
    }

    class EventsData
    {
        public String type { get; set; }
        public String id { get; set; }

        public EventAttributes attributes { get; set; }
    }

    class Events
    {
        public IList<EventsData> data { get; set; }
    }
}
