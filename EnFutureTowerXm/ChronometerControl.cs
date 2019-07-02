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
    public class ChronometerControl
    {
        private Chronometer chronometer;
        private long maxTime;

        public long currentTime;

        public ChronometerControl(Chronometer chronometer, long maxTime)
        {
            this.chronometer = chronometer;
            this.maxTime = maxTime;
            currentTime = maxTime;
            chronometer.CountDown = true;
            chronometer.ChronometerTick += Chronometer_ChronometerTick;
        }

        public void Start()
        {
            chronometer.Base = SystemClock.ElapsedRealtime() + currentTime;
            chronometer.Start();
        }

        private void Chronometer_ChronometerTick(object sender, EventArgs e)
        {
            currentTime--;
            if (currentTime == 0)
            {
                Stop();
            }
        }

        public void Stop ()
        {
            chronometer.Stop();
        }

        public void Pause ()
        {
            chronometer.Stop();
        }

        
        public void GameStateChangedHandler(object sender, GameStateChangedEventArgs e)
        {
            switch (e.state)
            {
                case GameState.IDLE:
                    {
                        Stop();
                        break;
                    }
                case GameState.PAUSE:
                    {
                        Pause();
                        break;
                    }
                case GameState.PLAY:
                    {
                        Start();
                        
                        break;
                    }  
            }
        }
    }
}