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
    public class Tower
    {
        public int Max_HP;
        public int HP;

        public bool isDead;

        public delegate void TowerHPChangeHandler(object sender, TowerHPChangeEventArgs e);

        public event TowerHPChangeHandler HPChanged;

        public Tower(int max_hp)
        {
            Max_HP = max_hp;
            HP = max_hp;
            isDead = false;
        }

        public void ApplyDamage(int damage)
        {

            HP -= damage;
            if (HP < 0)
            {
                isDead = true;
            }
            if (HPChanged != null)
                HPChanged(this, new TowerHPChangeEventArgs(HP));
        }

        public void Heal(int heal)
        {
            HP += heal;
            if (HP > Max_HP)
                HP = Max_HP;
            if (HPChanged != null)
                HPChanged(this, new TowerHPChangeEventArgs(HP));
        }
    }
}