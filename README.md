# Portals of Phereon Small Fixes

Here I will post small fixes that I end up making for the game while I play it.

Based on V0.14.0.1 from OCT 21, 2020


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

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-11 23_06_59-PortalsOfPhereon.png?raw=true)

![Mod](https://github.com/purpleorangegames/portalsofphereonsmallfixes/blob/main/Images/2020-11-11 23_05_38-PortalsOfPhereon.png?raw=true)

updateRoom() at BrothelInterfaceController.cs
from:
```
ToolTipManager.instance.showCharacterTooltip(tmp2, new Vector3(-476f, 45f, 0f));
```
to:
```
ToolTipManager.instance.showCharacterTooltip(tmp2, new Vector3(-727f, -305f, 0f));
```
