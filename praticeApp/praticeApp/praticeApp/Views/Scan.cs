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
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        public int scanTries;

        public Scan() : base()
        {
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            zxing.OnScanResult += (result) =>
            {
                while (scanTries <= 5)
                {
                    if (ScanController.ProcessResult(result.Text))
                    {
                        Debug.WriteLine("\nScan succeeded...\n");
                        break;
                    }
                    else
                    {
                        Debug.WriteLine("\nScan failed...\n");
                        scanTries = (scanTries + 1);
                        overlay.TopText = "Poging: " + scanTries;
                    }
                }

                if (scanTries <= 5)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Succes!", "Je account is succesvol gekoppeld!", "OK");
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert("Fout!", "Je account kan niet worden gekoppeld, controleer je internetverbinding of probeer het later opnieuw.", "OK");
                        });
                }
                zxing.IsScanning = false;
                scanTries = 0;
                App.Current.MainPage = new NavigationPage(new NavMaster());


            };
            overlay = new ZXingDefaultOverlay
            {
                TopText = "Poging: " + scanTries,
                BottomText = "Ga naar aattendance.nl -> App Koppelen",
                ShowFlashButton = zxing.HasTorch,
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}

