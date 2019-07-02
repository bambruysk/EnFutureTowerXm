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
    class Artefact : IActor
    {
        public enum ArtefactType { HEAL_ONETEME, HEAL_PERMANENT, BOMB, POISON }
        public ArtefactType Type { get; }


        // CoolDown 
        public int cooldown;
        public int powerValue;


        public Artefact (ArtefactType type, int powerValue, int cooldown )
        {
            Type = type;
        }


    }
}