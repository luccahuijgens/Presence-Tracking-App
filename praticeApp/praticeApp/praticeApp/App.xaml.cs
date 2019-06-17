using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using praticeApp.Views;
using OpenNETCF.IoC;
using praticeApp.Resources;
using praticeApp.DataAccess;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace praticeApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            String token = new String(new char[] { });

            RootWorkItem.Services.Add<YNCEndpoint>(new YNCEndpoint("https://beacon.aattendance.nl/api/v2/", true));
            MainPage = new NavigationPage(new NavMaster());

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
