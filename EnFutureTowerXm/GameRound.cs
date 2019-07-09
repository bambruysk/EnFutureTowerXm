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
    public class GameRound
    {
        public ActorFinder finder;
        public Station station;
        public Timer timer;

        private GameInfo gameInfo;

        public bool isDone;

        public int currentDefendersCount;
        public int currentAttackersCount;
        public decimal currentForce;

        public GameInfo GameInfo { get => gameInfo; }

        public GameRound()
        {
            finder = new ActorFinder();
            station = new Station();

            finder.FindStart();
            finder.ActorsFound += Finder_ActorsFound;
            isDone = false;

            currentAttackersCount = 0;
            currentDefendersCount = 0;
            currentForce = 0;

            gameInfo = new GameInfo(0, 0, 0, 0);

        }



        private void Finder_ActorsFound(object sender, ActorsFoundEventArgs e)
        {
            Tick(e.Actors);
        }

        public void Tick(Dictionary<string, IActor> actors)
        {
            ///finder.Update();
            var actorsList = new List<IActor>(actors.Values);
            var currentForce = GetCurrentForce(actorsList);
            if (currentForce == 0)
            {
                return;
            }
            else
            {
                station.ApplyImpact(currentForce);
                if (station.IsOwned())
                {
                    isDone = true;
                }
            }
            gameInfo = new GameInfo(currentAttackersCount, currentDefendersCount, currentForce,station.HP);
        }
        public decimal GetCurrentForce(List<IActor> actors)
        {
            int currentDefendersCount = 0;
            int currentAttackersCount = 0;

            foreach (var a in actors)
            {
                if (a is Player player)
                {
                    
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

            decimal _currentForce = 1m * (currentAttackersCount - currentDefendersCount);
            
           

            this.currentAttackersCount = currentAttackersCount;
            this.currentDefendersCount = currentDefendersCount;
            this.currentForce = _currentForce;
            
            //     GameTick(this, new GameTickEventArgs(3, 4, 5));

            /*
             * Aplly artefactes
             */
            return _currentForce;
        }



    }
}
    