using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace EnFutureTowerXm
{


    // TODO change to static or singletone
    // And rewrite this full
    
    public class BLEScanner
    {
        private Timer timer;
        private IBluetoothLE ble;
        public Plugin.BLE.Abstractions.Contracts.IAdapter adapter;
        private List<BLEDeviceView> deviceList;

        public int MinRssi;


        public BLEScanner()
        {

            timer = new Timer
            {
                Interval = 1000
            };
            timer.Elapsed += OnTimerTick;

            ble = CrossBluetoothLE.Current;
            adapter = ble.Adapter;
            deviceList = new List<BLEDeviceView>();

            MinRssi = -100;
            adapter.DeviceDiscovered += Adapter_DeviceDiscovered;


        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            Console.WriteLine("DeviceFound");

            if (e.Device.Rssi >= MinRssi)
            {
                if (deviceList.Exists(d => d.id == e.Device.Id))
                {

                    var dev = deviceList.Find(d => d.id == e.Device.Id);
                    dev.UpdateCountdown();
                    dev.RSSI = e.Device.Rssi;

                }
                else
                {
                    deviceList.Add(new BLEDeviceView(e.Device));
                }
            }
        }

        public void StartScan()
        {
            timer.Start();
        }

        public void StopScan()
        {
            timer.Stop();
        }
        private async void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            if (!ble.Adapter.IsScanning)
            {
                await adapter.StartScanningForDevicesAsync();
            }
            ProcessDevices();
        }

        public void ResumeScan()
        {
            timer.Start();
        }


        public List<BLEDeviceView> GetDeviceList()
        {
            return deviceList;
        }

        private void ProcessDevices()
        {
            foreach (var device in deviceList)
            {
                device.Countdown--;
            }
            for (int i = 0; i < deviceList.Count; i++)
            {
                if (deviceList[i].Countdown == 0)
                {
                    deviceList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}