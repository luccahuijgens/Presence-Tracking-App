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
        public static void FillQuestionDetailPage(Question question, Label questionTitle, Label questionLesson, Label questionDate)
        {
            questionTitle.Text = question.Title;
            questionLesson.Text = question.Subject;
            questionDate.Text = question.dateToString();

        }
        public static bool SubmitQuestion(Question question, bool isAnswerYes)
        {
            string token = ServiceProvider.GetConfigService().GetStudentToken();
            bool submitSuccess = false;
            if (isAnswerYes)
            {
                KeyValuePair<string, int> pair = new KeyValuePair<string, int>("agree", question.PossibleAnswers["agree"]);
                submitSuccess = ServiceProvider.GetQuestionService().SubmitQuestion(question.ID, pair.Value, token);
            }
            else
            {
                KeyValuePair<string, int> pair = new KeyValuePair<string, int>("disagree", question.PossibleAnswers["disagree"]);
                submitSuccess = ServiceProvider.GetQuestionService().SubmitQuestion(question.ID, pair.Value, token);
            }
            return submitSuccess;

        }


    }
}