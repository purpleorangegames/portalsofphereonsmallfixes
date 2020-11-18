

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


namespace HighLevelTraitSynergy
{

	[BepInPlugin(GUID, Name, Version)]
    public class HighLevelTraitSynergy : BaseUnityPlugin
    {
        public const string GUID = "net.pog.portalsofphereon.mod.HighLevelTraitSynergy";
        public const string Name = "High Level Trait Synergy";
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
        static public List<string> list = new List<string>();
        static public List<int> listLVL = new List<int>();
        static public int count = 0;
    }

    [HarmonyPatch(typeof(CharacterControllerScript))]
    [HarmonyPatch("initNewEnemy")]
    static class HighLevelTraitSynergy_Patch
    {

        static void Prefix(Stats s)
        {
            if (variables.count == variables.list.Count)
                return;

            string type = variables.list[variables.count];
            UnityEngine.Debug.Log("initNewEnemy type:" + variables.list[variables.count]);

            if (type == "")
            {
                variables.count++;
                return;
            }
    
            List<GeneticTraitType> owned = new List<GeneticTraitType>();

            List<GeneticTraitType> list = new List<GeneticTraitType>();

            int howManyTraits = (int) (3.0 + Math.Truncate((double)variables.listLVL[variables.count] / 10.0));

            for (int j=0; j<howManyTraits; j++)
            {
                list.Clear();
                owned.Clear();
                foreach (GeneticTrait t in s.GeneticTraits)
                    owned.Add(t.type);

                if (owned.Count<10)
                {

                    if (!owned.Contains(GeneticTraitType.Potential))
                        for (int i = 0; i < 1; i++)
                            list.Add(GeneticTraitType.Potential);

                    if (type == "NOR")
                    {
                        if (!owned.Contains(GeneticTraitType.Strong))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Strong);

                        if (!owned.Contains(GeneticTraitType.Fast))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Fast);

                        if (!owned.Contains(GeneticTraitType.Light))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Light);

                        if (!owned.Contains(GeneticTraitType.Heavy))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Heavy);

                        if (!owned.Contains(GeneticTraitType.Resilient))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Resilient);

