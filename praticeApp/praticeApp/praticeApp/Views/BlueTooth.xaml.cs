

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
using praticeApp.Service;
using praticeApp.Resources;
using System.Collections.Generic;
using Newtonsoft.Json;
using praticeApp.Domain;
using praticeApp.DataAccess;
using static praticeApp.Domain.BeaconJSON;
using System.Data;
using System.Linq;

namespace praticeApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlueTooth : ContentPage
    {
        private BeaconDiscovery _beaconDiscovery = null;
        //private YNCEndpoint _yncEndpoint = null;
        

        public BlueTooth()
        {
            InitializeComponent();

            String token = new String(new char[] { });
            
            _beaconDiscovery = new BeaconDiscovery();
        }

        protected async override void OnAppearing()
        {
            await UpdateRegistrationEvents();

            
        }

        public async Task UpdateRegistrationEvents()
        {
            var yncEndpoint = RootWorkItem.Services.Get<YNCEndpoint>();

            if (yncEndpoint == null)
            {
                await DisplayAlert("Fout", "Er is een onbekende fout opgetreden! Probeer het later opnieuw.", "OK");
                return;
            }

            KeyValuePair<YNCEndpointStatus, Events> events = await yncEndpoint.GetStudentEvents();


            if (events.Key == YNCEndpointStatus.OK && events.Value != null && events.Value.data.Count > 0)
            {
                // Update the events...
                var currentTime = DateTime.Now;

                //ObservableCollection<EventItem> EventList = new ObservableCollection<EventItem>();
                List<EventItem> combinedList = new List<EventItem>();


                foreach (var ev in events.Value.data)
                {
                    //Debug.WriteLine(ev.attributes.name);
                    //Debug.WriteLine(ev.attributes.lecturers);
                    //Debug.WriteLine(ev.attributes.classrooms);
                    //Debug.WriteLine(ev.attributes.attended);
                    //Debug.WriteLine(ev.attributes.starttime);
                    //Debug.WriteLine(ev.attributes.endtime);

                    DateTime start = new DateTime();
                    DateTime end = new DateTime();

                    if (ev.attributes.GetStartTime(ref start) && ev.attributes.GetEndTime(ref end))
                    {
                        var timeSpan = currentTime - start;

  
                            //Debug.WriteLine(ev.attributes.name);
                            //Debug.WriteLine(ev.attributes.lecturers);
                            //Debug.WriteLine(start.ToString());
                            //Debug.WriteLine(end.ToString());

                        if (timeSpan.Days <= 7)
                        {
                            combinedList.Add(new EventItem { LessonName = ev.attributes.name, LessonHasAttended = (ev.attributes.attended) ? "AANWEZIG" : "AFWEZIG", Teachers = "Docent(en): " + ev.attributes.lecturers, Start = start, End = end, TextColor = (ev.attributes.attended) ? "Black" : "White", BackgroundColor = (ev.attributes.attended) ? "White" : "#ff5959" }); // #f24343
                        }
                    }
                }

                noLessonRegLabel.IsVisible = false;
                EventsListView.IsVisible = true;

                combinedList = combinedList.OrderBy(x => x.Start).ToList();
                combinedList.Reverse();

                EventsListView.ItemsSource = new ObservableCollection<EventItem>(combinedList);
            }
            else
            {
                noLessonRegLabel.IsVisible = true;
                EventsListView.IsVisible = false;
            }

                

            }

        public async Task RequestPermissions()
        {
            await RequestLocationPermission();
            PermissionStatus statusLoc = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            PermissionStatus statusLocInUse = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);

            if (statusLoc != PermissionStatus.Granted || statusLocInUse != PermissionStatus.Granted)
            {
                await DisplayAlert("Fout", "Onvoldoende permissies, kan niet gebruik maken van locatie. Controlleer instellingen.", "OK");
            }
        }

        private async Task RequestLocationPermission()
        {
            // Actually coarse location would be enough, the plug-in only provides a way to request fine location
            var requestedPermissionsLoc = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Location);
            var requestedPermissionsLocInUse = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.LocationWhenInUse);
            //var requestedPermissionStatus = requestedPermissions[Plugin.Permissions.Abstractions.Permission.Location];
        }

        public async void ActivateBluetooth(object sender, EventArgs e)
        {
            if (_beaconDiscovery == null)
                return;

            ((Button)sender).IsEnabled = false;

            await RequestPermissions();

            ((Button)sender).Text += "...";

            try
            {
                await _beaconDiscovery.PerformSingleScan();
            } catch (System.NullReferenceException)
            {
                _beaconDiscovery.Disable();
                await DisplayAlert("Fout", "Er is een onbekende fout opgetreden!", "OK");
                ((Button)sender).IsEnabled = true;

                return;
            }

            var result = _beaconDiscovery.GetLatestScanResult();
            var beacons = new ObservableCollection<Beacon>();

            foreach (Beacon b in result)
            {
                if (b.BeaconType != Beacon.BeaconTypeEnum.Unknown && b.BeaconFrames.Count > 0)
                {
                    beacons.Add(b);
                }
            }

            if (beacons.Count > 0)
            {
                BeaconJSON beaconInJson = new BeaconJSON();

                foreach (Beacon b in beacons)
                {
                    beaconInJson.Add(GetBeaconUUID(b), b.Rssi);
                }

                try
                {
                    String json = beaconInJson.ParseJson();

                    var yncEndpoint = RootWorkItem.Services.Get<YNCEndpoint>();

                    if (yncEndpoint == null)
                    {
                        await DisplayAlert("Fout", "Er is een onbekende fout opgetreden! Probeer het later opnieuw.", "OK");
                        return;
                    }

                    YNCEndpointStatus endpointStatus = await yncEndpoint.AddBeaconRegistration(json);

                    switch (endpointStatus)
                    {
                        case YNCEndpointStatus.OK:
                            await DisplayAlert("Succes", "Geregistreerd!", "OK");
                            break;
                        case YNCEndpointStatus.NoYNCBeaconsFound:
                            await DisplayAlert("Fout", "Er zijn geen beacons van Academy Attendance gevonden!", "OK");
                            break;
                        case YNCEndpointStatus.AuthFailed:
                            await DisplayAlert("Autorisatie fout", "Er is een autorisatie fout opgetreden. Probeer opnieuw te registreren.", "OK");
                            break;
                        case YNCEndpointStatus.AlreadyRegistered:
                            await DisplayAlert("Registratie fout", "Je bent al registreert voor deze les!", "OK");
                            break;
                        case YNCEndpointStatus.UnexpectedHttpError:
                            await DisplayAlert("Fout", "Er is een onbekende fout opgetreden! Probeer het later opnieuw.", "OK");
                            break;
                        default:
                            break;
                    }

                }
                catch (JsonException ex)
                {
                    Debug.WriteLine("JsonConvert error: " + ex.ToString());
                    await DisplayAlert("Fout", "Er is een onbekende fout opgetreden! Probeer het later opnieuw.", "OK");
                }
                

                //beaconsText.Text = str;
            } else
            {
                await DisplayAlert("Fout", "Er zijn geen beacons van Academy Attendance gevonden!", "OK");
            }

            ((Button)sender).Text = ((Button) sender).Text.Replace("...", "");
            ((Button)sender).IsEnabled = true;

            await UpdateRegistrationEvents();

        }

        public String GetBeaconUUID(Beacon b)
        {
            // Whenever a beacon protocol is detected, we print the advertisement data.
            if (b.BeaconType != Beacon.BeaconTypeEnum.Unknown)
            {

                foreach (var beaconFrame in b.BeaconFrames)
                {
                    if (beaconFrame.IsValid())
                    {
                        // If we are dealing with a proximity beacon, dump to debug.
                        if (beaconFrame.GetType() == typeof(ProximityBeaconFrame))
                        {
                            return ((ProximityBeaconFrame)beaconFrame).UuidAsString;
                        }
                    }
                }
            }

            return "";
        }

        public void DumpBeacon(Beacon b)
        {
            // Whenever a beacon protocol is detected, we print the advertisement data.
            if (b.BeaconType != Beacon.BeaconTypeEnum.Unknown)
            {
                Debug.WriteLine("MAC Address: " + b.BluetoothAddressAsString);
                Debug.WriteLine("Beacon Type: " + b.BeaconType.ToString());
                Debug.WriteLine("Amount of frames: " + b.BeaconFrames.Count);

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


}
