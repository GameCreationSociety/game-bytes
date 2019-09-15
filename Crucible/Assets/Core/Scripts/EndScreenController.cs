using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndScreenController : UnitySingleton<EndScreenController>
{

    [SerializeField] private TextMeshProUGUI WinLoseText = null;
    [SerializeField] private TextMeshProUGUI P1Score = null;
    [SerializeField] private TextMeshProUGUI P2Score = null;
    [SerializeField] private TextMeshProUGUI GamesPlayedText = null;
    [SerializeField] private TextMeshProUGUI GamesWonText = null;



    // Start is called before the first frame update
    void Start()
    {
        PopulateUI(true); // populate in debug mode
    }

    // Update is called once per frame
    void Update()
    {

    }


    // If one player won, return player name
    // If both players won, return 'You both win!'
    // If both players lost more than 50% of games played, return 'Better luck next time!'
    string WhoWonText()
    {
        int p1Score = GameState.Instance.MinigamesWonByP1;
        int p2Score = GameState.Instance.MinigamesWonByP2;
        int gamesPlayed = GameState.Instance.MinigamesPlayed;
        int gamesWon = GameState.Instance.MinigamesWon;

        float percentWon = gamesWon / gamesPlayed;

        if (percentWon < 0.5f)
        {
            return "Better luck next time!";
        } else if (p1Score == p2Score)
        {
            return "You both win!";
        } else if (p1Score > p2Score)
        {
            return "Player 1 wins!";
        } else if (p2Score > p1Score)
        {
            return "Player 2 wins!";
        } else
        // for debugging 
        {
            return "_INVALID_WIN_STATE_";
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
