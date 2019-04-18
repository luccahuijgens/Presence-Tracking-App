using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using praticeApp.Domain;

namespace praticeApp.DataAccess
{
    public class QuestionLoader : BaseLoader
    {
        public QuestionLoader() { }

        private Question convertJson(JObject question)
        {
            int ID = (int)question["id"];
            DateTime Date = DateTime.Parse((String)question["date"]);
            string Sender = (String)question["sender"];
            string Subject = (String)question["subject"];
            string Title = (String)question["title"];
            string Body = (String)question["body"];
            string QuestionType = (String)question["questionType"];
            return new Question { FeedType = "Question", ID = ID, Date = Date, Sender = Sender, Subject = Subject, Title = Title, Body = Body, QuestionType=QuestionType };
        }
        public List<Question> getQuestions()
        {
            List<Question> QuestionList = new List<Question>();
            string url = "http://5ca5f65f3a08260014278eaf.mockapi.io/questions";


                using (System.IO.Stream s = GetConnection(url).GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        string jsonResponse = sr.ReadToEnd();
                        JArray Questionarray = JArray.Parse(jsonResponse);
                        foreach (JObject Question in Questionarray.Children<JObject>())
                        {
                        Question notobject = convertJson(Question);
                            QuestionList.Add(notobject);
                        }
                    }
                }
            return QuestionList;
        }
    }
}
