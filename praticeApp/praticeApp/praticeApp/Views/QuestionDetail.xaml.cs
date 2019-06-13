using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praticeApp.Controller;
using praticeApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionDetail : ContentPage
    {
        private Question question;
        public QuestionDetail(Question question)
        {
            InitializeComponent();
            QuestionDetailController.FillQuestionDetailPage(question, questionTitle, questionSubject, questionTags);
            this.question = question;
        }

        public void SubmitBooleanButton(object sender, EventArgs args)
        {
            if (QuestionDetailController.SubmitQuestion(question, answer.IsToggled))
            {
                DisplayAlert("Success","Your answer has been succesfully submitted.","Awesome!");
            }

        }
    }
}