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

        public static ZXingScannerPage BuildScannerpage()
        {
            var scanPage = new ZXingScannerPage();
            var animat = new Animation();
            scanPage.Animate("Qr", animat, 5, 100, null, null, null);
            return (scanPage);
        }

        public static Boolean ProcessResult(ZXing.Result scanResult)
        {
            if (!ServiceProvider.GetStudentService().GetStudentNameWithToken(scanResult.Text).Equals(null))
            {
                ServiceProvider.GetConfigService().WriteStudentToken(scanResult.Text);
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
