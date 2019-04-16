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

        private Question convertJson(JObject Question)
        {
            int ID = (int)Question["id"];
            string Title = (String)Question["title"];
            string Body = (String)Question["body"];
            return new Question { ID = ID, Title = Title, Body = Body };
        }
        public List<Question> getQuestions()
        {
            List<Question> QuestionList = new List<Question>();
            string url = "http://5ca5f65f3a08260014278eaf.mockapi.io/Questions";


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
