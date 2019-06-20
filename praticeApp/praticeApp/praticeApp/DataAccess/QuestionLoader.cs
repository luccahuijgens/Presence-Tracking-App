using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using praticeApp.Domain;

namespace praticeApp.DataAccess
{
    public class QuestionLoader : BaseLoader, IQuestionLoader
    {
        public QuestionLoader() { }

        private Question convertJson(JObject question)
        {
            int ID = (int)question["id"]; ;
            string Subject = (String)question["attributes"]["subject"];
            DateTime Date = DateTime.Parse((String)question["attributes"]["date"]);
            string Title = (String)question["title"];
            string QuestionType = (String)question["type"];
            List<String> Tags = new List<string>();
            return new Question { FeedItemType = "Question", ID = ID, Tags = Tags, Subject = Subject, Title = Title, QuestionType = QuestionType, Header = Title, Date = Date };
        }

        public List<Question> GetQuestions(String token)
        {
            List<Question> QuestionList = new List<Question>();
            //string url = "https://beacon.aattendance.nl/api/v2/event-questions/";
            string url = "https://5cd16a84d4a78300147bea4c.mockapi.io/questions";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream());
            string jsonResponse = responseString.ReadToEnd();
                        JArray Questionarray = JArray.Parse(jsonResponse);
                        foreach (JObject Question in Questionarray.Children<JObject>())
                        {
                        Question notobject = convertJson(Question);
                            QuestionList.Add(notobject);
                        }
            return QuestionList;
        }

        public bool SubmitQuestion(int questionId, int answerId, string token)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://beacon.aattendance.nl/api/v2/event-questions/" + questionId);

                var postData = "answer_possibility_id=" + answerId;
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}