                        if (!owned.Contains(GeneticTraitType.Intelligent))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Intelligent);

                        if (!owned.Contains(GeneticTraitType.Beautiful))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Beautiful);
                    }

                    else if (type == "STR")
                    {
                        if (!owned.Contains(GeneticTraitType.Strong))
                            for (int i = 0; i < 20; i++)
                                list.Add(GeneticTraitType.Strong);

                        if (!owned.Contains(GeneticTraitType.Wild))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Wild);

                        if (!owned.Contains(GeneticTraitType.Savage))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Savage);

                        if (!owned.Contains(GeneticTraitType.Heavy))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Heavy);

                        if (!owned.Contains(GeneticTraitType.Resilient))
                            for (int i = 0; i < 8; i++)
                                list.Add(GeneticTraitType.Resilient);

                        if (!owned.Contains(GeneticTraitType.LifeBound))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.LifeBound);

                        if (!owned.Contains(GeneticTraitType.Lumbering))
                            for (int i = 0; i < 4; i++)
                                list.Add(GeneticTraitType.Lumbering);
                    }

                    else if (type == "MAG")
                    {
                        if (!owned.Contains(GeneticTraitType.Arcane))
                            for (int i = 0; i < 20; i++)
                                list.Add(GeneticTraitType.Arcane);

                        if (!owned.Contains(GeneticTraitType.Infused))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Infused);

                        if (!owned.Contains(GeneticTraitType.Condensed))
                            for (int i = 0; i < 3; i++)
                                list.Add(GeneticTraitType.Condensed);

                        if (!owned.Contains(GeneticTraitType.Manabound))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Manabound);

                        if (!owned.Contains(GeneticTraitType.ManaSpark))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.ManaSpark);

                        if (!owned.Contains(GeneticTraitType.Intelligent))
                            for (int i = 0; i < 10; i++)
                                list.Add(GeneticTraitType.Intelligent);

                        if (!owned.Contains(GeneticTraitType.Fast))
                            for (int i = 0; i < 1; i++)
                                list.Add(GeneticTraitType.Fast);
                    }

                    else if (type == "SED")
                    {
                        if (!owned.Contains(GeneticTraitType.Seductive))
                            for (int i = 0; i < 20; i++)
                                list.Add(GeneticTraitType.Seductive);

                        if (!owned.Contains(GeneticTraitType.Corrupted))
                            for (int i = 0; i < 5; i++)
                                list.Add(GeneticTraitType.Corrupted);

                        if (!owned.Contains(GeneticTraitType.Beautiful))
                            for (int i = 0; i < 3; i++)
                                list.Add(GeneticTraitType.Beautiful);

                        if (!owned.Contains(GeneticTraitType.Light))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Light);

                        if (!owned.Contains(GeneticTraitType.Ethereal))
                            for (int i = 0; i < 1; i++)
                                list.Add(GeneticTraitType.Ethereal);

                        if (!owned.Contains(GeneticTraitType.Resilient))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Resilient);

                        if (!owned.Contains(GeneticTraitType.Fast))
                            for (int i = 0; i < 2; i++)
                                list.Add(GeneticTraitType.Fast);
                    }

                    if (list.Count > 0)
                        s.gainTrait(list[UnityEngine.Random.Range(0, list.Count)]);
                }
            }

            //variables.list.Clear();
            UnityEngine.Debug.Log("initNewEnemy :" + s.CharName);
            UnityEngine.Debug.Log("initNewEnemy :" + s.genetics.LevelUpType);
            UnityEngine.Debug.Log("initNewEnemy :" + s.MaxLevel);
            UnityEngine.Debug.Log("value :" + variables.list.Count + " - " + variables.count);
            variables.count++;

            foreach (GeneticTrait t in s.GeneticTraits)
            { 
                UnityEngine.Debug.Log("initNewEnemy :" + t.getName());
            }

            UnityEngine.Debug.Log("initNewEnemy -------------------------------");

        }
    }

    [HarmonyPatch(typeof(CharacterControllerScript))]
    [HarmonyPatch("spawnEnemies")]
    static class HighLevelTraitSynergy_Patch2
    {
        static void Prefix()
        {
            variables.list.Clear();
            variables.count = 0;

            OWCharacter[] array = SaveController.instance.encounter.characters;
            UnityEngine.Debug.Log("quantity :" + array.Length);
            foreach (OWCharacter a in array)
            {
                UnityEngine.Debug.Log("initNewEnemy :" + a.ToString());
                UnityEngine.Debug.Log("initNewEnemy species:" + a.species.ToString());

                CharacterData cd = CharacterControllerScript.instance.getCharacterData(a.species);
                UnityEngine.Debug.Log("initNewEnemy hp:" + cd.growth.hp);
                UnityEngine.Debug.Log("initNewEnemy lustDamage:" + cd.growth.lustDamage);
                UnityEngine.Debug.Log("initNewEnemy magic:" + cd.growth.magic);
                UnityEngine.Debug.Log("initNewEnemy mana:" + cd.growth.mana);
                UnityEngine.Debug.Log("initNewEnemy speed:" + cd.growth.speed);
                UnityEngine.Debug.Log("initNewEnemy strength:" + cd.growth.strength);

                variables.listLVL.Add(a.lvl);
                int addGenes = (int) (Math.Truncate(5.0 * (double)a.lvl / 10.0));
                if (a.lvl>15)
                {
                    if (UnityEngine.Random.Range(0, 10)==0)
                        variables.list.Add("NOR");
                    else if (cd.growth.strength > (cd.growth.magic + cd.growth.mana) / 2.0 && cd.growth.strength > cd.growth.lustDamage)
                        variables.list.Add("STR");
                    else if ((cd.growth.magic + cd.growth.mana) / 2.0 > cd.growth.strength && (cd.growth.magic + cd.growth.mana) / 2.0 > cd.growth.lustDamage)
                        variables.list.Add("MAG");
                    else if (cd.isHumanoid && cd.growth.lustDamage > (cd.growth.magic + cd.growth.mana) / 2.0 && cd.growth.lustDamage > cd.growth.strength)
                        variables.list.Add("SED");
                    else
                        variables.list.Add("NOR");


                    if (variables.list[variables.list.Count-1]!="NOR" && a.totalGenes + addGenes < 70)
                        a.totalGenes += addGenes;
                }
                else
                    variables.list.Add("");

                UnityEngine.Debug.Log("initNewEnemy lvl:" + a.lvl);
                UnityEngine.Debug.Log("initNewEnemy size:" + a.size);
                UnityEngine.Debug.Log("initNewEnemy hp:" + a.hpPercentage);
                UnityEngine.Debug.Log("initNewEnemy genes:" + a.totalGenes);
                UnityEngine.Debug.Log("initNewEnemy unique:" + a.uniqueCharacter);
                UnityEngine.Debug.Log("initNewEnemy boss:" + a.isBoss);

                UnityEngine.Debug.Log("---");
            }
        }
    }
}