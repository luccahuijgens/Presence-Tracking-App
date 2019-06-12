using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public class QuestionService:IQuestionService
    {
        private IQuestionLoader QuestionLoader = new QuestionLoader();

        public List<Question> GetQuestions()
        {
            return QuestionLoader.GetQuestions();
        }
    }
}
