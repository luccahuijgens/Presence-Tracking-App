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
                        scanTries = (scanTries + 1);
                        overlay.TopText = "Poging: " + scanTries;
                    }
                }
                
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await DisplayAlert("Fout~!", "Je account kan niet worden gekoppeld, controleer je internetverbinding of probeer het later opnieuw.", "Begrepen.");
                    });
                

};
            overlay = new ZXingDefaultOverlay
            {
                TopText = "Poging: " + scanTries,
                BottomText = "Even geduld a.u.b.",
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

