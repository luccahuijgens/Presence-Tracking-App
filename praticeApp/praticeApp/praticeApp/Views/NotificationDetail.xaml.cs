using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using praticeApp.Controller;
using praticeApp.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationDetail : ContentPage
    {
        public NotificationDetail(Notification notification)
        {
            InitializeComponent();
            NotificationDetailController.FillNotificationDetailPage(notification,notificationTitle,notificationDate,notificationBody);
            
        }
    }
}