using System.Reflection;
using HarmonyLib;
using Rocket.API;
using Rocket.Core.Plugins;
using SDG.Unturned;
using SherbetUniversalReturn.Models;
using UnityEngine;

namespace SherbetUniversalReturn
{
    public class UniversalReturn : RocketPlugin
    {
        private Harmony HarmonyInstance;

        public override void LoadPlugin()
        {
            base.LoadPlugin();
            HarmonyInstance = new Harmony("UniversalReturn");
            MethodInfo TeleportMethod = typeof(Player).GetMethod("teleportToLocationUnsafe");
            MethodInfo Prefix = typeof(UniversalReturn).GetMethod("TeleportPatch");
            PatchProcessor processor = HarmonyInstance.CreateProcessor(TeleportMethod);
            processor.AddPrefix(Prefix);
            processor.Patch();
            Rocket.Unturned.Events.UnturnedPlayerEvents.OnPlayerDeath += UnturnedPlayerEvents_OnPlayerDeath;
        }

        private void UnturnedPlayerEvents_OnPlayerDeath(Rocket.Unturned.Player.UnturnedPlayer player, EDeathCause cause, ELimb limb, Steamworks.CSteamID murderer)
        {
            player.Player.gameObject.getOrAddComponent<LocationHistory>().AddNew(player.Position, player.Player.look.yaw);
        }

        public static void TeleportPatch(Player __instance, Vector3 position, float yaw)
        {
            __instance.gameObject.getOrAddComponent<LocationHistory>().AddNew(__instance.transform.position, __instance.look.yaw);
        }

        public override void UnloadPlugin(PluginState state = PluginState.Unloaded)
        {
            base.UnloadPlugin(state);
            HarmonyInstance.UnpatchAll("UniversalReturn");
        }
    }
}