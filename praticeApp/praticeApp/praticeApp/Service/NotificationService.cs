using System;
using System.Collections.Generic;
using System.Text;
using praticeApp.DataAccess;
using praticeApp.Domain;

namespace praticeApp.Service
{
    public class NotificationService
    {
        private INotificationLoader NotificationLoader = new NotificationLoader();

        public List<Notification> GetNotifications()
        {
            string token = ServiceProvider.GetConfigService().GetStudentToken();
            return NotificationLoader.GetNotifications(token);
        }
    }
}
