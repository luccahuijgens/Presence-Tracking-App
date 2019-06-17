using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praticeApp.Service;
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
            this.question = question;
            questionTitle.Text = question.Title;
            questionSubject.Text = question.Subject;
            questionTags.Text = "dummy tags";
        }
        private void SubmitBooleanButton(object sender, EventArgs args)
        {
            if(answer.IsToggled)
            {
                ServiceProvider.GetQuestionService().submitQuestion(question.ID,1);
            }
            else {
                ServiceProvider.GetQuestionService().submitQuestion(question.ID, 2);
            }
            
        }
    }
}