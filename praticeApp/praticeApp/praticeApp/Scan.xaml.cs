using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace praticeApp
{
    public partial class Scan : ContentPage
    {
        public Scan()
        {
            InitializeComponent();
        }
        public void OnClickScan(object sender, EventArgs e)
        {
            ScanAsync();
        }
        public async void ScanAsync()
        {

            var scanPage = new ZXingScannerPage();


            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    getName(result.Text);
                });
            };

            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);

        }
        private void getName(String StudentID)
        {
            string url = "https://training.aattendance.nl/api/v0/studentsearch/?search=" + StudentID;
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