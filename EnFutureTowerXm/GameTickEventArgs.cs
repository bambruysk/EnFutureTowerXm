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
        public GameTickEventArgs(int attackersCount, int defendersCount, int currentForce)
        {
            this.attackersCount = attackersCount;
            this.defendersCount = defendersCount;
            this.currentForce = currentForce;
        }

        public int attackersCount { get; }
        public int defendersCount { get; }
        public int currentForce { get; }

        

    }
}