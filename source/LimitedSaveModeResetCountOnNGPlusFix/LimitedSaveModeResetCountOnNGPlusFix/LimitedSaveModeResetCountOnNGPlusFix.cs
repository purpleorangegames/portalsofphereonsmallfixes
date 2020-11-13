

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

namespace LimitedSaveModeResetCountOnNGPlusFix
{

    [BepInPlugin(GUID, Name, Version)]
    public class LimitedSaveModeResetCountOnNGPlusFix : BaseUnityPlugin
    {
        public const string GUID = "net.pog.portalsofphereon.mod.LimitedSaveModeResetCountOnNGPlusFix";
        public const string Name = "Limited Save Mode Reset Count On NGPlus Fix";
        public const string Version = "1.0";

        private void Start()
        {
            UnityEngine.Debug.Log(Name + " loaded (" + GUID + ")");

            var harmony = new Harmony(GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //Set Halloween flag regardless of date
    [HarmonyPatch(typeof(SaveController))]
    [HarmonyPatch("startNewGamePlus")]
    static class LimitedSaveModeResetCountOnNGPlusFix_Patch
	{
		static void Postfix(SaveController __instance)
		{
			if (__instance.difficulty.saveType == SaveType.Limited)
			{
				switch (__instance.difficulty.gameLength)
				{
					case GameLength.QuickRun:
						__instance.difficulty.loadingLeft = 5;
						break;
					case GameLength.VeryShort:
						__instance.difficulty.loadingLeft = 5;
						break;
					case GameLength.Short:
						__instance.difficulty.loadingLeft = 10;
						break;
					case GameLength.Medium:
						__instance.difficulty.loadingLeft = 15;
						break;
					case GameLength.Long:
						__instance.difficulty.loadingLeft = 20;
						break;
					case GameLength.VeryLong:
						__instance.difficulty.loadingLeft = 25;
						break;
					case GameLength.FreePlay:
						__instance.difficulty.loadingLeft = 50;
						break;
				}
			}
		}
	}
}
