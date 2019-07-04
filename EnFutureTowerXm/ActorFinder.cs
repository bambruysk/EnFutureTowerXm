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

namespace EnFutureTowerXm
{
        /// <summary>
        /// Use it for game logic. And create actor from table.
        /// </summary>
    public class ActorFinder
    {
        private Dictionary<string,IActor> _currentVisibleActors;

        //private DeviceDatabase devDatabase;
        private LocalDeviceDatabase devDatabase;
        public BLEScanner bLEScanner;

        private ActorFactory ActorFactory;

        private List<BLEDeviceView> _currentDevList;

        private Timer timer;


        public ActorFinder()
        {
            bLEScanner = new BLEScanner();
            devDatabase = new LocalDeviceDatabase();
            ActorFactory = new ActorFactory();
            _currentVisibleActors = new Dictionary<string, IActor>();
            bLEScanner.adapter.DeviceDiscovered += Adapter_DeviceDiscovered;

            timer = new Timer
            {
                Interval = 1000
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var actor in _currentVisibleActors)
            {
                actor.Value.TickTimeout();
                if (actor.Value.GetTimeout() == -10)
                {
                    _currentVisibleActors.Remove(actor.Key);
                }
            }
        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            string id = e.Device.Id.ToString();
            var row = devDatabase.GetDeviceById(id);
            if (row != null)
            {
                _currentVisibleActors.TryAdd(row.ID, ActorFactory.NewActor(row));
            }
        }

        // after it will be need change to events

        public void FindStart()
        {
            bLEScanner.StartScan();
        }

        public void FindStop()
        {
            bLEScanner.StopScan();
        }

        public Dictionary<string, IActor> GetActors()
        {
            var result = new Dictionary<string, IActor>();
            var devlist = bLEScanner.GetDeviceList();
            foreach (var dev in devlist)
            {
                string id = dev.id.ToString();
                var row = devDatabase.GetDeviceById(id);
                if (row != null)
                {
                    result.TryAdd(row.ID, ActorFactory.NewActor(row));
                }
            }
            return result;
        }

        public void Update ()
        {
            var dev_list = bLEScanner.GetDeviceList();
//            _currentVisibleActors.Clear();

            foreach (var dev in dev_list)
            {
                var row =  devDatabase.GetDeviceById(dev.id.ToString());
                Console.WriteLine("Find {0}", dev.id.ToString());

            }
        } 
 
    }
}