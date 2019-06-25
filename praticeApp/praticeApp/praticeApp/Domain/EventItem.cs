using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    public class EventItem
    {
        public String Teachers { get; set; }
        public String LessonName { get; set; }

        public String LessonHasAttended { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public String TextColor { get; set; }

        public String BackgroundColor { get; set; }
    }
}
