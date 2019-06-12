using Newtonsoft.Json.Linq;
using praticeApp.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace praticeApp.Views
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
            var animat = new Animation();
            scanPage.Animate("Qr", animat, 5, 100, null, null, null);
            await Navigation.PushAsync(scanPage);

            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                if (GetName(result.Text))
                {
                    ConfigService.writeStudentToken(result.Text);
                }
                Console.WriteLine("\n" + result.ToString() + "\n");

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(() =>
                {
                        Navigation.PopAsync();
                });
            };


        }
        private Boolean GetName(String studentToken)
        {
            string url = "https://beacon.aattendance.nl/api/v2/students";
            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + studentToken);
                try
                {
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {

                            string jsonResponse = sr.ReadToEnd();
                            JObject jobject = JObject.Parse(jsonResponse);
                            Console.WriteLine(jsonResponse);
                            return true;
                            //var StudentArray = jobject["object"];
                            // var Student = StudentArray[0];
                            //  NameLabel.Text = (String)Student["id"];
                            //NameLabel.Text = jsonResponse;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    return false;
                }


            }

            else
            {
                return false;
            }

        }
    }
}