using OpenNETCF.IoC;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using praticeApp.Domain;
using praticeApp.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UniversalBeacon.Library.Core.Entities;
using UniversalBeacon.Library.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniversalBeacon.Library.Core.Interop;
using praticeApp.Service;
using Newtonsoft.Json;
using praticeApp.DataAccess;
using static praticeApp.Domain.BeaconJSON;

namespace praticeApp.Controller
{
    public class BackgroundController
    {

        private BeaconDiscovery _beaconDiscovery = null;
        //private YNCEndpoint _yncEndpoint = null;


        public BackgroundController()
        {
          

            String token = new String(new char[] { });

            _beaconDiscovery = new BeaconDiscovery();
        }

      
        public async void ActivateBluetooth()
        {
            if (_beaconDiscovery == null)
                return;

            try
            {
                await _beaconDiscovery.PerformSingleScan();
            }
            catch (System.NullReferenceException)
            {
                _beaconDiscovery.Disable();
                Debug.WriteLine("Er is een onbekende fout opgetreden!");         
                
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
                String str = "Er zijn " + beacons.Count + " beacons gevonden:\n";
                BeaconJSON beaconInJson = new BeaconJSON();

                foreach (Beacon b in beacons)
                {
                    beaconInJson.Add(GetBeaconUUID(b), b.Rssi);

                    str += " - " + GetBeaconUUID(b);
                    str += " RSSI: " + b.Rssi.ToString();
                    str += "\n";
                }

                try
                {
                    String json = beaconInJson.ParseJson();

                    var yncEndpoint = RootWorkItem.Services.Get<YNCEndpoint>();

                    if (yncEndpoint == null)
                    {
                        Debug.WriteLine("Er is een onbekende fout opgetreden! Probeer het later opnieuw.");
                        return;
                    }

                    YNCEndpointStatus endpointStatus = await yncEndpoint.AddBeaconRegistration(json);

                    switch (endpointStatus)
                    {
                        case YNCEndpointStatus.OK:
                            Debug.WriteLine("Geregistreerd!");
                            break;
                        case YNCEndpointStatus.NoYNCBeaconsFound:
                            Debug.WriteLine("Er zijn geen beacons van Academy Attendance gevonden!");
                            break;
                        case YNCEndpointStatus.AuthFailed:
                             Debug.WriteLine("Er is een autorisatie fout opgetreden. Probeer opnieuw te registreren.");
                            break;
                        case YNCEndpointStatus.AlreadyRegistered:
                            Debug.WriteLine("Je bent al registreert voor deze les!");
                            break;
                        case YNCEndpointStatus.UnexpectedHttpError:
                            Debug.WriteLine("Er is een onbekende fout opgetreden! Probeer het later opnieuw.");
                            break;
                        default:
                            break;
                    }

                }
                catch (JsonException ex)
                {
                    Debug.WriteLine("JsonConvert error: " + ex.ToString());
                    Debug.WriteLine("Er is een onbekende fout opgetreden! Probeer het later opnieuw.");
                }

                
            }
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
   