# Portals of Phereon Small Fixes

Here I will post small fixes that I end up making for the game while I play it.

Based on V0.14.0.1 from OCT 21, 2020


# BepInEx
I converted the the first three fixes to Harmony (you can check them below), you can then use BepInEx to load Harmony mods with the game.
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
![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_29_11-BepInEx.png?raw=true)

Inside this folder you can put the plugins I've made, they are all DLLs, like this:
![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-13%2013_29_26-plugins.png?raw=true)

You can choose which to use, exclude the ones you don't want.

You can download all the source code which contains the dlls or just the dlls here:
https://github.com/purpleorangegames/portalsofphereonsmallfixes/releases/download/1/PortalOfPhereonSmallFixes_2020_11_13.7z

The last fix about the list sort persistence hasn't been converted to harmony yet.


# Fixes explained, used dnSpy to make the modifications


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
