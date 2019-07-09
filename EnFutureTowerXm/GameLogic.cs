using System;
using System.Collections.Generic;
using System.Timers;

namespace EnFutureTowerXm
{
    public enum GameState { IDLE, WAITING_1, PLAY_1, WAITING_2, PLAY_2, WAITING_3, PLAY_3, FINAL }
    public class GameLogic
    {
        // MAin game parameters
        public int Max_HP;
        public int HP;

        // Interval for game tick in ms
        public int updateInterval;

        // Current Force. It will be diifferens between attacker and defencer count;
        public decimal currentForce;
        public int currentAttackersCount;
        public int currentDefendersCount;


        public int currentHeal;
        public int currentPoison;

        private Timer timer;
        public const int gameTickPeriod = 1000; // in ms



        




        // Game State. 

        public GameState gameState;

        public GameState defaultState = GameState.WAITING_1;


        public delegate void GameStateChangeHandler(object sender, GameStateChangedEventArgs e);

        public event GameStateChangeHandler GameStateChanged;

        public delegate void GameTickHandler(object sender, GameTickEventArgs e);

        public event GameTickHandler GameTick;

        public long gameTimePeriod;

        public long currentGameTime;



        public List<IActor> actors;

        public Dictionary<GameState, int> timings;

        private Timer secondTimer;

        public GameRound gameRound;

        public GameInfo GameInfo;

        public GameLogic()
        {

            //           station = new Station(); 


            gameState = GameState.WAITING_1;
          
            secondTimer = new Timer(1000);
            secondTimer.Elapsed += SecondTimer_Elapsed;
            secondTimer.Start();

            currentAttackersCount = 0;
            currentDefendersCount = 0;

            GameInfo = new GameInfo(0, 0, 0,0);

            timings = new Dictionary<GameState, int>
            {
                { GameState.IDLE, 0 },
                { GameState.PLAY_1, 60 },
                { GameState.PLAY_2, 60 },
                { GameState.PLAY_3, 60 },
                { GameState.WAITING_1, 120 },
                { GameState.WAITING_2, 120 },
                { GameState.WAITING_3, 120 },
                { GameState.FINAL, 300 }
            };

            ///GameStateChanged(this, new GameStateChangedEventArgs(gameState));

        }
        /*
        private void Finder_ActorsFound(object sender, ActorsFoundEventArgs e)
        {

            Tick(e.Actors);


        }
        */
        private void SecondTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            currentGameTime++;
            if (currentGameTime >= timings[gameState])
            {
                NextState();
            }
            GameInfo = (gameRound == null) ? new GameInfo(0, 0, 0, 0) : gameRound.GameInfo;
            //Tick();
        }
       
        private void NextState()
        {
            
            currentGameTime = 0;
            switch (gameState)
            {
                case GameState.WAITING_1:
                    gameState = GameState.PLAY_1;
                    // station = new Station();
                    gameRound = new GameRound();
                    break;
                case GameState.PLAY_1:
                    gameState = GameState.WAITING_2;
                    gameRound = null;
                    break;
                case GameState.WAITING_2:
                    gameState = GameState.PLAY_2;
                    // station = new Station();
                    gameRound = new GameRound();
                    break;
                case GameState.PLAY_2:
                    gameState = GameState.WAITING_3;
                    gameRound = null;
                    break;
                case GameState.WAITING_3:
                    gameState = GameState.PLAY_3;
                    // station = new Station();
                    gameRound = new GameRound(); break;
                case GameState.PLAY_3:
                    gameState = GameState.FINAL;
                    gameRound = null;
                    break;
                case GameState.FINAL:
                    gameRound = null;
                    break;

            }
            GameStateChanged(this, new GameStateChangedEventArgs(gameState));
        }


        public void Waiting()
        {

        }

        //public void StartGame()
        //{
        //    currentGameTime = 10 * 60 * 1000;
        //    finder.FindStart();
        //    gameState = GameState.WAITING_1;
        //    GameStateChanged(this, new GameStateChangedEventArgs(gameState));
        //}



/*
        // Call every game cycle
        public void Tick(Dictionary<string,IActor> actors)
        {
            if (station == null)
            {
                GameTick(this, new GameTickEventArgs(
                    0,
                    0,
                    0,
                    null
                    )
                );
                return;
            }
            ///finder.Update();
            var actorsList = new List<IActor>(actors.Values);
            currentForce = GetCurrentForce(actorsList);
            if (currentForce == 0)
            {
                return;
            }
            else
            {
                station.ApplyImpact(currentForce);
                if (station.IsOwned())
                {
                    NextState();
                }
            }
        }

        public decimal GetCurrentForce(List<IActor> actors)
        {
            int currentDefendersCount = 0;
            int currentAttackersCount = 0;

            foreach (var a in actors)
            {
                if (a is Player player)
                {
                    Console.WriteLine("pLAYER FOUN OF {0} an out team is {1}", player.Team.Color.ToString(), " pizdec");
                    // Console.WriteLine("Our team is {0}", GetTeam().Color.ToString());
                    if (player.Team.Color == Team.TeamColor.RED)
                    {

                        currentDefendersCount++;
                    }
                    else
                    {
                        currentAttackersCount++;
                    }
                }
            }

            decimal _currentForce = .2m * (currentAttackersCount - currentDefendersCount);

            GameTick(this, new GameTickEventArgs(
                currentAttackersCount,
                currentDefendersCount,
                _currentForce,
                station
                )
           );

            this.currentAttackersCount = currentAttackersCount;
            this.currentDefendersCount = currentDefendersCount;
            this.currentForce = _currentForce;

            //     GameTick(this, new GameTickEventArgs(3, 4, 5));

            return _currentForce;
        }
    */


    }
}