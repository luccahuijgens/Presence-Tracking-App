using praticeApp.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tracking : ContentPage
    {
        public Tracking()
        {
            InitializeComponent();
        }

        public void ActivateTracking(object sender, EventArgs e)
        {
            TrackingController.switchTracking();
        }
    }
}
