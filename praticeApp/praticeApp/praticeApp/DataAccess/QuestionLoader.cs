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

        private Question convertJson(JObject question, String eventName, String eventDate, Dictionary<String, int> possibleAnswers)
        {
            int ID = (int)question["id"]; ;
            string Subject = eventName;
            DateTime Date = DateTime.Parse(eventDate);
            string Title = (String)question["attributes"]["question"];
            string QuestionType = (String)question["attributes"]["question_type"];
            return new Question { FeedItemType = "Question", ID = ID, PossibleAnswers = possibleAnswers, Subject = Subject, Title = Title, QuestionType = QuestionType, Header = Title, Date = Date };
        }

        private String getDateStringFromEvent(JArray eventData, int eventId)
        {
            foreach (JObject Event in eventData.Children())
            {
                if ((int)Event["id"] == eventId)
                {
                    return (String)Event["attributes"]["starttime"];
                }
            }
            return null;
        }

        private String getNameFromEvent(JArray eventData, int eventId)
        {
            foreach (JObject Event in eventData.Children())
            {
                if ((int)Event["id"] == eventId)
                {
                    return (String)Event["attributes"]["name"];
                }
            }
            return "";
        }

        private Dictionary<String, int> getAnswersFromQuestions(JArray answers)
        {
            Dictionary<String, int> possibleAnswers = new Dictionary<String, int>();
            foreach (JObject Answer in answers)
            {
                possibleAnswers.Add((String)Answer["answer"], (int)Answer["id"]);
            }
            return possibleAnswers;
        }

        public List<Question> GetQuestions(String token)
        {
            List<Question> QuestionList = new List<Question>();
            string url = "https://beacon.aattendance.nl/api/v2/event-questions/";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream());
            string jsonResponse = responseString.ReadToEnd();
            JObject Questionobject = JObject.Parse(jsonResponse);
            JArray eventData = (JArray)Questionobject["included"];
            foreach (JObject Question in Questionobject["data"].Children())
            {
                int eventId = (int)Question["relationships"]["event"]["data"]["id"];
                String eventDate = getDateStringFromEvent(eventData, eventId);
                String eventName = getNameFromEvent(eventData, eventId);
                Dictionary<String, int> possibleAnswers = getAnswersFromQuestions((JArray)Question["attributes"]["answer_possibilities"]);
                Question notobject = convertJson(Question, eventName, eventDate, possibleAnswers);
                QuestionList.Add(notobject);
            }
            return QuestionList;
        }

        public bool SubmitQuestion(int questionId, int answerId, string token)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://beacon.aattendance.nl/api/v2/event-questions/" + questionId);


                request.Method = "POST";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"answer_possibility_id\":"+answerId+"}";

                    streamWriter.Write(json);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}