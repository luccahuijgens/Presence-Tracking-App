using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public class QuestionService
    {
        private IQuestionLoader QuestionLoader = new QuestionLoader();

        public List<Question> GetQuestions(string token)
        {
            return QuestionLoader.GetQuestions(token);
        }

        public bool SubmitQuestion(int questionId,int answerId,string token)
        {
            return QuestionLoader.SubmitQuestion(questionId, answerId,token);
        }

        public bool submitQuestion(int questionId,int answerId, string token)
        {
            return QuestionLoader.SubmitQuestion(questionId, answerId, token);
        }
    }
}
