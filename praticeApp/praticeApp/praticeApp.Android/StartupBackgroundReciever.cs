using Android.App;
using Android.Content;
using Android.OS;
using BackgroundTasks.Droid;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace praticeApp.Droid
{
    [BroadcastReceiver(Name = "com.yournextconcepts.Attendance.StartupBackgroundReciever", Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    public class StartupBackgroundReciever : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == Intent.ActionBootCompleted)
            {
                var alarmIntent = new Intent(context, typeof(BackgroundReceiver));
                var pending = PendingIntent.GetBroadcast(context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
                var alarmManager = Forms.Context.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.SetRepeating(AlarmType.ElapsedRealtimeWakeup, 3000, 2999, pending);

            }
        }
    }
}

       

