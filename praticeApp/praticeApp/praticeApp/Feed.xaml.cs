using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Feed : ContentPage
    {
        public ObservableCollection<Notification> NotificationList { get; set; }

        public Feed()
        {
            InitializeComponent();

            updateFeed();
        }

        void updateFeed()
        {
            NotificationList = new ObservableCollection<Notification>();
            string url = "http://5ca5f65f3a08260014278eaf.mockapi.io/notifications";
            var webRequest = System.Net.WebRequest.Create(url);
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.Timeout = 12000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("X-API-KEY", "459KrmhgSItMD0xBPX2KnThfsjUQXEMsh44P6YVu");

                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        string jsonResponse = sr.ReadToEnd();
                        JArray notificationarray = JArray.Parse(jsonResponse);
                        foreach (JObject notification in notificationarray.Children<JObject>())
                        {

                            Notification notobject = new Notification { ID = (int)notification["id"], Title = (String)notification["title"], Body = (String)notification["body"] };
                            NotificationList.Add(notobject);
                        }
                    }
                }

                MyListView.ItemsSource = NotificationList;
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
