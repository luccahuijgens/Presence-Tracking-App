using praticeApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.Service
{
   public interface IQuestionService
    {
        List<Question> GetQuestions();
    }
}
