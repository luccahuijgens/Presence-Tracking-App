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
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6IjY4ZWMwNjE1YWU2YzY3NmZlZDA2ZjRlNjBkMTJlN2Y0MzQ1ZmY4NTU4MTRkNDRiM2Q1ODNjOTQ1NGJiODc5Zjc2YmNiY2E1ZGY4MWFmMzYxIn0.eyJhdWQiOiIxIiwianRpIjoiNjhlYzA2MTVhZTZjNjc2ZmVkMDZmNGU2MGQxMmU3ZjQzNDVmZjg1NTgxNGQ0NGIzZDU4M2M5NDU0YmI4NzlmNzZiY2JjYTVkZjgxYWYzNjEiLCJpYXQiOjE1NjA0MzA2NzksIm5iZiI6MTU2MDQzMDY3OSwiZXhwIjoxNTkyMDUzMDc5LCJzdWIiOiI5Iiwic2NvcGVzIjpbXX0.yV46vydGw9lizwDvRWLI__cva_iKaJbXr5YO7SPulEFJ3P2I6Ml4Eybdxcz-AdUryduViGAPs3b1RicWrgG9Qwak-6yhPc7vEkK9V-ClLZkFsRNOdftMpDyXAsJPrFH7y-SNyHlZcZfqruHRECl5ODl-7-M-a_9Oj6JHibml0Xe-d1VFInCMxjXx-yGBfJfHTLDyu-9f04r16XIxwBvZZcOcUd1z59KNQW6s0Tzi0Q1G1_PR9Jq-dhDc7adunVE0rJbs2CFt0ip8x7nzl4hYZmCJ1wCqhywdIwKChGkYAbpcS4zrriHHuQZ1tY-y5F4RIkLcRT4-D9jb2GhBu-EFxrzmGD-Tm5a0sPzgaulPVuQvtfqzYCI4CmFX-rCc5FTP6DD9grRUs3fGFT85X7KD6oOJ2hsko1VRsemfmcDAElNcWGhJCfwCHA1NtAsHbwaO-UlvBLEXeW8RRD7MTO55olHxZK_NgrY6wY4wvU1uvXju685J5ZA6bsqCQ4jjPjHxsEm4hTYcg8gaBNHUqLpsTL_4GVG2RQKARMEZ-yGAOrJl0jG2easEdnlLslNtcsp2pe050El_JgIu8aceVFXMUk0jmBUOumVffYkvjq0mdplAhBBzgx8NILuelZOlsNYzc3f73Ql3MwU5mh908FP4ohQT-vTHxruYCiUOlYxhu-E";

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