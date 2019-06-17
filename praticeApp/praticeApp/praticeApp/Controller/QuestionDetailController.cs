using praticeApp.Domain;
using praticeApp.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace praticeApp.Controller
{
    class QuestionDetailController
    {
        public static void FillQuestionDetailPage(Question question, Label questionTitle, Label questionSubject, Label questionTags)
        {
            questionTitle.Text = question.Title;
            questionSubject.Text = question.Subject;
            questionTags.Text = "dummy tags";
        }
        public static bool SubmitQuestion(Question question,bool isAnswerYes)
        {
            string token = ServiceProvider.GetConfigService().GetStudentToken();
            bool submitSuccess=false;
            if (isAnswerYes)
            {
                submitSuccess=ServiceProvider.GetQuestionService().SubmitQuestion(question.ID, 1,token);
            }
            else
            {
                submitSuccess=ServiceProvider.GetQuestionService().SubmitQuestion(question.ID, 2,token);
            }
            return submitSuccess;

        }


    }
}
