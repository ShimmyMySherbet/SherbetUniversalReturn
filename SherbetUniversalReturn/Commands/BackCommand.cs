using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SherbetUniversalReturn.Models;
using UnityEngine;

namespace SherbetUniversalReturn.Commands
{
    public class BackCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "Back";

        public string Help => "Returns you to your previous location";

        public string Syntax => "Back";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "SherbetUniversalReturn.Back" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if (player.Player.gameObject.getOrAddComponent<LocationHistory>().Nodes.Count == 0)
            {
                UnturnedChat.Say(caller, "You do not have recorded locations!");
            } else
            {
                LocNode prevNode = player.Player.gameObject.getOrAddComponent<LocationHistory>().LastNode;
                if (player.Player.teleportToLocation(prevNode.Position, prevNode.Yaw))
                {
                    UnturnedChat.Say(caller, "Returned to previous location.");
                } else
                {
                    UnturnedChat.Say(caller, "Failed to return to previous location; location blocked..");
                }
            }
        }
    }
}
