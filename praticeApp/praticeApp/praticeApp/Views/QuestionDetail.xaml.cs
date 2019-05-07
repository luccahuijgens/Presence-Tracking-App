using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praticeApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionDetail : ContentPage
    {
        public QuestionDetail(Question question)
        {
            InitializeComponent();
            questionTitle.Text = question.Title;
            questionSubject.Text = question.Subject;
            questionTags.Text = "dummy tags";
        }
    }
}