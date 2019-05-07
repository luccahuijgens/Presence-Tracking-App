﻿using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Domain
{
    public class Question : FeedItem
    {
        public String Title { get; set; }
        public String QuestionType { get; set; }
        public List<String> Tags { get; set; } 
    }
}
