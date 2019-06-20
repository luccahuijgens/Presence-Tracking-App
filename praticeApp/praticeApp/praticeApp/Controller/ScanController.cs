using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using praticeApp.Service;

namespace praticeApp.Controller
{
    class ScanController
    {

        public static Boolean ProcessResult(String token)
        { 

            if (ServiceProvider.GetStudentService().GetStudentNameWithToken(token) != null)
            {
                ServiceProvider.GetConfigService().WriteStudentToken(token);
                Debug.WriteLine("\nDumped token to file...\n");
                return (true);
            }
            else
            {
                Debug.WriteLine("\nIncorrect toke....\n");
                return (false);
            }
        }
    }
}
