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

    public class ActorsFoundEventArgs
    {
        public Dictionary<string, IActor> Actors;
        public ActorsFoundEventArgs(Dictionary<string, IActor> actors)
        {
            Actors = actors;
        }
    }


    /// <summary>
    /// Use it for game logic. And create actor from table.
    /// </summary>
    public class ActorFinder
    {
        private Dictionary<string, IActor> _currentVisibleActors;

        //private DeviceDatabase devDatabase;
        private LocalDeviceDatabase devDatabase;
        public BLEScanner bLEScanner;

        private ActorFactory ActorFactory;

        private List<BLEDeviceView> _currentDevList;

        private Timer timer;

        public int minRSSI = -60;

        public delegate void ActorsFoundEventHandler(object sender, ActorsFoundEventArgs e);

        public event ActorsFoundEventHandler ActorsFound;

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




            bLEScanner.ScanTimout += BLEScanner_ScanTimout;

        }

        private void BLEScanner_ScanTimout(object sender, ScanTimoutEventArgs e)
        {

            var result = new Dictionary<string, IActor>();
            foreach (var dev in e.DevList)
            {
                Console.WriteLine(" I found {0}", dev.id);
                string id = dev.id.ToString();
                var row = devDatabase.GetDeviceById(id);
                if (row != null)
                {
                    result.TryAdd(row.ID, ActorFactory.NewActor(row));
                }
            }

            ActorsFound(this, new ActorsFoundEventArgs(result));
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            foreach (var actor in _currentVisibleActors)
            {
                actor.Value.TickTimeout();
                if (actor.Value.GetTimeout() == -5)
                {
                    _currentVisibleActors.Remove(actor.Key);
                }
            }
        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            if (e.Device.Rssi < minRSSI)
                return;
            string id = e.Device.Id.ToString();
            var row = devDatabase.GetDeviceById(id);
            if (row != null)
            {
                var actor = ActorFactory.NewActor(row);

                _currentVisibleActors.TryAdd(row.ID, actor);
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


        public Dictionary<string, IActor> GetActorsFromScan()
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
        public void Update()
        {
            var dev_list = bLEScanner.GetDeviceList();
            //            _currentVisibleActors.Clear();

            foreach (var dev in dev_list)
            {
                var row = devDatabase.GetDeviceById(dev.id.ToString());
                Console.WriteLine("Find {0}", dev.id.ToString());

            }
        }

    }
}