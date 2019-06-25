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
        public static void switchTracking()
        {
            Debug.WriteLine("New tracking state: " + service.GetTrackingState());
            if (Convert.ToBoolean(service.GetTrackingState()))
            {
                service.setTrackingState(false);
            }
            else
            {
                service.setTrackingState(true);
            }
        }
    }
}
