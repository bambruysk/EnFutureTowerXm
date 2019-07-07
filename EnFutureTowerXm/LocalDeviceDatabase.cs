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
        public LocalDeviceDatabase()
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
                { "fffffaa9c09e", new DeviceTableRow("fffffaa9c09e,ARTEFACT,,HEAL ONETIME,20,180") },
                { "fffffaa9bf75", new DeviceTableRow("fffffaa9bf75,ARTEFACT,,BOMB,20,180") },
                { "fffffaa9bd19", new DeviceTableRow("fffffaa9bd19,ARTEFACT,,HEAL PERMANENT,2,180") },
                { "fffffaa9bfb8", new DeviceTableRow("fffffaa9bfb8,ARTEFACT,,POISON,2,180") },
                { "fffffaa9bd1c", new DeviceTableRow("fffffaa9bd1c,ARTEFACT,,IMMUNE,0,180") },
                { "fffffaa9c00f", new DeviceTableRow("fffffaa9c00f,PLAYER,RED,,,") },
                { "fffffaa9c486", new DeviceTableRow("fffffaa9c486,PLAYER,RED,,,") },
                { "fffffaa9c065", new DeviceTableRow("fffffaa9c065,PLAYER,RED,,,") },
                { "fffffaa9bcb1", new DeviceTableRow("fffffaa9bcb1,PLAYER,RED,,,") },
                { "fffffaa9bcfd", new DeviceTableRow("fffffaa9bcfd,PLAYER,RED,,,") },
                { "fffffaa9bcfe", new DeviceTableRow("fffffaa9bcfe,PLAYER,RED,,,") },
                { "fffffaa9bc96", new DeviceTableRow("fffffaa9bc96,PLAYER,RED,,,") },
                { "fffffaa9bf73", new DeviceTableRow("fffffaa9bf73,PLAYER,BLUE,,,") },
                { "fffffaa9bcf3", new DeviceTableRow("fffffaa9bcf3,PLAYER,BLUE,,,") },
                { "fffffaa9c0b5", new DeviceTableRow("fffffaa9c0b5,PLAYER,BLUE,,,") },
                { "fffffaa9bfa7", new DeviceTableRow("fffffaa9bfa7,PLAYER,BLUE,,,") },
                { "fffffaa9c035", new DeviceTableRow("fffffaa9c035,PLAYER,BLUE,,,") },
                { "fffffaa9c05f", new DeviceTableRow("fffffaa9c05f,PLAYER,BLUE,,,") },
                { "fffffaa9bfc9", new DeviceTableRow("fffffaa9bfc9,PLAYER,BLUE,,,") },
                { "fffffaa9c3e8", new DeviceTableRow("fffffaa9c3e8,ARTEFACT,,HEAL ONETIME,20,180") },
                { "fffffaa9c080", new DeviceTableRow("fffffaa9c080,ARTEFACT,,HEAL ONETIME,20,180") },
                { "fffffaa9bf70", new DeviceTableRow("fffffaa9bf70,ARTEFACT,,BOMB,20,180") },
                { "fffffaa9c6b5", new DeviceTableRow("fffffaa9c6b5,ARTEFACT,,BOMB,20,180") },
                { "fffffaa9c3e6", new DeviceTableRow("fffffaa9c3e6,ARTEFACT,,HEAL PERMANENT,2,180") },
                { "fffffaa9bf84", new DeviceTableRow("fffffaa9bf84,ARTEFACT,,HEAL PERMANENT,2,180") },
                { "fffffaa9c52b", new DeviceTableRow("fffffaa9c52b,ARTEFACT,,POISON,2,180") },
                { "fffffaa9c038", new DeviceTableRow("fffffaa9c038,ARTEFACT,,POISON,2,180") }
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
