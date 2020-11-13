

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


namespace PersistentCharacterListAtHome
{

	[BepInPlugin(GUID, Name, Version)]
    public class PersistentCharacterListAtHome : BaseUnityPlugin
    {
        public const string GUID = "net.pog.portalsofphereon.mod.PersistentCharacterListAtHome";
        public const string Name = "Persistent Character List at Home";
        public const string Version = "1.0";

        private void Start()
        {
            UnityEngine.Debug.Log(Name + " loaded (" + GUID + ")");

            var harmony = new Harmony(GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

	public static class storedData
	{
		static int saveSort = 0;
		static bool saveAscending = false;

		static public void saveSortData(int v)
		{
			saveSort = v;
		}
		static public int getSortData()
		{
			return saveSort;
		}
		static public void saveAscendingData(bool v)
		{
			saveAscending = v;
		}
		static public bool getAscendingData()
		{
			return saveAscending;
		}
	}

	[HarmonyPatch(typeof(TownInterfaceController))]
	[HarmonyPatch("showHomeTab")]
	static class PersistentCharacterListAtHome_Patch
	{
		static void Prefix(TownInterfaceController __instance)
		{
			storedData.saveSortData(Traverse.Create(__instance).Field("currentSort").GetValue<int>());
			storedData.saveAscendingData(Traverse.Create(__instance).Field("sortAscending").GetValue<bool>());
		}
	}

	[HarmonyPatch(typeof(TownInterfaceController))]
	[HarmonyPatch("sortHome")]
	static class PersistentCharacterListAtHome_Patch3
	{
		static void Prefix(int index, TownInterfaceController __instance)
		{
			if (storedData.getSortData() == index)
				storedData.saveAscendingData(!storedData.getAscendingData());
			else
				storedData.saveAscendingData(false);

			storedData.saveSortData(index);
		}
	}

	[HarmonyPatch(typeof(TownInterfaceController))]
	[HarmonyPatch("updateHome")]
	static class PersistentCharacterListAtHome_Patch2
	{
		static void Prefix(TownInterfaceController __instance)
		{
			__instance.GetType().GetField("currentSort", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, storedData.getSortData());
			__instance.GetType().GetField("sortAscending", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, storedData.getAscendingData());

			int currentSort = Traverse.Create(__instance).Field("currentSort").GetValue<int>();
			int characterSelPage = Traverse.Create(__instance).Field("characterSelPage").GetValue<int>();
			bool sortAscending = Traverse.Create(__instance).Field("sortAscending").GetValue<bool>();

			int index = currentSort;

			//__instance.characterSelPage = 0;
			__instance.GetType().GetField("characterSelPage", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, 0);

			if (!sortAscending)
			{
				switch (index)
				{
					case 0:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.CharacterMarker == b.CharacterMarker)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -((int)a.CharacterMarker).CompareTo((int)b.CharacterMarker);
						});
						return;
					case 1:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.Level == b.Level)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.Level.CompareTo(b.Level);
						});
						return;
					case 2:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.Stamina == b.Stamina)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.Stamina.CompareTo(b.Stamina);
						});
						return;
					case 3:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.CurrMorale == b.CurrMorale)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.CurrMorale.CompareTo(b.CurrMorale);
						});
						return;
					case 4:
						TownManager.instance.home.Sort((Stats a, Stats b) => -a.getStrengthEval().CompareTo(b.getStrengthEval()));
						return;
					case 5:
						TownManager.instance.home.Sort((Stats a, Stats b) => -a.getBrothelScore(true).CompareTo(b.getBrothelScore(true)));
						return;
					case 6:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.species == b.species)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.species.CompareTo(b.species);
						});
						return;
					case 7:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.battleRank == b.battleRank)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.battleRank.CompareTo(b.battleRank);
						});
						return;
					case 8:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.size == b.size)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.size.CompareTo(b.size);
						});
						return;
					case 9:
						TownManager.instance.home.Sort((Stats a, Stats b) => -a.getTotalGeneticValue().CompareTo(b.getTotalGeneticValue()));
						return;
					case 10:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.CurrentEnergy == b.CurrentEnergy)
							{
								return -a.genetics.id.CompareTo(b.genetics.id);
							}
							return -a.CurrentEnergy.CompareTo(b.CurrentEnergy);
						});
						return;
					default:
						return;
				}
			}
			else
			{
				switch (index)
				{
					case 0:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.CharacterMarker == b.CharacterMarker)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return ((int)a.CharacterMarker).CompareTo((int)b.CharacterMarker);
						});
						return;
					case 1:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.Level == b.Level)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.Level.CompareTo(b.Level);
						});
						return;
					case 2:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.Stamina == b.Stamina)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.Stamina.CompareTo(b.Stamina);
						});
						return;
					case 3:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.CurrMorale == b.CurrMorale)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.CurrMorale.CompareTo(b.CurrMorale);
						});
						return;
					case 4:
						TownManager.instance.home.Sort((Stats a, Stats b) => a.getStrengthEval().CompareTo(b.getStrengthEval()));
						return;
					case 5:
						TownManager.instance.home.Sort((Stats a, Stats b) => a.getBrothelScore(true).CompareTo(b.getBrothelScore(true)));
						return;
					case 6:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.species == b.species)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.species.CompareTo(b.species);
						});
						return;
					case 7:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.battleRank == b.battleRank)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.battleRank.CompareTo(b.battleRank);
						});
						return;
					case 8:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.size == b.size)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.size.CompareTo(b.size);
						});
						return;
					case 9:
						TownManager.instance.home.Sort((Stats a, Stats b) => a.getTotalGeneticValue().CompareTo(b.getTotalGeneticValue()));
						return;
					case 10:
						TownManager.instance.home.Sort(delegate (Stats a, Stats b)
						{
							if (a.CurrentEnergy == b.CurrentEnergy)
							{
								return a.genetics.id.CompareTo(b.genetics.id);
							}
							return a.CurrentEnergy.CompareTo(b.CurrentEnergy);
						});
						return;
					default:
						return;
				}
			}
		}
	}
}