using praticeApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace praticeApp.DataAccess
{
    interface IQuestionLoader
    {
        List<Question> GetQuestions();
    }
}
