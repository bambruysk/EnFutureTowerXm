using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace EnFutureTowerXm
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        GameLogic gameLogic;
        Button startButton;
        Button stopButton;
        Chronometer chronometer;
        TextView textViewGameStatus;
        TextView textViewHealth;
        ChronometerControl chronometerControl;
        RadioGroup radioGroup;

        TextView textViewAttackersCount;
        TextView textViewDefendersCount;
        TextView TextViewForce;
        TowerView towerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            ImageView imageViewTower = FindViewById<ImageView>(Resource.Id.imageViewTower);
            towerView = new TowerView(imageViewTower);

            gameLogic = new GameLogic();
            gameLogic.tower.HPChanged += towerView.OnHPChanged;
            gameLogic.GameStateChanged += (s, e) => { textViewGameStatus.Text = e.statusText; };

            chronometer = FindViewById<Chronometer>(Resource.Id.chronometer1);
            chronometerControl = new ChronometerControl(chronometer, 10 * 60 * 1000);

            gameLogic.GameStateChanged += chronometerControl.GameStateChangedHandler;
            gameLogic.GameTick += GameLogic_GameTick;

            textViewGameStatus = FindViewById<TextView>(Resource.Id.textViewGameStatus);

            textViewHealth = FindViewById<TextView>(Resource.Id.textViewSimpleHealth);
            textViewHealth.Text = gameLogic.Max_HP.ToString();
            gameLogic.tower.HPChanged += (s, e) => { textViewHealth.Text = e.HP.ToString(); };


            startButton = FindViewById<Button>(Resource.Id.buttonStartPause);
            stopButton = FindViewById<Button>(Resource.Id.buttonStop);

            startButton.Click += StartButton_Click;
            startButton.Text = "Старт";

            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup1);


            textViewAttackersCount = FindViewById<TextView>(Resource.Id.textViewAttackCount);
            textViewDefendersCount = FindViewById<TextView>(Resource.Id.textViewDefenceCount);
            TextViewForce = FindViewById<TextView>(Resource.Id.textViewForce);

            gameLogic.finder.bLEScanner.adapter.DeviceDiscovered += Adapter_DeviceDiscovered;


        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            Toast toast = Toast.MakeText(this, "Device Found" + e.Device.Id.ToString(), ToastLength.Long );
            toast.Show();
        }

        private void GameLogic_GameTick(object sender, GameTickEventArgs e)
        {
            textViewAttackersCount.Text = e.attackersCount.ToString();
            textViewDefendersCount.Text = e.defendersCount.ToString();
            TextViewForce.Text = e.currentForce.ToString();
            textViewHealth.Text = gameLogic.tower.HP.ToString();
        }

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
           
            if(gameLogic.gameState == GameState.IDLE)
            {

                switch (radioGroup.CheckedRadioButtonId)
                {
                    case Resource.Id.radioButtonRed:
                        gameLogic.SetTeam(new Team(Team.TeamColor.RED));
                        break;
                    case Resource.Id.radioButtonGreen:
                        gameLogic.SetTeam(new Team(Team.TeamColor.BLUE));
                        break;
                    default:
                        gameLogic.SetTeam(new Team(Team.TeamColor.RED));
                        break;
                }
                startButton.Text = "Стоп";
                gameLogic.StartGame();
            }
            else if (gameLogic.gameState == GameState.PLAY)
            {

                startButton.Text = "Старт";
                gameLogic.StopGame();
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
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}

