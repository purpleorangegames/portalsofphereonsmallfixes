

using BepInEx;
using HarmonyLib;
using System.Reflection;

//using System.Xml;
//using System.IO;
//using System.Linq;
//using System.Runtime.CompilerServices;

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace DoubleAverageHostilityOfWorld
{

	[BepInPlugin(GUID, Name, Version)]
    public class DoubleAverageHostilityOfWorld : BaseUnityPlugin
    {
        public const string GUID = "net.pog.portalsofphereon.mod.DoubleAverageHostilityOfWorld";
        public const string Name = "Double Average Hostility of World";
        public const string Version = "1.0";

        private void Start()
        {
            UnityEngine.Debug.Log(Name + " loaded (" + GUID + ")");

            var harmony = new Harmony(GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    static public class variables
    {
        static public int multiplier = 2;
        static public List<int> list = new List<int>();
    }

    [HarmonyPatch(typeof(TownManager))]
    [HarmonyPatch("initPortals")]
    static class DoubleAverageHostilityOfWorld_Patch
    {
        static void Prefix(TownManager __instance)
        {
            variables.list.Clear();
            UnityEngine.Debug.Log("initPortals -------------------");
        }
    }

    [HarmonyPatch(typeof(Portal))]
    [HarmonyPatch("getEntryCost")]
    static class DoubleAverageHostilityOfWorld_Patch2
    {
        static void Prefix(Portal __instance)
        {
            foreach (Portal p in SaveController.instance.stabilizedPortals)
                if (!variables.list.Contains(p.world.id))
                {
                    p.world.averageHostility *= variables.multiplier;
                    variables.list.Add(p.world.id);
                    UnityEngine.Debug.Log("added s------------------- "+ p.world.id);
                }

            foreach (Portal p in SaveController.instance.randomPortals)
                if (!variables.list.Contains(p.world.id))
                {
                    p.world.averageHostility *= variables.multiplier;
                    variables.list.Add(p.world.id);
                    UnityEngine.Debug.Log("added r------------------- " + p.world.id);
                }

            foreach (Portal p in SaveController.instance.eventPortals)
                if (!variables.list.Contains(p.world.id))
                {
                    p.world.averageHostility *= variables.multiplier;
                    variables.list.Add(p.world.id);
                    UnityEngine.Debug.Log("added e------------------- " + p.world.id);
                }
        }
    }

    [HarmonyPatch(typeof(SaveLoadManager))]
    [HarmonyPatch("loadGame")]
    static class DoubleAverageHostilityOfWorld_Patch3
    {
        static void Postfix()
        {
            variables.list.Clear();
            UnityEngine.Debug.Log("initPortals loadGame-------------------");
            foreach (Portal p in SaveController.instance.stabilizedPortals)
            {
                variables.list.Add(p.world.id);
                UnityEngine.Debug.Log("added s------------------- " + p.world.id);
            }

            foreach (Portal p in SaveController.instance.randomPortals)
            {
                variables.list.Add(p.world.id);
                UnityEngine.Debug.Log("added r------------------- " + p.world.id);
            }

            foreach (Portal p in SaveController.instance.eventPortals)
            {
                variables.list.Add(p.world.id);
                UnityEngine.Debug.Log("added e------------------- " + p.world.id);
            }
        }
    }
    
}