#define BEACON_ENABLE_DEBUG

using OpenNETCF.IoC;
using praticeApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UniversalBeacon.Library.Core.Entities;
using Xamarin.Forms;

namespace praticeApp.Resources
{
    class BeaconDiscovery
    {
        private BeaconService _service;

        private void Init()
        {
            _service = RootWorkItem.Services.Get<BeaconService>();
            if (_service == null)
            {
                Debug.WriteLine("Creating beacon discovery service...");
                _service = RootWorkItem.Services.AddNew<BeaconService>();
                //if (_service.Beacons != null) _service.Beacons.CollectionChanged += Beacons_CollectionChanged;
            }
        }

        public async Task PerformSingleScan()
        {
            Init();   

            if (_service == null)
            {
                Disable();
            }

#if BEACON_ENABLE_DEBUG
            Debug.WriteLine("Performing single scan...");
#endif

            await _service.PerformSingleScan();
               
            Disable();
        }

        public ObservableCollection<Beacon> GetLatestScanResult()
        {
            return _service.Beacons;
        }

        public void EnableBackgroundService()
        {
            Init();

            _service.CreateScannerIntervalTimer();
        }

        public void Disable()
        {
            _service.Dispose();
        }
       
        ~BeaconDiscovery()
        {
            Disable();

            if (_service != null)
                RootWorkItem.Services.Remove(_service); // Cleanup the service...
        }
    }
}
