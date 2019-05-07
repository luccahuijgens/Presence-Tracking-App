using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            notificationTitle.Text = notification.Subject;
            notificationDate.Text = Convert(notification.Date);
            notificationBody.Text = notification.Content;
        }

        public String Convert(DateTime Date)
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