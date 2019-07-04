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
        public GameTickEventArgs(int attackersCount, int defendersCount, int currentForce, int poison, int heal, int permanentHeal, int bomb)
        {
            AttackersCount = attackersCount;
            DefendersCount = defendersCount;
            CurrentForce = currentForce;
            Poison = poison;
            Heal = heal;
            PermanentHeal = permanentHeal;
            Bomb = bomb;
        }

        public int AttackersCount { get; }
        public int DefendersCount { get; }
        public int CurrentForce { get; }

        public int Poison { get; }
        public int Heal { get; }

        public int PermanentHeal { get; }
        public int Bomb { get; }

        public string CurrentEffects()
        {
            string effects = " ";
            if (Poison != 0)
                effects += " Яд: +" + Poison.ToString();
            if (Heal != 0)
                effects += " Лечение +" + PermanentHeal.ToString();
            if (effects == " ")
                effects = " нет";
            return effects;
        }

    }
}