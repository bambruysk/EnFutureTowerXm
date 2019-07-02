using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    class ActorFinder
    {
        private List<IActor> _currentVisibleActors;

        private DeviceDatabase devDatabase;

        private BLEScanner bLEScanner;

        private ActorFactory ActorFactory;


        public ActorFinder()
        {
            bLEScanner = new BLEScanner();
            devDatabase = new DeviceDatabase();
            ActorFactory = new ActorFactory();
            
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

        public List<IActor> GetActors()
        {
            return _currentVisibleActors;
        }

        public void Update ()
        {
            var dev_list = bLEScanner.GetDeviceList();
            _currentVisibleActors.Clear();

            foreach (var dev in dev_list)
            {
                var row =  devDatabase.GetDeviceById(dev.id.ToString());
                if (row != null)
                {
                    _currentVisibleActors.Add(ActorFactory.NewActor(row));
                }
            }
        } 
 
    }
}