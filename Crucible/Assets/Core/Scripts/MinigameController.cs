using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/** Class used to control the minigame progress and update the game state on game finish. 
 *  If you are not the lead for the minigame project please DO NOT MODIFY this file. 
 *  Talk to the lead if you need something here to change.*/
public class MinigameController : UnitySingleton<MinigameController>
{
    [SerializeField] private GameSettings Settings = null;

    [Header("Gameplay Info")]
    [Range(5.0f, 180.0f)] public float DurationSeconds = 60.0f;
    public bool WinOnTimeEnd;
    public bool TimerPaused = false;

    [Header("Optional")]
    public TextMeshProUGUI TimerText = null;
    public TextMeshProUGUI Score1Text = null;
    public TextMeshProUGUI Score2Text = null;

    [Header("DEBUG")]
    public MinigameGamemodeTypes DEBUG_GamemodeToTest = MinigameGamemodeTypes.TWOPLAYERCOOP;

    [HideInInspector] public MinigameGamemodeTypes CurrentGamemode;
    [HideInInspector] public bool MinigameEnded = false;

    private float TimeElapsedSeconds = 0;
    private int P1Score = 0;
    private int P2Score = 0;

    public int GetScore(int PlayerNum)
    {
        return PlayerNum == 1 ? P1Score : (PlayerNum == 2 ? P2Score : 0);
    }

    public void AddScore(int PlayerNum, int AddScore)
    {
        if(PlayerNum == 1)
        {
            P1Score += AddScore;
        }
        else if(PlayerNum == 2)
        {
            P2Score += AddScore;
        }
    }

    public float GetElapsedTime()
    {
        return TimeElapsedSeconds;
    }

    public float GetTotalDuration()
    {
        return DurationSeconds;
    }

    public float GetPercentTimePassed()
    {
        return TimeElapsedSeconds / DurationSeconds;
    }

    private void Awake()
    {
        /* A gamemode will be automatically set only if the minigame scene is launched through the game.
         * If it's not valid, we grab the preset debug gamemode*/
        if (GameState.Instance.IsGameStateValid())
        {
            CurrentGamemode = GameState.Instance.Gamemode;
        }
        else
        {
            CurrentGamemode = DEBUG_GamemodeToTest;
        }

        if(CurrentGamemode != MinigameGamemodeTypes.TWOPLAYERVS)
        {
            if(Score1Text) Score1Text.enabled = false;
            if(Score2Text) Score2Text.enabled = false;
        }

       Instantiate(Settings.StartGameTransition);
    }

    private IEnumerator GoBackToLauncher()
    {
        yield return new WaitForSecondsRealtime(Settings.GameEndGraphicsWait);
        if (GameState.Instance.CurrentMode == MinigameMode.REGULAR)
        {
            SceneManager.LoadScene(Settings.MinigameLauncherScene.ScenePath);
        }
        else if (GameState.Instance.CurrentMode == MinigameMode.ROCKPAPERNUKE)
        {
            SceneManager.LoadScene("RockPaperScissorsNuke");
        }
    }

    private IEnumerator GoToEndScreen()
    {
        yield return new WaitForSecondsRealtime(Settings.GameEndGraphicsWait);
        SceneManager.LoadScene(Settings.MinigameEndScene.ScenePath);
    }

    public void FinishGame(LastMinigameFinish FinishState)
    {
        if (!MinigameEnded)
        {
            // Detect if the user passed in a bad finish state for the current minigame type
            if (CurrentGamemode == MinigameGamemodeTypes.TWOPLAYERVS)
            {
                if (FinishState == LastMinigameFinish.LOST
                    || FinishState == LastMinigameFinish.WON)
                {
                    Debug.LogError("Tried to finish a versus game as a singleplayer/coop game!");
                    return;
                }
            }
            else
            {
                if (FinishState == LastMinigameFinish.P1WIN
                    || FinishState == LastMinigameFinish.P2WIN
                    || FinishState == LastMinigameFinish.TIE)
                {
                    Debug.LogError("Tried to finish a singleplayer/coop game as a versus game!");
                    return;
                }
            }

            // Since we passed the bad state check, we can finish up the minigame correctly
            MinigameEnded = true;
            DisplayFinishGraphics(FinishState);

            // Only update the game state if it actually exists (in the case the minigame is launched from scene in editor,
            // the game state WILL NOT be valid
            if (GameState.Instance.IsGameStateValid())
            {
                // if we have played numGamesToPlay number of games, then go to the EndScreen scene. Otherwise, go back
                // to the minigame launcher
                GameState.Instance.FinishMinigame(FinishState);

                if (GameState.Instance.MinigamesPlayed >= GameState.Instance.SelectedMinigames.Count 
                    || GameState.Instance.MinigamesWonByP1 > (GameState.Instance.SelectedMinigames.Count/2)
                    || GameState.Instance.MinigamesWonByP2 > (GameState.Instance.SelectedMinigames.Count/2))
                {
                    Debug.Log("Going back to the end screen");
                    StartCoroutine(GoToEndScreen());
                }
                else
                {
                    StartCoroutine(GoBackToLauncher());
                }
            }
        }
    }

    private void DisplayFinishGraphics(LastMinigameFinish FinishState)
    {
        switch (FinishState)
        {
            case LastMinigameFinish.LOST:
                Instantiate(Settings.LoseGraphicsPrefab);
                break;
            case LastMinigameFinish.WON:
                Instantiate(Settings.WinGraphicsPrefab);
                break;
            case LastMinigameFinish.TIE:
                Instantiate(Settings.TieGraphicsPrefab);
                break;
            case LastMinigameFinish.P1WIN:
                Instantiate(Settings.P1WinGraphicsPrefab);
                break;
            case LastMinigameFinish.P2WIN:
                Instantiate(Settings.P2WinGraphicsPrefab);
                break;
            case LastMinigameFinish.NONE:
            default:
                break;
        }
        Instantiate(Settings.EndGameTransition);
        
    }

    private void Update()
    {
        if (!MinigameEnded)
        {
            if (!TimerPaused)
            {
                TimeElapsedSeconds += Time.deltaTime;
            }

            // Check whether to finish the minigame due to time running out
            if (TimeElapsedSeconds >= DurationSeconds)
            {
                if (CurrentGamemode == MinigameGamemodeTypes.TWOPLAYERVS)
                {
                    if(P1Score == P2Score)
                    {
                        FinishGame(LastMinigameFinish.TIE);
                    }
                    else
                    {
                        FinishGame(P1Score > P2Score ? LastMinigameFinish.P1WIN : LastMinigameFinish.P2WIN);
                    }
                }
                else
                {
                    FinishGame(WinOnTimeEnd ? LastMinigameFinish.WON : LastMinigameFinish.LOST);
                }
                    
                TimeElapsedSeconds = DurationSeconds;
            }

            // Update the timer text if it exists to reflect the time left in seconds.
            if (TimerText)
            {
                int TimeLeft = Mathf.CeilToInt(DurationSeconds - TimeElapsedSeconds);
                TimerText.SetText(TimeLeft.ToString());
            }

            if(Score1Text)
            {
                Score1Text.SetText(P1Score.ToString());
            }

            if(Score2Text)
            {
                Score2Text.SetText(P2Score.ToString());
            }
        }
    }
}
