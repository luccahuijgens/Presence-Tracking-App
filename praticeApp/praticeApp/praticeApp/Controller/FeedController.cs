using praticeApp.Domain;
using praticeApp.Service;
using praticeApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace praticeApp.Controller
{
    class FeedController
    {


        public static Page ItemTapped(object sender, ItemTappedEventArgs e)
        {
            FeedItem feedItem = (FeedItem)((ListView)sender).SelectedItem;
            if (feedItem.GetType() == typeof(Notification))
            {
                Notification tappedNotification = (Notification)feedItem;
                var newPage = new NotificationDetail(tappedNotification);
                return newPage;
            }
            else if (feedItem.GetType() == typeof(Question))
            {
                Question tappedQuestion = (Question)feedItem;
                var newPage = new QuestionDetail(tappedQuestion);
                return newPage;
            }
            return new Page();
        }
        public static ObservableCollection<FeedItem> updateFeed()
        {
            string token = ServiceProvider.GetConfigService().GetStudentToken();
            ObservableCollection<FeedItem> FeedList = new ObservableCollection<FeedItem>();
            List<FeedItem> combinedList = new List<FeedItem>();
            foreach (Question question in ServiceProvider.GetQuestionService().GetQuestions(token))
            {
                combinedList.Add(question);
            }
            foreach (Notification notification in ServiceProvider.GetNotificationService().GetNotifications(token))
            {
                combinedList.Add(notification);
            }
            combinedList = combinedList.OrderBy(x => x.Date).ToList();
            combinedList.Reverse();
            FeedList = new ObservableCollection<FeedItem>(combinedList);
            return FeedList;
        }
    }
}
