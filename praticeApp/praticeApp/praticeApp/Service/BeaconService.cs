#define BEACON_ENABLE_DEBUG

using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UniversalBeacon.Library.Core.Entities;
using UniversalBeacon.Library.Core.Interfaces;
using UniversalBeacon.Library.Core.Interop;
using Xamarin.Forms;

namespace praticeApp.Service
{
    internal class BeaconService : IDisposable
    {
        private readonly BeaconManager _manager;

        private bool _performingScan = false;

        public BeaconService()
        {
            // get the platform-specific provider
            var provider = RootWorkItem.Services.Get<IBluetoothPacketProvider>();

            if (provider != null)
            {
                // create a beacon manager, giving it an invoker to marshal collection changes to the UI thread
                _manager = new BeaconManager(provider, Device.BeginInvokeOnMainThread);
                //_manager.Start();

                provider.WatcherStopped += Provider_WatcherStopped;

#if BEACON_ENABLE_DEBUG
                _manager.BeaconAdded += _manager_BeaconAdded;
                provider.AdvertisementPacketReceived += Provider_AdvertisementPacketReceived;    
#endif
            }
        }

        public bool IsScanning()
        {
            return _performingScan;
        }

        
        public void CreateScannerIntervalTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(60), () =>
            {
#if BEACON_ENABLE_DEBUG
                Debug.WriteLine("Starting scanning...");
#endif
                if (!_performingScan)
                    CreateBackgroundScan();

                return true;
            });
        }

        private void CreateBackgroundScan()
        {
            _performingScan = true;
            StartScan();

            // Instantiate a timer to stop the service and process our results.
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                _performingScan = false;

                Debug.WriteLine("Deactivating Beacon discovery service...");
                Debug.WriteLine("Final beacon count: " + Beacons.Count);

                Debug.WriteLine("Stopping scanning...");

                //();
                _manager?.Stop();
                //RootWorkItem.Services.Remove(_service); // Cleanup the service...

                // --> Start registration service
                // --> Stop registration service

                // -----> POST API CALL (YNC) Json with all beacons
                // -----> Response: Registered/unregistred (classroom HL15-2.34)

                // We want to kill the timer directly, so we return false. The timer will now stop.
                return false;
            });
        }

        public async Task PerformSingleScan()
        {
            _performingScan = true;
            StartScan();

            await Task.Delay(5000);

            // Instantiate a timer to stop the service and process our results.
            //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            //{
            _performingScan = false;

            Debug.WriteLine("Deactivating Beacon discovery service...");
            Debug.WriteLine("Final beacon count: " + Beacons.Count);

            Debug.WriteLine("Stopping scanning...");

            //();
            _manager?.Stop();
            //RootWorkItem.Services.Remove(_service); // Cleanup the service...

            // --> Start registration service
            // --> Stop registration service

            // -----> POST API CALL (YNC) Json with all beacons
            // -----> Response: Registered/unregistred (classroom HL15-2.34)

            // We want to kill the timer directly, so we return false. The timer will now stop.
            //return false;
           //});
        }


        private void StartScan()
        {
            _manager?.Start();
        }

        public void Dispose()
        {
            _manager?.Stop();
        }

        public ObservableCollection<Beacon> Beacons => _manager?.BluetoothBeacons;


#if BEACON_ENABLE_DEBUG
        void _manager_BeaconAdded(object sender, Beacon e)
        {
            Debug.WriteLine($"_manager_BeaconAdded {sender} Beacon {e.BluetoothAddressAsString}");
        }

        void Provider_AdvertisementPacketReceived(object sender, UniversalBeacon.Library.Core.Interop.BLEAdvertisementPacketArgs e)
        {

        }

        void Provider_WatcherStopped(object sender, BTError error)
        {
            Debug.WriteLine("Beacon Service stopped with error: " + error.ToString());
        }
#endif
    }
}
