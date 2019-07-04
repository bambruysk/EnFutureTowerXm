using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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

        public bool immuneStatus;

        private Timer countDownTimer;

        public int FreezeElapsedTime;

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
            if (!immuneStatus)
            {
                HP -= damage;
                if (HP < 0)
                {
                    isDead = true;
                }
                HPChanged?.Invoke(this, new TowerHPChangeEventArgs(HP));
            }
        }

        public void Heal(int heal)
        {
            if (!immuneStatus)
            {
                HP += heal;
                if (HP > Max_HP)
                {
                    HP = Max_HP;
                }

                HPChanged?.Invoke(this, new TowerHPChangeEventArgs(HP));
            }
        }

        public void Freeze (int freezeTime)
        {
            FreezeElapsedTime = freezeTime;
            immuneStatus = true;
            countDownTimer = new Timer(1000);
            countDownTimer.AutoReset = false;
            countDownTimer.Elapsed += (s, e) => {
                freezeTime--;
                if (freezeTime == 0)
                {
                    countDownTimer.Stop();
                    immuneStatus = false;
                }
            };
            countDownTimer.Start();
        }
    }
}