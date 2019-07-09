﻿using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Timers;

namespace EnFutureTowerXm
{
    [Activity(Label = "Раздача Артефактов", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        GameLogic gameLogic;
        Button startButton;

        Chronometer chronometer;
        TextView textViewGameStatus;

        TextView textViewHealth;
        ChronometerControl chronometerControl;

        TextView textViewAttackersCount;
        TextView textViewDefendersCount;
        TextView TextViewForce;

        ArtefactListView artefactListView;
        RadioGroup radioGroup;

        TextView textViewGameHistory;

        ProgressBar progressBar;

        Timer uiTimer;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            gameLogic = null;

            uiTimer = new Timer(99);
            uiTimer.Elapsed += UiTimer_Elapsed;
            uiTimer.Start();
            var redColor = Android.Graphics.Color.Red;
            var blueColor = Android.Graphics.Color.Blue;

            chronometer = FindViewById<Chronometer>(Resource.Id.chronometer1);

            textViewHealth = FindViewById<TextView>(Resource.Id.textViewSimpleHealth);
            textViewHealth.Text = "Стоп";

            startButton = FindViewById<Button>(Resource.Id.buttonStartPause);

            startButton.Click += StartButton_Click;
            startButton.Text = "Старт";

            textViewAttackersCount = FindViewById<TextView>(Resource.Id.textViewAttackCount);

            textViewAttackersCount.SetTextColor(blueColor);


            textViewDefendersCount = FindViewById<TextView>(Resource.Id.textViewDefenceCount);
            textViewDefendersCount.SetTextColor(redColor);

            TextViewForce = FindViewById<TextView>(Resource.Id.textViewForce);

            artefactListView = new ArtefactListView(this);
            artefactListView.UpdateView();

            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            progressBar.Indeterminate = false;
            progressBar.Max = 50;
            progressBar.Progress = 25;
            textViewGameStatus = FindViewById<TextView>(Resource.Id.textViewGameStatus);

        }

        private void GameLogic_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            chronometerControl = new ChronometerControl(chronometer, gameLogic.timings[e.state], true);


            switch (e.state)
            {
                case GameState.WAITING_1:
                    artefactListView.UpdateView();
                    break;
                case GameState.WAITING_2:
                    artefactListView.Next1();
                    break;

                case GameState.WAITING_3:
                    artefactListView.Next2();
                    break;
            }
        }

        private void UiTimer_Elapsed(object sender, ElapsedEventArgs e)
        {


            RunOnUiThread(() =>
            {
                if (gameLogic == null)
                    return;
                var gameInfo = gameLogic.GameInfo;
                textViewAttackersCount.Text = gameInfo.AttackersCount.ToString();
                textViewDefendersCount.Text = gameInfo.DefendersCount.ToString();
                TextViewForce.Text = (-gameInfo.SummaryForce).ToString();
                textViewHealth.Text = (gameLogic.gameRound == null) ? "Ждем" : gameLogic.GameInfo.HP.ToString();
                progressBar.Max = 50;
                progressBar.Progress = (int)gameInfo.HP;

                var listGameStatusText = new Dictionary<GameState, string> {
                    { GameState.IDLE, "Остановлена" },
                    { GameState.PLAY_1, "Первый раунд"},
                    { GameState.PLAY_2, "Второй раунд" },
                    { GameState.PLAY_3, "Третий раунд" },
                    { GameState.WAITING_1, "Ждем первый раунд" },
                    { GameState.WAITING_2, "Ждем второй раунд" },
                    { GameState.WAITING_3, "Ждем третий раунд" },
                    { GameState.FINAL, "Окончание" }
                };

                textViewGameStatus.Text = listGameStatusText[gameLogic.gameState] + " " + gameLogic.gameState.ToString();

                //textViewGameStatus.Text = gameLogic.gameState.ToString();

                if (gameInfo.HP > 0)
                {
                    progressBar.ProgressDrawable.SetColorFilter(Android.Graphics.Color.Blue, Android.Graphics.PorterDuff.Mode.SrcIn);
                }
                else
                {
                    progressBar.ProgressDrawable.SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.SrcIn);

                }
            });
        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            Toast toast = Toast.MakeText(this, "Device Found" + e.Device.Id.ToString(), ToastLength.Long);
            toast.Show();
        }

        private void GameLogic_GameTick(object sender, GameTickEventArgs e)
        {

            /*           textViewAttackersCount.Text = e.AttackersCount.ToString();
                       textViewDefendersCount.Text = e.DefendersCount.ToString();
                       TextViewForce.Text = (-e.CurrentForce).ToString();
                       textViewHealth.Text = (e.Station == null) ? "0": e.Station.HP.ToString();
                       progressBar.Max = (e.Station == null) ? 50 : (int)(e.Station.Max_HP);
                       progressBar.Progress = (e.Station == null) ? 0 : (int)(Math.Abs(e.Station.HP));

                       var listGameStatusText = new Dictionary<GameState, string> {
                           { GameState.IDLE, "Остановлена" },
                           { GameState.PLAY_1, "Первый раунд"},
                           { GameState.PLAY_2, "Второй раунд" },
                           { GameState.PLAY_3, "Третий раунд" },
                           { GameState.WAITING_1, "Ждем первый раунд" },
                           { GameState.WAITING_2, "Ждем второй раунд" },
                           { GameState.WAITING_3, "Ждем третий раунд" },
                           { GameState.FINAL, "Окончание" }
                       };

                       textViewGameStatus.Text = listGameStatusText[gameLogic.gameState] + " " + gameLogic.gameState.ToString();

                       //textViewGameStatus.Text = gameLogic.gameState.ToString();

                       if (e.Station.HP > 0)
                       {
                           progressBar.ProgressDrawable.SetColorFilter(Android.Graphics.Color.Blue, Android.Graphics.PorterDuff.Mode.SrcIn);
                       } else
                       {
                           progressBar.ProgressDrawable.SetColorFilter(Android.Graphics.Color.Red, Android.Graphics.PorterDuff.Mode.SrcIn);

                       }*/
        }

        /// <summary>
        /// ///////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /*
                private void Chronometer_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
                {
                    if (chronometer.Base == SystemClock.ElapsedRealtime())
                    {
                        gameLogic.StopGame();
                    }
                }
        */

        private void StartButton_Click(object sender, EventArgs e)
        {

            if (gameLogic == null)
            {

                gameLogic = new GameLogic();
                chronometerControl = new ChronometerControl(chronometer, gameLogic.timings[gameLogic.defaultState], true);
                startButton.Text = "Стоп";
               // gameLogic.finder.bLEScanner.adapter.DeviceDiscovered += Adapter_DeviceDiscovered;
                gameLogic.GameStateChanged += GameLogic_GameStateChanged;
                textViewGameStatus.Text = "Игра начата";
                artefactListView.Show();
            }
            else
            {

                startButton.Text = "Старт";
                gameLogic.GameStateChanged -= GameLogic_GameStateChanged;
                gameLogic.GameTick -= GameLogic_GameTick;
                chronometerControl = null;
                gameLogic = null;

            }

        }



        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
    }
}

