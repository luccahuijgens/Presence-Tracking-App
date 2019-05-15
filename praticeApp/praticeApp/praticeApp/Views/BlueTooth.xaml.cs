using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using OpenNETCF.IoC;
using UniversalBeacon.Library.Core.Entities;
using UniversalBeacon.Library.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniversalBeacon.Library.Core.Interop;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

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
        }

        void Provider_AdvertisementPacketReceived(object sender, UniversalBeacon.Library.Core.Interop.BLEAdvertisementPacketArgs e)
        {
     
        }


    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlueTooth : ContentPage
    {
        private BeaconService _service;
        //bool _permissionGranted = false;

        public BlueTooth()
        {
            InitializeComponent();
        }
            

        public async Task RequestPermissions()
        {
            await RequestLocationPermission();
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("Fout", "Onvoldoende permissies, kan niet gebruik maken van locatie. Controlleer instellingen.", "OK");
            }
        }

        private async Task RequestLocationPermission()
        {
            // Actually coarse location would be enough, the plug-in only provides a way to request fine location
            var requestedPermissions = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Location);
            var requestedPermissionStatus = requestedPermissions[Plugin.Permissions.Abstractions.Permission.Location];
            Debug.WriteLine("Location permission status: " + requestedPermissionStatus);
            
            // Set the permissions granted flag.
            // _permissionGranted = (requestedPermissionStatus == PermissionStatus.Granted); // Change to CrossPermissions.Current.CheckPermissionStatusAsync
        }

        public void DumpBeacon(Beacon b)
        {
            Debug.WriteLine("MAC Address: " + b.BluetoothAddressAsString);
            Debug.WriteLine("Beacon Type: " + b.BeaconType.ToString());

            // Whenever a beacon protocol is detected, we print the advertisement data.
            if (b.BeaconType != Beacon.BeaconTypeEnum.Unknown)
            {
                Debug.WriteLine("Amount of frames: " + b.BeaconFrames.Count);

                if (b.BeaconFrames.Count > 0)
                {
                    foreach (var beaconFrame in b.BeaconFrames)
                    {
                        if (beaconFrame.IsValid())
                        {
                            // If we are dealing with a proximity beacon, dump to debug.
                            if (beaconFrame.GetType() == typeof(ProximityBeaconFrame))
                            {
                                Debug.WriteLine("--> Proximity frame detected!");

                                // Conversion to JSON and call API...

                                ProximityBeaconFrame proximityFrame = (ProximityBeaconFrame)beaconFrame;
                                Debug.WriteLine("--> Major: " + proximityFrame.MajorAsString);
                                Debug.WriteLine("--> Minor: " + proximityFrame.MinorAsString);
                                Debug.WriteLine("--> UUID: " + proximityFrame.UuidAsString);
                                Debug.WriteLine("--> TX power: " + proximityFrame.TxPower.ToString() + " dB");
                            }
                        }
                    }

                }
            }
        }

        public bool EnableBluetoothLEDiscovery()
        {
            _service = RootWorkItem.Services.Get<BeaconService>();
            if (_service == null)
            {
                Debug.WriteLine("Activating Beacon discovery service...");
                _service = RootWorkItem.Services.AddNew<BeaconService>();
                if (_service.Beacons != null) _service.Beacons.CollectionChanged += Beacons_CollectionChanged;

                // Service on (10 seconds)
                // Service sleeping (5 minutes)
                // Service on (10 seconds)
                // Service sleeping (5 minutes)

                // Instantiate a timer to stop the service and process our results.
                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    Debug.WriteLine("Deactivating Beacon discovery service...");
                    Debug.WriteLine("Final beacon count: " + _service.Beacons.Count);

                    foreach (Beacon b in _service.Beacons)
                    {
                        DumpBeacon(b);
                    }

                    _service.Dispose();
                    RootWorkItem.Services.Remove(_service); // Cleanup the service...

                    // --> Start registration service
                    // --> Stop registration service

                    // -----> POST API CALL (YNC) Json with all beacons
                    // -----> Response: Registered/unregistred (classroom HL15-2.34)

                    // We want to kill the timer directly, so we return false. The timer will now stop.
                    return false;
                });

                return true;
            }
            else
            {
                Debug.WriteLine("Beacon service pointer is null...");
                return false;
            }
        }

        public async void ActivateBluetooth(object sender, EventArgs e)
        {
            await RequestPermissions();

            if (!EnableBluetoothLEDiscovery())
            {
                await DisplayAlert("Fout", "Er is een onbekende fout opgetreden!", "OK");
            }
        }
        private void Beacons_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine($"Beacons_CollectionChanged {sender} e {e}");
        }
    }
}
