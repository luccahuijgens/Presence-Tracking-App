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

        public List<Question> GetQuestions()
        {
            String token = ServiceProvider.GetConfigService().GetStudentToken();
            return QuestionLoader.GetQuestions(token);
        }

        public bool SubmitQuestion(int questionId,int answerId)
        {
            String token = ServiceProvider.GetConfigService().GetStudentToken();
            return true;
        }
    }
}
