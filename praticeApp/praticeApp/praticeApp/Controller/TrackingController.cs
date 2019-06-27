using praticeApp.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace praticeApp.Controller
{
    class TrackingController
    {
        public static ConfigService service = ServiceProvider.GetConfigService();
        public static String switchTracking()
        {
           
            if (Convert.ToBoolean(service.GetTrackingState()))
            {
                service.SetTrackingState(false);
            }
            else
            {
                service.SetTrackingState(true);
            }
            Debug.WriteLine("New tracking state: " + service.GetTrackingState());
            return service.GetTrackingState();
        }

        public static String GetCurrentTrackingState()
        {
            return (service.GetTrackingState());
        }
    }
}
