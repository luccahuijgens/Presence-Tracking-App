using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            int ID = (int)question["id"];;
            string Subject = (String)question["attributes"]["subject"];
            DateTime Date = DateTime.Parse((String)question["attributes"]["date"]);
            string Title = (String)question["title"];
            string QuestionType = (String)question["type"];
            List<String> Tags = new List<string>();
            return new Question { FeedItemType = "Question", ID = ID, Tags = Tags, Subject = Subject, Title = Title, QuestionType = QuestionType, Header = Title, Date = Date };
        }

        public async System.Threading.Tasks.Task<bool> submitQuestion(int questionId,int answerId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        { "answer_possibility_id", answerId.ToString() }
                    };

                    var content = new FormUrlEncodedContent(values);

                    var response = await client.PostAsync("https://beacon.aattendance.nl/api/v2/event-questions/"+questionId, content);

                    var responseString = await response.Content.ReadAsStringAsync();
                    return true;
                }
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        public List<Question> GetQuestions()
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
