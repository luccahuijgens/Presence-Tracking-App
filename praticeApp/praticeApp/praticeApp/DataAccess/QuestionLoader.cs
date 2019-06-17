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
            token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjY4ZWMwNjE1YWU2YzY3NmZlZDA2ZjRlNjBkMTJlN2Y0MzQ1ZmY4NTU4MTRkNDRiM2Q1ODNjOTQ1NGJiODc5Zjc2YmNiY2E1ZGY4MWFmMzYxIn0.eyJhdWQiOiIxIiwianRpIjoiNjhlYzA2MTVhZTZjNjc2ZmVkMDZmNGU2MGQxMmU3ZjQzNDVmZjg1NTgxNGQ0NGIzZDU4M2M5NDU0YmI4NzlmNzZiY2JjYTVkZjgxYWYzNjEiLCJpYXQiOjE1NjA0MzA2NzksIm5iZiI6MTU2MDQzMDY3OSwiZXhwIjoxNTkyMDUzMDc5LCJzdWIiOiI5Iiwic2NvcGVzIjpbXX0.yV46vydGw9lizwDvRWLI__cva_iKaJbXr5YO7SPulEFJ3P2I6Ml4Eybdxcz-AdUryduViGAPs3b1RicWrgG9Qwak-6yhPc7vEkK9V-ClLZkFsRNOdftMpDyXAsJPrFH7y-SNyHlZcZfqruHRECl5ODl-7-M-a_9Oj6JHibml0Xe-d1VFInCMxjXx-yGBfJfHTLDyu-9f04r16XIxwBvZZcOcUd1z59KNQW6s0Tzi0Q1G1_PR9Jq-dhDc7adunVE0rJbs2CFt0ip8x7nzl4hYZmCJ1wCqhywdIwKChGkYAbpcS4zrriHHuQZ1tY-y5F4RIkLcRT4-D9jb2GhBu-EFxrzmGD-Tm5a0sPzgaulPVuQvtfqzYCI4CmFX-rCc5FTP6DD9grRUs3fGFT85X7KD6oOJ2hsko1VRsemfmcDAElNcWGhJCfwCHA1NtAsHbwaO-UlvBLEXeW8RRD7MTO55olHxZK_NgrY6wY4wvU1uvXju685J5ZA6bsqCQ4jjPjHxsEm4hTYcg8gaBNHUqLpsTL_4GVG2RQKARMEZ-yGAOrJl0jG2easEdnlLslNtcsp2pe050El_JgIu8aceVFXMUk0jmBUOumVffYkvjq0mdplAhBBzgx8NILuelZOlsNYzc3f73Ql3MwU5mh908FP4ohQT-vTHxruYCiUOlYxhu-E";
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
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}