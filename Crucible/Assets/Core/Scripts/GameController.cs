using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** Class used to start the game and generate lists of minigames.
 *  If you are not the lead for the minigame project please DO NOT MODIFY this file. 
 *  Talk to the lead if you need something here to change.*/
public class GameController : UnitySingleton<GameController>
{
    [SerializeField] private GameSettings Settings = null;
    [SerializeField] private int MinigamesPerGame = 5;

    [Header("DEBUG Settings")]
    [SerializeField] private MinigameInfo[] DEBUG_MinigamesToLaunch = null;
    [SerializeField] private MinigameGamemodeTypes DEBUG_TestGameMode;

    private void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        if (!Application.isEditor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void StartGame( MinigameGamemodeTypes GameModeSelected )
    {
        // We launch the debug minigame only if we are in the editor to make sure the build never uses a debug minigame
        if (Application.isEditor && DEBUG_MinigamesToLaunch.Length > 0)
        {
            GameState.Instance.SetupNewMinigames(DEBUG_MinigamesToLaunch, GameModeSelected);
            if (GameState.Instance.CurrentMode == MinigameMode.REGULAR)
            {
                SceneTransitionController.Instance.TransitionToScene(Settings.MinigameLauncherScene.ScenePath);
            }
            else if (GameState.Instance.CurrentMode == MinigameMode.ROCKPAPERNUKE)
            {
                SceneTransitionController.Instance.TransitionToScene("RockPaperScissorsNuke");
            }
        }
        else
        {
            // Add in minigames from shuffled list until we have enough minigames to play through the whole game
            MinigameInfo[] MinigamesToPlay = new MinigameInfo[MinigamesPerGame];
            MinigameInfo[] ShuffledMinigames = Settings.GetShuffledMinigames();
            int MinigamesAdded = 0;
            for (int i = 0; MinigamesAdded < MinigamesPerGame; i = (i + 1) % ShuffledMinigames.Length)
            {
                if (ShuffledMinigames[i].SupportedGameModes.HasFlag(GameModeSelected))
                {
                    MinigamesToPlay[MinigamesAdded] = ShuffledMinigames[i];
                    MinigamesAdded++;
                }
            }

            GameState.Instance.SetupNewMinigames(MinigamesToPlay, GameModeSelected);
            if (GameState.Instance.CurrentMode == MinigameMode.REGULAR)
            {
                SceneTransitionController.Instance.TransitionToScene(Settings.MinigameLauncherScene.ScenePath);
            }
            else if (GameState.Instance.CurrentMode == MinigameMode.ROCKPAPERNUKE)
            {
                SceneTransitionController.Instance.TransitionToScene("RockPaperScissorsNuke");
            }
        }
    }

    // NOT USED CURRENTLY
    public void Start1PlayerGame()
    {
        StartGame(MinigameGamemodeTypes.ONEPLAYER);
    }
    
    public void Start2PlayerCoopGame()
    {
        StartGame(MinigameGamemodeTypes.TWOPLAYERCOOP);
    }

    public void Start2PlayerVsGame()
    {
        GameState.Instance.CurrentMode = MinigameMode.REGULAR;
        StartGame(MinigameGamemodeTypes.TWOPLAYERVS);
    }

    // NOT USED CURRENTLY
    public void StartRockPaperNukeGame()
    {
        GameState.Instance.CurrentMode = MinigameMode.ROCKPAPERNUKE;
        StartGame(MinigameGamemodeTypes.TWOPLAYERVS);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
