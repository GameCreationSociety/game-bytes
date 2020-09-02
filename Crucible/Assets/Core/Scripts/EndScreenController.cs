using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*

Author: George Whitfield
gwhitfie@andrew.cmu.edu
September 15, 2019

If you are not the lead for the minigame project please DO NOT MODIFY this file.
*  Talk to the lead if you need something here to change.*/

public class EndScreenController : UnitySingleton<EndScreenController>
{

    [SerializeField] private TextMeshProUGUI WinLoseText = null;
    [SerializeField] private TextMeshProUGUI P1Score = null;
    [SerializeField] private TextMeshProUGUI P2Score = null;
    [SerializeField] private TextMeshProUGUI GamesPlayedText = null;
    [SerializeField] private TextMeshProUGUI GamesWonText = null;
    [SerializeField] private float waitTimeUntilLoadMainMenu = 30f;
    [SerializeField] private GameObject confetti;
            

    // Start is called before the first frame update
    void Start()
    {
        PopulateUI(true); // populate in debug mode
        StartCoroutine("RunEndScreen");
    }

    string WhoWonText()
    {
        int p1Score = GameState.Instance.MinigamesWonByP1;
        int p2Score = GameState.Instance.MinigamesWonByP2;
        int gamesPlayed = GameState.Instance.MinigamesPlayed;
        int gamesWon = GameState.Instance.MinigamesWon;

        if (GameState.Instance.Gamemode == MinigameGamemodeTypes.TWOPLAYERVS)
        {
            if (p1Score == p2Score)
            {
                return "Tie!";
            }
            else if (p1Score > p2Score)
            {
                return "Player 1 wins!";
            }
            else 
            {
                return "Player 2 wins!";
            }
        }
        // IF not vs, we currently assume a coop mode
        else
        {
            float percentWon = 0;
            if (gamesPlayed != 0)
            {
                percentWon = gamesWon / gamesPlayed;
            }

            if (percentWon < 0.5f)
            {
                return "You lose! :(";
            }
            else
            {
                return "You win! :)";
            }
        }
    }

    IEnumerator RunEndScreen()
    {
        while (waitTimeUntilLoadMainMenu > 0)
        {
            // transition to the main menu if the keys R or SPACE are pressed
            waitTimeUntilLoadMainMenu -= Time.deltaTime;
            InputListener();
            yield return null;
        }
        // transition to the main meny if we wait more the waitTimeUntilLoadMainMenu
        SceneTransitionController.Instance.TransitionToScene("MainMenu");
    }
    void InputListener()
    {
        if (MinigameInputHelper.IsButton1Down(1) || MinigameInputHelper.IsButton1Down(2))
        {
            //disable the confetti
            confetti.SetActive(false);
            SceneTransitionController.Instance.TransitionToScene("MainMenu");
        }
    }

    void PopulateUI(bool IsInDebugMode)
    {
        if (GameState.Instance.IsGameStateValid() || IsInDebugMode)
        {
            WinLoseText.SetText(WhoWonText());
            P1Score.SetText(GameState.Instance.MinigamesWonByP1.ToString());
            P2Score.SetText(GameState.Instance.MinigamesWonByP2.ToString());
            GamesPlayedText.SetText(GameState.Instance.MinigamesPlayed.ToString());
            GamesWonText.SetText(GameState.Instance.MinigamesWon.ToString());
        }


    }
}
