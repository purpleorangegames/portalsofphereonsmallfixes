# Portals of Phereon Small Fixes

Here I will post small fixes that I end up making for the game while I play it.

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
