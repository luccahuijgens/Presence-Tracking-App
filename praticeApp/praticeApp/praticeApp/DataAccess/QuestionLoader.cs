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
            int ID = (int)question["id"];;
            string Subject = (String)question["subject"];
            string Title = (String)question["title"];
            string QuestionType = (String)question["type"];
            List<String> Tags = new List<string>();
            return new Question {FeedItemType="Question", ID = ID, Tags=Tags, Subject = Subject, Title = Title, QuestionType=QuestionType,Header=Title };
        }
        public List<Question> getQuestions()
        {
            List<Question> QuestionList = new List<Question>();
            string url = "https://5cd16a84d4a78300147bea4c.mockapi.io/questions";


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
