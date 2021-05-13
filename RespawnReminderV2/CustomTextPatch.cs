using System;
using System.Collections.Generic;
using System.Text;
using VirtualBrightPlayz.SCP_ET;
using UnityEngine;
using PluginFramework;
using HarmonyLib;
using RespawnReminderV2;
using System.Threading.Tasks;
using VirtualBrightPlayz.SCP_ET.ServerConsole;
using UnityEngine;
using System.Collections;
using Mirage;
using VirtualBrightPlayz.SCP_ET.Player;
using VirtualBrightPlayz.SCP_ET.World.Events;

namespace CustomTextTimer
{
    [HarmonyPatch(typeof(IntroTeleport), nameof(IntroTeleport.StartRound))]
    public static class CustomTextPatch
    {
        private const int MonkeDelay = 300000;
        public static void Postfix(IntroTeleport __instance)
        {
           // Loop(__instance);

        }
    }
    [HarmonyPatch(typeof(RoundEnd), nameof(RoundEnd.Update))]
    public static class UpdatePatch
    {
        public static float timer = 0;
        public static void Postfix(RoundEnd __instance)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = MainClass.config.TimerMessageInterval;

                GameObject.FindObjectOfType<PlayerMain>().textChat.RpcAddMessage(MainClass.config.MessageFormat, MainClass.config.MessageColor);
            }
        }
    }
}
