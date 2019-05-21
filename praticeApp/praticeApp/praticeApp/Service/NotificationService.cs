using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public class NotificationService : INotificationService
    {
        private INotificationLoader NotificationLoader = new NotificationLoader();

        public List<Notification> GetNotifications()
        {
            return NotificationLoader.GetNotifications();
        }
    }
}
