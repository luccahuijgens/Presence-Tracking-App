using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace praticeApp
{
    public partial class MainPage : ContentPage
    {
        private string myName;
        public string MyName
        {
            get { return myName; }
            set
            {
                myName = value;
                OnPropertyChanged(nameof(MyName)); // Notify that there was a change on this property
            }
        }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            SubmitButton.Clicked += getName;
        }
           private void getName(object sender, EventArgs e) {
            string url= "https://training.aattendance.nl/api/v0/studentsearch/?search="+StudentID.Text;
            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("X-API-KEY", "459KrmhgSItMD0xBPX2KnThfsjUQXEMsh44P6YVu");

                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        string jsonResponse = sr.ReadToEnd();
                        JObject jobject = JObject.Parse(jsonResponse);
                        var StudentArray = jobject["object"];
                        var Student = StudentArray[0];
                        NameLabel.Text = (String)Student["student_name"];
                        //NameLabel.Text = jsonResponse;
                    }
                }
            }
        }
        }
    
}
