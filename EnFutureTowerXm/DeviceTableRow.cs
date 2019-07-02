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
    public class DeviceTableRow
    {
        public string ID { get; }
        public string Type { get; }
        public string Team { get; }
        public string ArtefactType { get; }
        public int PowerValue { get; }
        public int Cooldown { get; }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="id"> ID</param>
        /// <param name="type"> Type </param>
        /// <param name="team"></param>
        /// <param name="artefactType"></param>
        /// <param name="powerValue"></param>
        /// <param name="cooldown"></param>

        public DeviceTableRow(string id, string type, string team, string artefactType, int powerValue, int cooldown)
        {
            ID = id;
            Type = type;
            Team = team;
            ArtefactType = artefactType;
            PowerValue = powerValue;
            Cooldown = cooldown;

        }

        /// <summary>
        /// Constructor fron CSV table. I wlii need checking data later
        /// </summary>
        /// <param name="csv_string"></param>

        public DeviceTableRow(string csv_string)
        {
            string[] splitted_str = csv_string.Split(',');
            ID = splitted_str[0];
            Type = splitted_str[1];
            Team = splitted_str[2];
            ArtefactType = splitted_str[3];

            if (Int32.TryParse(splitted_str[4], out int power))
            {
                PowerValue = power;
                /// Need add raising exception but I do it later
            }
            if (Int32.TryParse(splitted_str[5], out int cd))
            {
                Cooldown = cd;
                /// Need add raising exception but I do it later
            }

        }
    }
}