using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace praticeApp.Droid
{
    [BroadcastReceiver]
    public class BackgroundReceiver : BroadcastReceiver
    {
  
        public override void OnReceive(Context context, Intent intent)
        {
            
            PowerManager pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
            wakeLock.Acquire();
            MessagingCenter.Send<Object>(this, "BackgroundLoop");
            wakeLock.Release();
            System.Diagnostics.Debug.WriteLine("BackLoop");
        }
    }
}