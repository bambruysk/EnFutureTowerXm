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
    public class Player : IActor
    {
        public Team Team { get; }
        public int timeout;
        public Player (Team team)
        {
            timeout = 5;
            Team = team;
        }

        public void TickTimeout()
        {
            timeout--; 
        }

        public int GetTimeout()
        {
            return timeout;
        }
    }
}