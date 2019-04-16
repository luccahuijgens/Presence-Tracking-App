using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public static class NotificationService
    {
        private static NotificationLoader notificationLoader = new NotificationLoader();

        public static List<Notification> GetNotifications()
        {
            return notificationLoader.getNotifications();
        }
    }
}
