using praticeApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace praticeApp.DataAccess
{
    interface INotificationLoader
    {
        List<Notification> GetNotifications(string token);
    }
}
