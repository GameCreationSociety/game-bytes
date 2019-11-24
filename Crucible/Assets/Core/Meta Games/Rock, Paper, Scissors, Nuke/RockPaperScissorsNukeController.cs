using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RockPaperScissorsNukeController : UnitySingleton<RockPaperScissorsNukeController>
{
   private enum choice
    {
        ROCK,
        PAPER,
        SCISSORS,
        NUKE,
        ANTINUKE,
        NONE
    };

    private enum battleOutcome
    {
        P1WIN,
        P2WIN,
        TIE,
        INVALID
    };

    private enum RPSNGameState
    {
        CHOOSE_WEAPON,
        ANIMATIONS_WAIT,
        CHOOSE_STAGE
    };

    [System.Serializable]
    public struct Controls
    {
        public KeyCode P1Confirm;
        public KeyCode P1Rock;
        public KeyCode P1Paper;
        public KeyCode P1Scissors;
        public KeyCode P1Nuke;
        public KeyCode P1AntiNuke;
        public KeyCode P2Confirm;
        public KeyCode P2Rock;
        public KeyCode P2Paper;
        public KeyCode P2Scissors;
        public KeyCode P2Nuke;
        public KeyCode P2AntiNuke;
    };


    [Header("Gameplay")]
    private GameState gameState;
    [SerializeField] private RPSNGameState state;
    [SerializeField] private float RPSNChoiceWaitTime;
    [SerializeField] private float winnerChoiceDisplayTime;
    [SerializeField] private float chooseMinigameWaitTime;

    [SerializeField] private choice p1Choice;
    [SerializeField] private bool confirmedP1;
    [SerializeField] private choice p2Choice;
    [SerializeField] private bool confirmedP2;

    [SerializeField] private Controls controls;

    [SerializeField] int rockPrice;
    [SerializeField] int paperPrice;
    [SerializeField] int scissorsPrice;
    [SerializeField] int nukePrice;
    [SerializeField] int antiNukePrice;

    [SerializeField] private int numberOfMinigamesToChooseFrom;
    
    [Header("Sounds")]
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioClip choiceSelected;

    [Header("Graphics")]
    [SerializeField] GameObject chooseMinigameGraphics;
    [SerializeField] TextMeshProUGUI p1ChoiceGUI;
    [SerializeField] TextMeshProUGUI p2ChoiceGUI;
    [SerializeField] TextMeshProUGUI p1ConfirmedGUI;
    [SerializeField] TextMeshProUGUI p2ConfirmedGUI;
    [SerializeField] TextMeshProUGUI p1Score;
    [SerializeField] TextMeshProUGUI p2Score;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI RPSNcountdownTimer;
    [SerializeField] Sprite rock;
    [SerializeField] Sprite paper;
    [SerializeField] Sprite scissors;
    [SerializeField] Sprite nuke;
    [SerializeField] Sprite antinuke;
    [SerializeField] Image p1ChoiceImage;
    [SerializeField] Image p2ChoiceImage;
    [SerializeField] GameObject battleResultUI;
    [SerializeField] GameObject playBattleUI;

    void InitRockPaperScissorsNuke()
    {
        p1Choice = choice.NONE;
        p2Choice = choice.NONE;

        // init the graphics
        p1ChoiceGUI.enabled = false;
        p2ChoiceGUI.enabled = false;
        p1Score.SetText(GameState.Instance.ScorePlayer1.ToString() + " POINTS");
        p2Score.SetText(GameState.Instance.ScorePlayer2.ToString() + " POINTS");

        state = RPSNGameState.ANIMATIONS_WAIT; 
    }


    private void Start()
    {
        gameState = GameState.Instance;
        InitRockPaperScissorsNuke();
        StartCoroutine(WaitForChoices());

    }

    // returns true if both players have selected a choice,
    // returns false otherise
    bool BothPlayersReady()
    {
        if (confirmedP1 && confirmedP2)
        {
            return true;
        }
        return false;
    }

    void DisplayBattleOutcome(battleOutcome outcome)
    {
        Debug.Log("Displaying the battle outcome");
        p1ChoiceGUI.SetText(p1Choice.ToString());
        p2ChoiceGUI.SetText(p2Choice.ToString());

        // update the score
        p1Score.SetText(GameState.Instance.ScorePlayer1.ToString() + " POINTS");
        p2Score.SetText(GameState.Instance.ScorePlayer2.ToString() + " POINTS");

        battleResultUI.SetActive(true);
        playBattleUI.SetActive(false);

        // display art assets corresponding to each player's choice
        Sprite p1Sprite = rock; // default image is rock
        Sprite p2Sprite = rock;

        switch (p1Choice)
        {
            case choice.ROCK:
                p1Sprite = rock;
                break;
            case choice.PAPER:
                p1Sprite = paper;
                break;
            case choice.SCISSORS:
                p1Sprite = scissors;
                break;
            case choice.NUKE:
                p1Sprite = nuke;
                break;
            case choice.ANTINUKE:
                p1Sprite = antinuke;
                break;
        }
        switch (p2Choice)
        {
            case choice.ROCK:
                p2Sprite = rock;
                break;
            case choice.PAPER:
                p2Sprite = paper;
                break;
            case choice.SCISSORS:
                p2Sprite = scissors;
                break;
            case choice.NUKE:
                p2Sprite = nuke;
                break;
            case choice.ANTINUKE:
                p2Sprite = antinuke;
                break;
        }

        p1ChoiceImage.sprite = p1Sprite;
        p2ChoiceImage.sprite = p2Sprite;

        switch (outcome)
        {
            case battleOutcome.INVALID:
                Debug.Log("Oh no! You've created an invalid battle outcome!");
                winText.SetText("##INVALID BATTLE OUTCOME##");
                break;

            case battleOutcome.P1WIN:
                winText.SetText("Player 1 Wins!");
                break;

            case battleOutcome.P2WIN:
                winText.SetText("Player 2 Wins!");
                break;

            case battleOutcome.TIE:
                winText.SetText("It's a tie!");
                break;
        }
        
    }

    // wait for the players to choose their move, display results from battle
    IEnumerator WaitForChoices()
    {
        state = RPSNGameState.CHOOSE_WEAPON;
        // wait for players to choose their move
        float choiceCounter = RPSNChoiceWaitTime;
        while (!BothPlayersReady() && choiceCounter > 1)
        {
            choiceCounter -= 0.01f;
            // update the counter
            RPSNcountdownTimer.SetText(Mathf.FloorToInt(choiceCounter).ToString());
            ChoiceListen();
            yield return new WaitForSeconds(0.01f);
        }
        // purchase the choices
        PurchaseChoice(1);
        PurchaseChoice(2);
        battleOutcome result = Battle();

        // display the results of their choices
        DisplayBattleOutcome(result);
        float displayOutcomeCounter = winnerChoiceDisplayTime;
        while (displayOutcomeCounter > 1)
        {
            displayOutcomeCounter -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        // have the player who won the battle select the next stage
        StartCoroutine(ChooseMinigame());

        // SIMULATE PLAYING A GAME

        /*
        if (result == battleOutcome.P1WIN)
        {
            SimulatePlayerWinningMinigame(1);
        }
        else if (result == battleOutcome.P2WIN)
        {
            SimulatePlayerWinningMinigame(2);
        }
        else
        {
            SimulatePlayerWinningMinigame(Random.Range(1, 2));
        }
        */
    }

    void PurchaseChoice(int player)
    {
        if (player == 1) // player 1
        {
            switch (p1Choice)
            {
                case choice.ROCK:
                    if (GameState.Instance.ScorePlayer1 >= rockPrice)
                    {
                        GameState.Instance.ScorePlayer1 -= rockPrice;
                    } else
                    {
                        p1Choice = RandomChoice();
                    }
                    break;
                case choice.PAPER:
                    if (GameState.Instance.ScorePlayer1 >= paperPrice)
                    {
                        GameState.Instance.ScorePlayer1 -= paperPrice;
                    } else
                    {
                        p1Choice = RandomChoice();
                    }
                    break;
                case choice.SCISSORS:
                    if (GameState.Instance.ScorePlayer1 >= scissorsPrice)
                    {
                        GameState.Instance.ScorePlayer1 -= scissorsPrice;
                    }  else
                    {
                        p1Choice = RandomChoice();
                    }
                    break;
                case choice.NUKE:
                    if (GameState.Instance.ScorePlayer1 >= nukePrice)
                    {
                        GameState.Instance.ScorePlayer1 -= nukePrice;
                    }   else
                    {
                        p1Choice = RandomChoice();
                    }
                    break;
                case choice.ANTINUKE:
                    if (GameState.Instance.ScorePlayer1 >= antiNukePrice)
                    {
                        GameState.Instance.ScorePlayer1 -= antiNukePrice;
                    } else
                    {
                        p1Choice = RandomChoice();
                    }
                    break;
            }
        } else // player 2
        {
            switch (p2Choice)
            {
                case choice.ROCK:
                    if (GameState.Instance.ScorePlayer2 >= rockPrice)
                    {
                        GameState.Instance.ScorePlayer2 -= rockPrice;
                    }
                    else
                    {
                        p2Choice = RandomChoice();
                    }
                    break;
                case choice.PAPER:
                    if (GameState.Instance.ScorePlayer2 >= paperPrice)
                    {
                        GameState.Instance.ScorePlayer2 -= paperPrice;
                    }
                    else
                    {
                        p2Choice = RandomChoice();
                    }
                    break;
                case choice.SCISSORS:
                    if (GameState.Instance.ScorePlayer2 >= scissorsPrice)
                    {
                        GameState.Instance.ScorePlayer2 -= scissorsPrice;
                    }
                    else
                    {
                        p2Choice = RandomChoice();
                    }
                    break;
                case choice.NUKE:
                    if (GameState.Instance.ScorePlayer2 >= nukePrice)
                    {
                        GameState.Instance.ScorePlayer2 -= nukePrice;
                    }
                    else
                    {
                        p2Choice = RandomChoice();
                    }
                    break;
                case choice.ANTINUKE:
                    if (GameState.Instance.ScorePlayer2 >= antiNukePrice)
                    {
                        GameState.Instance.ScorePlayer2 -= antiNukePrice;
                    }
                    else
                    {
                        p2Choice = RandomChoice();
                    }
                    break;
            }
        }
    }


    void SimulatePlayerWinningMinigame(int playerWon)
    {
        // give the playe the points
        GameState.Instance.MinigamesPlayed++;
        GameState.Instance.MinigamesWon++;
        if (playerWon == 1) // player 1
        {
            GameState.Instance.ScorePlayer1 += Random.Range(1, 5);
            GameState.Instance.MinigamesWonByP1++;
        } else // player 2
        {
            GameState.Instance.ScorePlayer2 += Random.Range(1, 5);
            GameState.Instance.MinigamesWonByP2++;
        }
        // reload the rock paper scissors nuke scene if nobody won yet
        if (GameState.Instance.ScorePlayer1 >= GameState.Instance.WinningScore ||
            GameState.Instance.ScorePlayer2 >= GameState.Instance.WinningScore)
        {
            SceneTransitionController.Instance.TransitionToScene("EndScreen");
        } else
        {
        SceneTransitionController.Instance.TransitionToScene("RockPaperScissorsNuke");
        }
    }


    // update variables with player's choice (rock, paper, scissors, nuke, antinuke)
    void ChoiceListen()
    {
        // player one controls

        if (Input.GetKey(controls.P1Confirm))
        {
            confirmedP1 = true;
            // display on UI that p1 confimred
            p1ConfirmedGUI.SetText("Ready");
        }
        else if (Input.GetKey(controls.P1Rock))
        {
            p1Choice = choice.ROCK;
        }
        else if (Input.GetKey(controls.P1Paper))
        {
            p1Choice = choice.PAPER;
        }
        else if (Input.GetKey(controls.P1Scissors)) 
        {
            p1Choice = choice.SCISSORS;
        }
        else if (Input.GetKey(controls.P1Nuke))
        {
            p1Choice = choice.NUKE;
        }
        else if (Input.GetKey(controls.P1AntiNuke))
        {
            p1Choice = choice.ANTINUKE;
        }

        //player two controls

        if (Input.GetKey(controls.P2Confirm))
        {
            confirmedP2 = true;
            p2ConfirmedGUI.SetText("Ready");
            // display on UI that p2 is confirmed
        }
        else if (Input.GetKey(controls.P2Rock))
        {
            p2Choice = choice.ROCK;
        }
        else if (Input.GetKey(controls.P2Paper))
        {
            p2Choice = choice.PAPER;
        }
        else if (Input.GetKey(controls.P2Scissors)) 
        {
            p2Choice = choice.SCISSORS;
        }
        else if (Input.GetKey(controls.P2Nuke))
        {
            p2Choice = choice.NUKE;
        }
        else if (Input.GetKey(controls.P2AntiNuke))
        {
            p2Choice = choice.ANTINUKE;
        }
    }



    // Let the player who won choose the minigame
    IEnumerator ChooseMinigame()
    {
        // state = RPSNGameState.CHOOSE_STAGE;
        SceneTransitionController.Instance.TransitionToScene("SelectMinigame");
        yield return null;
    }

    // randomly chooses rock, paper or scissors
    choice RandomChoice()
    {
        return (choice)Random.Range(0, 3); 
    }



    // returns the result rock paper scissors nuke battle
    battleOutcome Battle()
    {
        // if either of the players do not have a choice, select a random choice
        if (p1Choice == choice.NONE)
        {
            p1Choice = RandomChoice();
        }
        if (p2Choice == choice.NONE)
        {
            p2Choice = RandomChoice();
        }

        // p1 nuke
        if (p1Choice == choice.NUKE)
        {
            if (p2Choice == choice.ANTINUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P2WIN;
                return battleOutcome.P2WIN;
            }
            else if (p2Choice == choice.NUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.TIE;
                return battleOutcome.TIE;
            }
            else
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P1WIN;
                return battleOutcome.P1WIN;
            }
        }

        // p1 anti nuke
        if (p1Choice == choice.ANTINUKE)
        {
            if (p2Choice == choice.NUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P1WIN;
                return battleOutcome.P1WIN;
            }
            else if (p2Choice == choice.ANTINUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.TIE;
                return battleOutcome.TIE;
            }
            else
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P2WIN;
                return battleOutcome.P2WIN;
            }
        }

        // p1 rock
        if (p1Choice == choice.ROCK)
        {
            // rock beats scissors and antinuke
            if (p2Choice == choice.SCISSORS || p2Choice == choice.ANTINUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P1WIN;
                return battleOutcome.P1WIN;
            // rock ties rock
            } else if (p2Choice == choice.ROCK)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.TIE;
                return battleOutcome.TIE;
            }
            // rock loses to everything else
            else
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P2WIN;
                return battleOutcome.P2WIN;
            }
        }

        // p1 scissors
        if (p1Choice == choice.SCISSORS)
        {
            // scissors beats paper and antinuke
            if (p2Choice == choice.PAPER || p2Choice == choice.ANTINUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P1WIN;
                return battleOutcome.P1WIN;
            }
            // scissors ties scissors
            else if (p2Choice == choice.SCISSORS)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.TIE;
                return battleOutcome.TIE;
            }
            // scissors loses to everything else
            else
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P2WIN;
                return battleOutcome.P2WIN;
            }
        }

        // p1 paper
        if (p1Choice == choice.PAPER)
        {
            // paper beats rock and antinuke
            if (p2Choice == choice.ROCK || p2Choice == choice.ANTINUKE)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P1WIN;
                return battleOutcome.P1WIN;
            }
            // paper ties paper
            else if (p2Choice == choice.PAPER)
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.TIE;
                return battleOutcome.TIE;
            }
            // paper loses to everything else
            else
            {
                GameState.Instance.LastMetagameFinishState = LastMetagameFinish.P2WIN;
                return battleOutcome.P2WIN;
            }
        }
        // THIS CASE SHOULD NEVER HAPPEN. FOR DEBUGGING
        return battleOutcome.INVALID;
    }

    // generates a random subset of minigames without repeats (if possible)
    List<MinigameInfo> RandomMinigamesSubset()
    {
        List<MinigameInfo> possibleChoiceCopy = new List<MinigameInfo>(gameState.SelectedMinigames);
        List<MinigameInfo> result = new List<MinigameInfo>();
        while (result.Count < numberOfMinigamesToChooseFrom)
        {
            // generate a new random index
            int randomInt1 = Mathf.RoundToInt(Random.Range(0, possibleChoiceCopy.Count));
            // add the random minigame to the result list
            result.Add(possibleChoiceCopy[randomInt1]);
            // remove the minigame that we added to result from possibleChoiceCopy (this ensures we dont have repeats)
            possibleChoiceCopy.RemoveAt(randomInt1);
            
            // if we run out of possible minigames to add, then allow repeats
            if (possibleChoiceCopy.Count == 0)
            {
                while (result.Count < numberOfMinigamesToChooseFrom)
                {
                    int randomInt2 = Mathf.RoundToInt(Random.Range(0, gameState.SelectedMinigames.Count));
                    // add the random minigame to the result list
                    result.Add(gameState.SelectedMinigames[randomInt2]);
                }
            }
        }
        return result;
    }
}
