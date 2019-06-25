using Android.App;
using Android.Content;
using Android.OS;
using System;
using Xamarin.Forms;

namespace BackgroundTasks.Droid
{
    [Service]
    public class Background : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
          

            return StartCommandResult.NotSticky;
        }

    }
}