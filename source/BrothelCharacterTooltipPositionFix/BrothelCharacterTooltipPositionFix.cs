

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

namespace BrothelCharacterTooltipPositionFix
{

    [BepInPlugin(GUID, Name, Version)]
    public class BrothelCharacterTooltipPositionFix : BaseUnityPlugin
    {
        public const string GUID = "net.pog.portalsofphereon.mod.brothelcharactertooltippositionfix";
        public const string Name = "Brothel Character Tooltip Position Fix";
        public const string Version = "1.0";

        private void Start()
        {
            UnityEngine.Debug.Log(Name + " loaded (" + GUID + ")");

            var harmony = new Harmony(GUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //Set Halloween flag regardless of date
    [HarmonyPatch(typeof(BrothelInterfaceController))]
    [HarmonyPatch("updateRoom")]
    static class BrothelCharacterTooltipPositionFix_Patch
	{

		static void Postfix(BrothelInterfaceController __instance)
		{
			for (int i = __instance.roomCharacterRoster.transform.childCount - 1; i >= 0; i--)
			{
				if (__instance.roomCharacterRoster.transform.GetChild(i).name == "BrothelCHaracterNew(Clone)")
				{
					GameObject oldGameObject = __instance.roomCharacterRoster.transform.GetChild(i).gameObject;
					UnityEngine.Debug.Log(__instance.roomCharacterRoster.transform.GetChild(i));

					for (int j = 0; j < __instance.currentRoom.brothelCharacters.Count; j++)
					{
						Brothel.BrothelCharacter brothelCharacter = __instance.currentRoom.brothelCharacters[j];
						if (oldGameObject.GetComponentInChildren<TMP_Text>().text == brothelCharacter.stats.CharName
						 && oldGameObject.GetComponentsInChildren<TMP_Text>()[1].text == __instance.getDescription(brothelCharacter) )
                        {
							Stats tmp2 = brothelCharacter.stats;
							EventTrigger componentInChildren = oldGameObject.GetComponentInChildren<EventTrigger>();
							EventTrigger.Entry entry = new EventTrigger.Entry();
							entry.eventID = EventTriggerType.PointerClick;
							entry.callback.AddListener(delegate (BaseEventData A_1)
							{
									ToolTipManager.instance.showCharacterTooltip(tmp2, new Vector3(-727f, -305f, 0f));
							});
							componentInChildren.triggers.Add(entry);
						}
					}
				}
			}
		}

		/*
		static bool Prefix(BrothelInterfaceController __instance)
		{
			for (int i = __instance.roomCharacterRoster.transform.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(__instance.roomCharacterRoster.transform.GetChild(i).gameObject);
			}
			for (int j = 0; j < __instance.currentRoom.brothelCharacters.Count; j++)
			{
				Brothel.BrothelCharacter brothelCharacter = __instance.currentRoom.brothelCharacters[j];
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(__instance.characterPrefab, __instance.roomCharacterRoster.transform);
				gameObject.transform.localScale = Vector3.one;
				gameObject.GetComponentInChildren<TMP_Text>().text = brothelCharacter.stats.CharName;
				gameObject.GetComponentsInChildren<TMP_Text>()[1].text = __instance.getDescription(brothelCharacter);
				gameObject.GetComponentsInChildren<Image>()[1].sprite = brothelCharacter.stats.getAvatarPic();
				gameObject.GetComponentsInChildren<Image>()[3].sprite = ImageManager.instance.getIcon(brothelCharacter.stats.CurrGender);
				int tmp = brothelCharacter.stats.genetics.id;
				Stats tmp2 = brothelCharacter.stats;
				gameObject.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
				{
					__instance.moveCharacterOutOfRoom(tmp);
					ToolTipManager.instance.hideCharacterTooltip();
				});
				gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(delegate ()
				{
					__instance.removeCharacterFromRoom(tmp);
					ToolTipManager.instance.hideCharacterTooltip();
				});
				EventTrigger componentInChildren = gameObject.GetComponentInChildren<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(delegate (BaseEventData A_1)
				{
					ToolTipManager.instance.showCharacterTooltip(tmp2, new Vector3(-727f, -305f, 0f));
				});
				componentInChildren.triggers.Add(entry);
				EventTrigger.Entry entry2 = new EventTrigger.Entry();
				entry2.eventID = EventTriggerType.PointerExit;
				entry2.callback.AddListener(delegate (BaseEventData A_0)
				{
					ToolTipManager.instance.startInactivityTimer();
				});
				componentInChildren.triggers.Add(entry2);
				componentInChildren.triggers.Add(entry2);
			}
			__instance.updateRoomStats();

			return false;
		}
		*/
	}

}
