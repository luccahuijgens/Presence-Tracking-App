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
        public ZXingScannerPage scannerPage;


        public Scan()
        {
            InitializeComponent();
            scannerPage = ScanController.BuildScannerpage();
            ScanAsync(scannerPage);
        }

        public async void ScanAsync(ZXingScannerPage scannerPage)
        {
            await Navigation.PushAsync(scannerPage);
            scannerPage.OnScanResult += (result) =>
            {
                if (ScanController.ProcessResult(result)){
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
        }
    }
}