using System;
using System.Drawing;
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
        TextView textViewGameEffects;
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

            gameLogic = null;

            chronometer = FindViewById<Chronometer>(Resource.Id.chronometer1);
            chronometerControl = new ChronometerControl(chronometer, 10 * 60 * 1000);


            textViewGameStatus = FindViewById<TextView>(Resource.Id.textViewGameStatus);
            textViewGameEffects = FindViewById<TextView>(Resource.Id.textViewGameEffects);
            

            textViewHealth = FindViewById<TextView>(Resource.Id.textViewSimpleHealth);
            textViewHealth.Text = "Стоп";


            startButton = FindViewById<Button>(Resource.Id.buttonStartPause);
            stopButton = FindViewById<Button>(Resource.Id.buttonStop);

            startButton.Click += StartButton_Click;
            startButton.Text = "Старт";

            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup1);


            textViewAttackersCount = FindViewById<TextView>(Resource.Id.textViewAttackCount);
            textViewDefendersCount = FindViewById<TextView>(Resource.Id.textViewDefenceCount);
            TextViewForce = FindViewById<TextView>(Resource.Id.textViewForce);




        }

        private void Adapter_DeviceDiscovered(object sender, Plugin.BLE.Abstractions.EventArgs.DeviceEventArgs e)
        {
            Toast toast = Toast.MakeText(this, "Device Found" + e.Device.Id.ToString(), ToastLength.Long );
            toast.Show();
        }

        private void GameLogic_GameTick(object sender, GameTickEventArgs e)
        {
            textViewAttackersCount.Text = e.AttackersCount.ToString();
            textViewDefendersCount.Text = e.DefendersCount.ToString();
            TextViewForce.Text = (-e.CurrentForce).ToString();
            textViewHealth.Text = gameLogic.tower.HP.ToString();
            textViewGameEffects.Text = "Эффекты:" + e.CurrentEffects();

            if (e.Bomb != 0)
            {
                Toast toast = Toast.MakeText(this, "Бомба взорвана!!!", ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }

            if (e.Heal != 0)
            {
                Toast toast = Toast.MakeText(this, "Лечениt!", ToastLength.Long);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();
            }

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
           
            if(gameLogic == null)
            {

                switch (radioGroup.CheckedRadioButtonId)
                {
                    case Resource.Id.radioButtonRed:
                        CreateGameLogic(new Team(Team.TeamColor.RED));
                        break;
                    case Resource.Id.radioButtonGreen:
                        CreateGameLogic(new Team(Team.TeamColor.BLUE));
                        break;
                    default:
                        CreateGameLogic(new Team(Team.TeamColor.RED));
                        break;
                }
                startButton.Text = "Стоп";
                gameLogic.StartGame();
            }
            else 
            {
                gameLogic = null;
                startButton.Text = "Старт";
                //gameLogic.StopGame();
            }

        }

        public void CreateGameLogic(Team team)
        {
            gameLogic = new GameLogic(team);
            gameLogic.GameStateChanged += (s, e) => { textViewGameStatus.Text = e.statusText; };
            gameLogic.tower.HPChanged += towerView.OnHPChanged;
            gameLogic.GameTick += GameLogic_GameTick;
            gameLogic.GameStateChanged += chronometerControl.GameStateChangedHandler;
            gameLogic.tower.HPChanged += (s, e) => { textViewHealth.Text = e.HP.ToString(); };
            gameLogic.finder.bLEScanner.adapter.DeviceDiscovered += Adapter_DeviceDiscovered;
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

