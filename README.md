# Portals of Phereon Small Fixes

Here I will post small fixes that I end up making for the game while I play it.

Based on V0.14.0.1 from OCT 21, 2020

## If you are using mods, do not report bugs, remove mods before testing for a bug

## If you have a save file which you played with mods then you shouldn't report bugs related to this save file, start a new game to verify if the bug really exist before reporting

## Do not ask for help with the installation, use or to request new mods in any forum/discord/site, if you need to, contact me directly at tiagocc0@gmail.com or DM me at discord tiago#5270

# BepInEx
I converted all four fixes to Harmony (you can check them below), you can then use BepInEx to load Harmony mods with the game.
Added two other mods made only in Harmony that doubles the hostility from portals.
https://github.com/BepInEx/BepInEx

I tested using this version, it needs to be x86 for this game in particular on the current version stated above.
https://github.com/BepInEx/BepInEx/releases/download/v5.4/BepInEx_x86_5.4.0.0.zip


To install BepInEx first go to the Portals of Phereon folder, after downloading the file above:

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_27_31-POPExample.png?raw=true)


Then open the zip file, I use 7z, which you can see here, and copy the files:

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_28_07-BepInEx_x86_5.4.0.0.zip_.png?raw=true)


You will end up like this:

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_28_21-POPExample.png?raw=true)


Then in the new BepInEx folder, enter it and create a new folder called 'plugins':

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_29_11-BepInEx2.png?raw=true)


Inside this folder you can put the plugins I've made, they are all DLLs, like this:

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_29_26-plugins.png?raw=true)


You can choose which to use, exclude the ones you don't want.

You can download all the source code which contains the dlls or just the dlls here:
https://github.com/purpleorangegames/portalsofphereonsmallfixes/releases/download/1.4/PortalOfPhereonSmallFixes_2020_11_17.7z


# Fixes explained, used dnSpy to make the modifications

Making mofications using dnSpy is very different from making them using Harmony, in this case, since I prefer to use only prefix and postfix patches with it.
So the explanation below is not the same as the DLLs explained above.

### Fix: Reset loads left in Limited Save mode when player starts a NewGamePlus (ng+)

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/POPSmallFixLoadsLeftNGP.png?raw=true)

At SaveController.cs
```
  public bool startNewGamePlus()
  {
   ...
   //START - FIX RESET SAVE LOADING LEFT AFTER NEW GAME PLUS 2020_11_11
   if (this.difficulty.saveType == SaveType.Limited)
   {
    switch (this.difficulty.gameLength)
    {
     case GameLength.QuickRun:
      this.difficulty.loadingLeft = 5;
      break;
     case GameLength.VeryShort:
      this.difficulty.loadingLeft = 5;
      break;
     case GameLength.Short:
      this.difficulty.loadingLeft = 10;
      break;
     case GameLength.Medium:
      this.difficulty.loadingLeft = 15;
      break;
     case GameLength.Long:
      this.difficulty.loadingLeft = 20;
      break;
     case GameLength.VeryLong:
      this.difficulty.loadingLeft = 25;
      break;
     case GameLength.FreePlay:
      this.difficulty.loadingLeft = 50;
      break;
    }
   }
   //END - FIX RESET SAVE LOADING LEFT AFTER NEW GAME PLUS 2020_11_11
   ...
  }
```


### Fix: Position Character Tooltip so it won't be cut offscreen

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-11%2023_06_59-PortalsOfPhereon.png?raw=true)

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-11%2023_05_38-PortalsOfPhereon.png?raw=true)

updateRoom() at BrothelInterfaceController.cs
from:
```
ToolTipManager.instance.showCharacterTooltip(tmp2, new Vector3(-476f, 45f, 0f));
```
to:
```
ToolTipManager.instance.showCharacterTooltip(tmp2, new Vector3(-727f, -305f, 0f));
```


### Fix: Duplicated characters when removing them post battle, those duplicated would always be from reserve
It seems that after removing they were being added again to the list when showBattleSymmary was run again

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-12%2008_39_54-PortalsOfPhereon.png?raw=true)

showBattleSummary() at InterfaceController

add
```
if (firstOpen) 
```
to
```
foreach (Stats s in GameController.instance.reserveCharacters)
{
	BattleCharacterInfo item2 = new BattleCharacterInfo(s, false);
	GameController.instance.battleCharacterInfos.Add(item2);
} 
```


### Fix: Persistent character list at home

This will allow characters that you remove from party or brothel to get sorted automatically instead of ending up at the end of the list.
Also stop the sort selected being reset when you return to the list.

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-12%2019_28_24-PortalsOfPhereon.png?raw=true)

showHomeTab(..) at TownInterfaceController
Instead of always reseting to the default value now it will only reset when it is invalid.
from
```
this.currentSort = 0;
```
to
```
if (this.currentSort < 0 || this.currentSort > 10)
{
	this.currentSort = 0;
}
```


sortHome(..) at TownInterfaceController
Duplicated the function moving the sorting to another function with the same name but that accepts no parameter.
Since the old function, after sorting, calls to draw the list.
Later when the list is updated will also call the sorting function, but the part that is only sorting so it won't end up in a loop.
```
public void sortHome(int index)
{
	if (index == this.currentSort)
	{
		this.sortAscending = !this.sortAscending;
	}
	else
	{
		this.currentSort = index;
		this.sortAscending = false;
	}
	this.sortHome();
	this.homePage = 0;
	this.updateHome(true);
}

public void sortHome()
{
	int index = this.currentSort;
	this.characterSelPage = 0;
	if (!this.sortAscending)
	{
		switch (index)
...
		default:
			return;
		}
	}
}
```


updateHome(..) at TownInterfaceController
Then we finally add the sort function at the start of the function that draws the list.
```
public void updateHome(bool alsoUpdateParty = true)
{
	this.sortHome();
```


### Mod: Double Average Hostility of World

This one I made directly into Harmony, it basically makes a list that is reset when initPortals(..) at TownManager.cs runs and after that everytime getEntryCost(..) at Portal.cs runs it doubles the average hostility and adds the id to the list so it won't double again.

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-14%2013_57_11-PortalsOfPhereon.png?raw=true)


### Mod: High Level Trait Synergy

Also made directly into Harmony, this mod will start when you initiate combat and when each enemy spawns.
It checks for units above level 15, then chooses one of four profiles for it (strength, magic, seductive or normal) and also try to add a few points of genes.
For each profile there is a set of traits the creature can get, each with a different 'rarity' level.
The purpose of the mod is to make specialized enemies, since we will have a specializes team it makes sense for me for the enemy to also become stronger over time (as level increases). 

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-17%2021_59_52-PortalsOfPhereon.png?raw=true)

