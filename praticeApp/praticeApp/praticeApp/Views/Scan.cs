using Newtonsoft.Json.Linq;
using praticeApp.Controller;
using praticeApp.Service;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace praticeApp.Views
{
    public partial class Scan : ZXingScannerPage
    {

        public Scan()
        {
            var animat = new Animation();
            this.Animate("Qr", animat, 5, 100, null, null, null);
            IsScanning = true;
            ScanAsync();
        }

        public void ScanAsync()
        {
            OnScanResult += (result) =>
            {


                if (ScanController.ProcessResult(result.Text))
                {

                    Debug.WriteLine("\nScans Succeed...\n");
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Succes~!", "Je account is succesvol gekoppeld", "Begrepen.");
                    });

                    App.Current.MainPage = new NavigationPage(new NavMaster());


                }
                else
                {

                    Debug.WriteLine("\nScanfailded...\n");
                }

            };


        }


    }
}

