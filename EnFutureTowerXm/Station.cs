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
    public class Station
    {
        // if HP more than 0  it red player owner, Other blue

        public decimal HP;

        public int redImpact;
        public int blueImpact;

        public Team Owner;

        public decimal Max_HP = 50;

        public Station()
        {
            HP = 0;
        }

        public void ApplyImpact(decimal value)
        {
            HP += value;
            if (HP > Max_HP)
            {
                HP = Max_HP;
            }
            if (HP < -Max_HP)
            {
                HP = -Max_HP;
            }
        }

        public bool IsOwned()
        {
            return (Math.Abs(HP) >= Max_HP);
        }

    }
}