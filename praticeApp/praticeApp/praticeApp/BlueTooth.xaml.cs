using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace praticeApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BlueTooth : ContentPage
    {
        public BlueTooth()
        {
            InitializeComponent();
        }
        public void ActivateBluetooth (object sender, EventArgs e)
        {
           BleScan();
            

            //Hier komen de bluetooth functies
        }

        public async void BleScan()
        {
            var ble = CrossBluetoothLE.Current;
            var state = ble.State;
            var adapter = ble.Adapter;
            var deviceList = new ArrayList();
        
            Plugin.BLE.Abstractions.Contracts.IDevice device = null;
            
            adapter.ScanTimeout = 10000;
  
          
            
            adapter.DeviceDiscovered += (s, a) =>
            {
                deviceList.Add(a.Device);
                if (a.Device.Name == "AA-Beacon")
                {
                    device = a.Device;
                    adapter.StopScanningForDevicesAsync();
                    BleConect(device, adapter);

                    //DisplayAlert("Bluetooth device", "id: " + a.Device.Id.ToString() + " Name: " + a.Device.Name + " Rssi: " + a.Device.Rssi.ToString() + "Hash code: " + a.Device.GetHashCode().ToString() + " Native: " + a.Device.NativeDevice, "ok");
                }
                
            };
            await adapter.StartScanningForDevicesAsync();

           
        }

        public async void BleConect(Plugin.BLE.Abstractions.Contracts.IDevice device, Plugin.BLE.Abstractions.Contracts.IAdapter adapter)
        {
            try
            {
                await adapter.ConnectToDeviceAsync(device);
                var beaconservices = device.GetServicesAsync();
                await DisplayAlert("state", device.State.ToString() + " services: " + beaconservices.ToString(), "ok");
                adapter.DisconnectDeviceAsync(device);

            }
            catch (DeviceConnectionException e)
            {
                DisplayAlert("ERROR", "COULDN'T CONNCET", "FUUU");
            }

        }
            
        
    }
}
