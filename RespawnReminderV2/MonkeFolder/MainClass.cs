using System;
using System.IO;
using PluginFramework;
using PluginFramework.Logging;
using UnityEngine;
using HarmonyLib;
using Newtonsoft.Json;
namespace RespawnReminderV2
{
    public class MainClass : Plugin
    {
        public static Harmony inst;

        public static MainClass.ReminderConfig config;

        public override void OnDisable()
        {
            MainClass.inst.UnpatchAll(null);
            MainClass.inst = null;
        }

        public override void OnEnable()
        {
            PluginSystem.Manager.Logger.Info("customTextTimer", "INIT");
            MainClass.inst = new Harmony("customTextTimer");
            MainClass.inst.PatchAll();
            string str = Path.Combine(Application.dataPath, "../settings/customTextTimer.json");
            if (!File.Exists(str))
            {
                File.WriteAllText(str, JsonConvert.SerializeObject(new MainClass.ReminderConfig()));
            }

            MainClass.config = JsonConvert.DeserializeObject<MainClass.ReminderConfig>(File.ReadAllText(str));
        }

        public class ReminderConfig
        {
            public string MessageFormat { get; set; } = "This is default, please add your message";

            public float TimerMessageInterval { get; set; } = 60;

            public string MessageColor { get; set; } = "#FFFF00";
        }
    }
}
