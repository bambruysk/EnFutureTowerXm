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
using Plugin.BLE.Abstractions.Contracts;

namespace EnFutureTowerXm
{


    public class BLEDeviceView

    {
        public Guid id { get; }
        public int Countdown;

        public int RSSI;

        //        public enum eDevType {FORGE, ALCHEMY, JEWELRY, PALACE, ARTIFACT}


        public BLEDeviceView(IDevice device)
        {
            id = device.Id;
            Countdown = 5;
            RSSI = device.Rssi;

        }

        public void CountDownTick ()
        {
            Countdown--;
        }

        public void UpdateCountdown()
        {
            Countdown = 5;
        }

        public string GetShortId( Guid guid)
        {
            string id_short_str = guid.ToString();
            if (id_short_str.Length > 12)
            {
                id_short_str = id_short_str.Substring(id_short_str.Length - 12);
            }
            return id_short_str;
        }
    }
}