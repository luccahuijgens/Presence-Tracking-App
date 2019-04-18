using System;
using System.Collections.Generic;
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
        public ObservableCollection<FeedItem> FeedList { get; set; }

        public Feed()
        {
            InitializeComponent();
            updateFeed();
        }

        void updateFeed()
        {
            List<FeedItem> combinedList= NotificationService.GetNotifications().Cast<FeedItem>().Concat(QuestionService.GetQuestions().Cast<FeedItem>()).ToList();
            combinedList.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
            combinedList.Reverse();
            FeedList = new ObservableCollection<FeedItem>(combinedList);
            MyListView.ItemsSource = FeedList;
        }

        private void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            FeedItem feedItem = (FeedItem)((ListView)sender).SelectedItem;
            if (feedItem.FeedType == "Notification")
            {
                Notification tappedNotification = (Notification)feedItem;
                var newPage = new NotificationDetail(tappedNotification);
                Navigation.PushAsync(newPage);
            }
        }
        private void UpdateFeedButton(object sender, EventArgs args)
        {
            updateFeed();
        }
    }
}
