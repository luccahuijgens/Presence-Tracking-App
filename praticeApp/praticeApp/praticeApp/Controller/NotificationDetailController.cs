using praticeApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace praticeApp.Controller
{
    class NotificationDetailController
    {
        public static void FillNotificationDetailPage(Notification notification, Label notificationTitle, Label notificationDate, Label notificationBody)
        {
            notificationTitle.Text = notification.Subject;
            notificationDate.Text = Convert(notification.Date);
            notificationBody.Text = notification.Content;
        }

        public static String Convert(DateTime Date)
        {
            if (DateTime.Now.ToString("MM/dd/yyyy").Equals(Date.ToString("MM/dd/yyyy")))
            {
                return Date.ToString("HH:mm");
            }
            else if (DateTime.Today - Date.Date == TimeSpan.FromDays(1))
            {
                return "Yesterday";
            }
            else if (DateTime.Today.Year != Date.Year)
            {
                return Date.ToString("dd/MM/yyyy");
            }
            else
            {
                return Date.ToString("dd MMM");
            }
        }
    }
}
