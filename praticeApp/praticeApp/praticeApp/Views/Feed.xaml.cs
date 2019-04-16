using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using praticeApp.Domain;
using praticeApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
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
            NotificationList = new ObservableCollection<Notification>(NotificationService.GetNotifications());
            MyListView.ItemsSource = NotificationList;
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Notification tappedNotification = (Notification)((ListView)sender).SelectedItem;
            var newPage = new NotificationDetail(tappedNotification);
            Navigation.PushAsync(newPage);
        }
        private void UpdateFeedButton(object sender, EventArgs args)
        {
            updateFeed();
        }
    }
}
