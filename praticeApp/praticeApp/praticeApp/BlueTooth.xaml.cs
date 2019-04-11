using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlueTooth : ContentPage
    {
        public BlueTooth()
        {
            InitializeComponent();
        }
        public void ActivateBluetooth(object sender, EventArgs e)
        {

            //Hier komen de bluetooth functies
        }
    }
}