

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

namespace DuplicatedCharactersOnBattleSummaryFix
{

    [BepInPlugin(GUID, Name, Version)]
    public class DuplicatedCharactersOnBattleSummaryFix : BaseUnityPlugin
    {
        public const string GUID = "net.pog.portalsofphereon.mod.DuplicatedCharactersOnBattleSummaryFix";
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
    [HarmonyPatch(typeof(InterfaceController))]
    [HarmonyPatch("showBattleSummary")]
    static class DuplicatedCharactersOnBattleSummaryFix_Patch
	{
		static void Prefix(InterfaceController __instance)
		{
            for (int i = GameController.instance.battleCharacterInfos.Count-1; i >=0 ; i--)
            {
                BattleCharacterInfo battleCharacterInfo = GameController.instance.battleCharacterInfos[i];

                if (battleCharacterInfo.stats.TownActivity.ToString()=="Reserve")
                { 
                    GameController.instance.battleCharacterInfos.RemoveAt(i);
                }
            }
        }
	}
}
