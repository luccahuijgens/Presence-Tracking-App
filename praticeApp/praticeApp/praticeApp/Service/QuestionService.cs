using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public static class QuestionService
    {
        private static QuestionLoader QuestionLoader = new QuestionLoader();

        public static List<Question> GetQuestions()
        {
            return QuestionLoader.getQuestions();
        }
    }
}
