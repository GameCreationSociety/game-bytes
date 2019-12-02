using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LastMinigameFinish
{
    WON,
    LOST,
    P1WIN,
    P2WIN,
    TIE,
    NONE
};

[System.Serializable]
public struct GameWinValues
{
    public int meteorMasher;
    public int shotPot;
    public int _default;
}

[System.Serializable]
public enum LastMetagameFinish
{
    P1WIN,
    P2WIN,
    TIE,
    NONE
}

[System.Serializable]
public enum MinigameMode
{
    REGULAR,
    ROCKPAPERNUKE
}


/** Persistent singleton (never deleted after creation) used to keep track the current state of the game. 
 *  If you are not the lead for the minigame project please DO NOT MODIFY this file. 
 *  Talk to the lead if you need something here to change.*/
public class GameState : UnitySingletonPersistent<GameState>
{
    public MinigameMode CurrentMode;
    public LastMinigameFinish LastMinigameFinishState;
    public LastMetagameFinish LastMetagameFinishState;
    public int MinigamesWon = 0;
    public int MinigamesPlayed = 0;
    public int MinigamesWonByP1 = 0;
    public int MinigamesWonByP2 = 0;
    public int ScorePlayer1 = 0;
    public int ScorePlayer2 = 0;
    public List<MinigameInfo> SelectedMinigames;
    public MinigameInfo CurrentMinigame;
    public int WinningScore = 10;
    public MinigameGamemodeTypes Gamemode;
    public GameWinValues winVals = new GameWinValues
    {
        meteorMasher = 3,
        shotPot = 2,
        _default = 1
    };

    /** A scene might be launched not from the minigame launcher but directly. In that case, the game state will not be valid.*/
    public bool IsGameStateValid()
    {
        return SelectedMinigames != null && SelectedMinigames.Count > 0 && MinigamesPlayed < SelectedMinigames.Count;
    }

    public void SetupNewMinigames(MinigameInfo[] NewSelectedMinigames, MinigameGamemodeTypes NewGamemode)
    {
        MinigamesWon = 0;
        MinigamesPlayed = 0;
        MinigamesWonByP1 = 0;
        MinigamesWonByP2 = 0;
        LastMinigameFinishState = LastMinigameFinish.NONE;
        LastMetagameFinishState = LastMetagameFinish.NONE;
        SelectedMinigames = new List<MinigameInfo>(NewSelectedMinigames);
        Gamemode = NewGamemode;
    }

    public void FinishMinigame(LastMinigameFinish FinishState)
    {
        MinigamesPlayed++;
        LastMinigameFinishState = FinishState;
        switch(FinishState)
        {
            case LastMinigameFinish.LOST:
                break;
            case LastMinigameFinish.WON:
                UpdateMetaScore(1);
                UpdateMetaScore(0);
                MinigamesWon++;
                break;
            case LastMinigameFinish.TIE:
                UpdateMetaScore(1);
                UpdateMetaScore(0);
                break;
            case LastMinigameFinish.P1WIN:
                UpdateMetaScore(0);
                MinigamesWonByP1++;
                break;
            case LastMinigameFinish.P2WIN:
                UpdateMetaScore(1);
                MinigamesWonByP2++;
                break;
            case LastMinigameFinish.NONE:
            default:
                break;
        }
    }

    public void UpdateMetaScore(int winningPlayer)
    {
        string gameName = CurrentMinigame.name.ToLower();

        switch (gameName)
        {
            case "meteor masher":
                AddMetaScore(winningPlayer, winVals.meteorMasher);
                break;
            case "shotpot":
                AddMetaScore(winningPlayer, winVals.shotPot);
                break;
            default:
                AddMetaScore(winningPlayer, winVals._default);
                break;
        }
    }

    public void AddMetaScore(int player, int score)
    {
        if (player == 0)
        {
            ScorePlayer1 += score;
        } else
        {
            ScorePlayer2 += score;
        }
    }
}

