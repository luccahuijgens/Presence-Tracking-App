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
    public partial class Scan : ContentPage
    {
      

        public Scan()
        {
            InitializeComponent();
            ScanAsync();
        }

        public async void ScanAsync()
        {
            ZXingScannerPage scannerPage = ScanController.BuildScannerpage();
            scannerPage.IsScanning = true;
            scannerPage.OnScanResult += (result) =>
            {
         

                if (ScanController.ProcessResult(result.Text)){
                    scannerPage.IsScanning = false;
                    Debug.WriteLine("\nScans Succeed...\n");
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopAsync();
                    });

            }
                else
                {
                    Debug.WriteLine("\nScanfailded...\n");
                }

            };

            await Navigation.PushAsync(scannerPage);
        }
    }
}