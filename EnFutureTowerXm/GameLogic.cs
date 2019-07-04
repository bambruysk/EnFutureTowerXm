using System;
using System.Collections.Generic;
using System.Timers;

namespace EnFutureTowerXm
{
    public enum GameState { IDLE, PLAY }
    public class GameLogic
    {
        // MAin game parameters
        public int Max_HP;
        public int HP;
        // Tower
        public Tower tower;
        // Interval for game tick in ms
        public int updateInterval;

        // Current Force. It will be diifferens between attacker and defencer count;
        public int currentForce;
        public int currentAttackersCount;
        public int currentDefendersCount;
        private Team team;

        public int currentHeal;
        public int currentPoison;

        private Timer timer;
        public const int gameTickPeriod = 1500; // in ms

        // Current Owner
        public Team GetTeam()
        {
            return team;
        }

        // Current Owner
        public void SetTeam(Team value)
        {
            if (gameState == GameState.IDLE)
            {
                team = value;
            }
        }

        // Game State. 

        public GameState gameState;

        public delegate void GameStateChangeHandler(object sender, GameStateChangedEventArgs e);

        public event GameStateChangeHandler GameStateChanged;

        public delegate void GameTickHandler(object sender, GameTickEventArgs e);

        public event GameTickHandler GameTick;

        public long gameTimePeriod;

        public long currentGameTime;

        private List<IActor> disabled;

        public List<IActor> actors;

        private List<string> used_artifacts;

        public ActorFinder finder;

        public GameLogic(Team myteam)
        {
            Max_HP = 100;
            HP = Max_HP;
            tower = new Tower(Max_HP);

            gameTimePeriod = 10 * 60 * 1000;

            gameState = GameState.IDLE;
            finder = new ActorFinder();

            timer = new Timer(gameTickPeriod);

            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            currentAttackersCount = 0;
            currentDefendersCount = 0;
            team = myteam;

            used_artifacts = new List<string>();

            //Tick();

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (gameState == GameState.PLAY)
            {
                Tick();
            }
        }

        public void StartGame()
        {
            currentGameTime = 10 * 60 * 1000;
            finder.FindStart();
            gameState = GameState.PLAY;
            GameStateChanged(this, new GameStateChangedEventArgs("Игра запущена", GameState.PLAY));
        }



        public void StopGame()
        {
            finder.FindStop();
            gameState = GameState.IDLE;
            GameStateChanged(this, new GameStateChangedEventArgs("Игра остановлена", GameState.IDLE));

        }

        public void ResumeGame()
        {
            finder.FindStart();
            gameState = GameState.PLAY;
            GameStateChanged(this, new GameStateChangedEventArgs("Игра запущена", GameState.PLAY));
        }

        // Call every game cycle
        public void Tick()
        {
            finder.Update();
            actors = new List<IActor>(finder.GetActors().Values);
            currentForce = GetCurrentForce(actors);
            if (currentForce == 0)
            {
                return;
            }
            else
            {

                if (currentForce > 0)
                {
                    tower.ApplyDamage(currentForce);
                }
                // If heal is Enabled
                else
                {
                    tower.Heal(-currentForce);
                }
                if (tower.isDead)
                {
                    StopGame();
                }
            }
        }

        public int GetCurrentForce(List<IActor> actors)
        {
            int currentDefendersCount = 0;
            int currentAttackersCount = 0;
            int artefactForce = 0;
            currentHeal = 0;
            currentPoison = 0;
            int bombDamageValue = 0;
            int HealOneTime = 0;
            foreach (var a in actors)
            {
                if (a is Player player)
                {
                    Console.WriteLine("pLAYER FOUN OF {0} an out team is {1}", player.Team.Color.ToString(), " pizdec");
                    Console.WriteLine("Our team is {0}", GetTeam().Color.ToString());
                    if (player.Team.Color == GetTeam().Color)
                    {

                        currentDefendersCount++;
                    }
                    else
                    {
                        currentAttackersCount++;
                    }
                }
                else if (a is Artefact artefact)
                {
                    switch (artefact.Type)
                    {
                        case Artefact.ArtefactType.BOMB:

                            if (!used_artifacts.Contains(artefact.Id))
                            {
                                artefactForce += artefact.powerValue;
                                bombDamageValue += artefact.powerValue;
                                used_artifacts.Add(artefact.Id);
                            }
                            break;
                        case Artefact.ArtefactType.HEAL_ONETEME:

                            if (!used_artifacts.Contains(artefact.Id))
                            {
                                artefactForce -= artefact.powerValue;
                                HealOneTime += artefact.powerValue;
                                used_artifacts.Add(artefact.Id);
                            }
                            break;
                        case Artefact.ArtefactType.HEAL_PERMANENT:
                            artefactForce -= artefact.powerValue;
                            currentHeal += artefact.powerValue;
                            break;
                        case Artefact.ArtefactType.POISON:
                            artefactForce += artefact.powerValue;
                            currentPoison += artefact.powerValue;
                            break;
                        case Artefact.ArtefactType.IMMUNE:
                            return 0;

                    }
                }

            }

            int _currentForce = currentAttackersCount - currentDefendersCount + artefactForce;



            GameTick(this, new GameTickEventArgs(
                currentAttackersCount,
                currentDefendersCount,
                _currentForce,
                currentPoison,
                HealOneTime,
                currentHeal,
                bombDamageValue
                )
                );

            //     GameTick(this, new GameTickEventArgs(3, 4, 5));

            /*
             * Aplly artefactes
             */
            return _currentForce;
        }



    }
}