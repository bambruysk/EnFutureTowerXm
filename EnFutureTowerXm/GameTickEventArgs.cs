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

    public class GameTickEventArgs
    {
        public GameTickEventArgs(int attackersCount, int defendersCount, double currentForce, Station station)
        {
            AttackersCount = attackersCount;
            DefendersCount = defendersCount;
            CurrentForce = currentForce;
            Station = station;

        }

        public int AttackersCount { get; }
        public int DefendersCount { get; }
        public double CurrentForce { get; }
        public Station Station { get; }




    }
}