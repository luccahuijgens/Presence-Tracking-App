using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using OpenNETCF.IoC;
using UniversalBeacon.Library.Core.Entities;
using UniversalBeacon.Library.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniversalBeacon.Library.Core.Interop;

namespace praticeApp.Views
{
    internal class BeaconService : IDisposable
    {
        private readonly BeaconManager _manager;

        public BeaconService()
        {
            // get the platform-specific provider
            var provider = RootWorkItem.Services.Get<IBluetoothPacketProvider>();
            Debug.WriteLine("Before provider !=");
            if (null != provider)
            {
                Debug.WriteLine("After provider !=");
                // create a beacon manager, giving it an invoker to marshal collection changes to the UI thread
                _manager = new BeaconManager(provider, Device.BeginInvokeOnMainThread);
                _manager.Start();
                Debug.WriteLine("After start");

                _manager.BeaconAdded += _manager_BeaconAdded;
                provider.AdvertisementPacketReceived += Provider_AdvertisementPacketReceived;

            }
        }

        public void Dispose()
        {
            _manager?.Stop();
        }

        public ObservableCollection<Beacon> Beacons => _manager?.BluetoothBeacons;


        void _manager_BeaconAdded(object sender, Beacon e)
        {
            Debug.WriteLine($"_manager_BeaconAdded {sender} Beacon {e.BluetoothAddressAsString}");
            if (e.BluetoothAddressAsString.Equals("7F:AA:4A:01:55:FC"))
            {
                Debug.WriteLine("Beacon found making packet.");

                Debug.WriteLine(e.BeaconType.ToString());
                



            }
        }

        void Provider_AdvertisementPacketReceived(object sender, UniversalBeacon.Library.Core.Interop.BLEAdvertisementPacketArgs e)
        {
            Debug.WriteLine($"Provider_AdvertisementPacketReceived {sender} Beacon {e}");
        }

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlueTooth : ContentPage
    {
        private BeaconService _service;
        public BlueTooth()
        {
            InitializeComponent();
        }
        public void ActivateBluetooth(object sender, EventArgs e)
        {
            _service = RootWorkItem.Services.Get<BeaconService>();
            if (_service == null)
            {
                Debug.WriteLine("Service is null.");
                _service = RootWorkItem.Services.AddNew<BeaconService>();
                if (_service.Beacons != null) _service.Beacons.CollectionChanged += Beacons_CollectionChanged;
            }
            Debug.WriteLine("Service is not null.");
        }
        private void Beacons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine($"Beacons_CollectionChanged {sender} e {e}");
        }
    }
}
