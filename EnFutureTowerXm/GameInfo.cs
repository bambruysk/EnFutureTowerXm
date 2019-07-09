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
    public class GameInfo
    {
        public int AttackersCount;
        public int DefendersCount;
        public decimal SummaryForce;
        public decimal HP;

     
        public GameInfo(int attackersCount, int defendersCount, decimal summaryForce, decimal HP)
        {
            AttackersCount = attackersCount;
            DefendersCount = defendersCount;
            SummaryForce = summaryForce;
            this.HP = HP;
            
        }


    }
}