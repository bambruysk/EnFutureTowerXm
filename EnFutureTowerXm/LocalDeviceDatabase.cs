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
    class LocalDeviceDatabase
    {
        public Dictionary<string, DeviceTableRow> dev_table;
        public LocalDeviceDatabase ()
        {
            dev_table = new Dictionary<string, DeviceTableRow>
            {
                { "fffffaa9c01c", new DeviceTableRow("fffffaa9c01c,PLAYER,RED,,,") },
                { "fffffaa9bcf4", new DeviceTableRow("fffffaa9bcf4,PLAYER,RED,,,") },
                { "fffffaa9bcf6", new DeviceTableRow("fffffaa9bcf6,PLAYER,RED,,,") },
                { "fffffaa9c001", new DeviceTableRow("fffffaa9c001,PLAYER,RED,,,") },
                { "fffffaa9c0ba", new DeviceTableRow("fffffaa9c0ba,PLAYER,RED,,,") },
                { "fffffaa9bcc6", new DeviceTableRow("fffffaa9bcc6,PLAYER,BLUE,,,") },
                { "fffffaa9bcf2", new DeviceTableRow("fffffaa9bcf2,PLAYER,BLUE,,,") },
                { "fffffaa9c5b0", new DeviceTableRow("fffffaa9c5b0,PLAYER,BLUE,,,") },
                { "fffffaa9bd03", new DeviceTableRow("fffffaa9bd03,PLAYER,BLUE,,,") },
                { "fffffaa9c0a6", new DeviceTableRow("fffffaa9c0a6,PLAYER,BLUE,,,") },
                { "fffffaa9bd19", new DeviceTableRow("fffffaa9bd19,ARTEFACT,,HEAL ONETIME,20,180") },
                { "fffffaa9bf75", new DeviceTableRow("fffffaa9bf75,ARTEFACT,,BOMB,20,180") },
                { "fffffaa9c09e", new DeviceTableRow("fffffaa9c09e,ARTEFACT,,HEAL PERMANENT,2,180") },
                { "fffffaa9bfb8", new DeviceTableRow("fffffaa9bfb8,ARTEFACT,,POISON,2,180") },
                { "fffffaa9bd1c", new DeviceTableRow("fffffaa9bd1c,ARTEFACT,,IMMUNE,0,180") }
            };
        }

        public DeviceTableRow GetDeviceById(string id)
        {
           
           
           if (id.Length > 12)
           {
                id = id.Substring(id.Length - 12);
           }


            if (dev_table.ContainsKey(id))
                return dev_table[id];
            else
                return null; // bydlocode hule
        }
    }
}