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

    /// <summary>
    /// Make actrors such as Player and Artefact 
    /// </summary>
    public class ActorFactory
    {
        public IActor NewActor(DeviceTableRow row)
        {
            if (row.Type == "PLAYER")
            {
                if (row.Team == "RED")
                {
                    return new Player(new Team(Team.TeamColor.RED));
                }
                else if (row.Team == "BLUE")
                {
                    return new Player(new Team(Team.TeamColor.BLUE));
                }
            }
            else if (row.Type == "ARTEFACT")
            {
                var value = row.PowerValue;
                var cooldown = row.Cooldown;
                if (row.ArtefactType == "HEAL ONETIME")
                {
                    return new Artefact(Artefact.ArtefactType.HEAL_ONETEME, value, cooldown);
                }
                if (row.ArtefactType == "BOMB")
                {
                    return new Artefact(Artefact.ArtefactType.BOMB, value, cooldown);
                }
                if (row.ArtefactType == "HEAL PERMANENT")
                {
                    return new Artefact(Artefact.ArtefactType.HEAL_PERMANENT, value, cooldown);
                }
                if (row.ArtefactType == "POISON")
                {
                    return new Artefact(Artefact.ArtefactType.POISON, value, cooldown);
                }
                if (row.ArtefactType == "IMMUNE")
                {
                    return new Artefact(Artefact.ArtefactType.IMMUNE, value, cooldown);
                }

            }
            return null;
        }
    }
}