using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SherbetUniversalReturn.Models;
using UnityEngine;

namespace SherbetUniversalReturn.Commands
{
    public class ReturnCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Both;

        public string Name => "Return";

        public string Help => "Returns a player to their previous position";

        public string Syntax => "Return [Player]";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string>() { "SherbetUniversalReturn.Return" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length >= 1)
            {
                UnturnedPlayer targetPlayer = UnturnedPlayer.FromName(command[0]);
                if (targetPlayer == null)
                {
                    UnturnedChat.Say(caller, "Failed to find player.");
                    return;
                }
                LocNode LastNode = targetPlayer.Player.gameObject.getOrAddComponent<LocationHistory>().LastNode;
                if (LastNode == LocNode.Empty)
                {
                    UnturnedChat.Say(caller, "Player does not have any recorded locations.");
                    return;
                }
                if (targetPlayer.Player.teleportToLocation(LastNode.Position, LastNode.Yaw))
                {
                    UnturnedChat.Say(caller, "Returned player to their previous position");
                    UnturnedChat.Say(targetPlayer, "You were returned to your previous location");
                } else
                {
                    UnturnedChat.Say(caller, "Player's previous location is blocked.");
                }
            } else
            {
                UnturnedChat.Say(caller, Syntax);
            }
        }
    }
}
