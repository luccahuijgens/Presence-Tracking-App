using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using praticeApp.Views;
using OpenNETCF.IoC;
using praticeApp.Resources;
using praticeApp.Controller;
using System.Diagnostics;
using praticeApp.DataAccess;
using praticeApp.Service;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace praticeApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public App()
        {
            InitializeComponent();

            String token = new String(new char[] { });

            if (!RootWorkItem.Services.Contains<YNCEndpoint>())
            {
                RootWorkItem.Services.Add<YNCEndpoint>(new YNCEndpoint("https://beacon.aattendance.nl/api/v2/", true));
            }
            
            MainPage = new NavigationPage(new NavMaster());
            BackgroundController bController = new BackgroundController();
            
            ConfigService service = ServiceProvider.GetConfigService();

            MessagingCenter.Subscribe<object>(this, "BackgroundLoop", (s) =>
            {
                Device.BeginInvokeOnMainThread(() =>
              { 
                  if (Convert.ToBoolean(service.GetTrackingState()))
                  {
                      service.SetTrackingTime( "1");
                      //MainPage.DisplayAlert("test", service.gettrackingTime(), "okay");
                      bController.ActivateBluetooth();
                     
                      
                    
                  }
                });
            });

        }
       
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
