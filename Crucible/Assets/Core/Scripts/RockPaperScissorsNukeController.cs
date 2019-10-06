using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }
    [Header("Gameplay")]
    [SerializeField] private float RPSNChoiceWaitTime;
    [SerializeField] private float winnerChoiceDisplayTime;
    [SerializeField] private float chooseMinigameWaitTime;

    [SerializeField] private choice p1Choice;
    [SerializeField] private choice p2Choice;

    [SerializeField] private int numberOfMinigamesToChooseFrom;
    [SerializeField] private List<MinigameInfo> allPossibleMinigames;

    [Header("Sounds")]
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioClip choiceSelected;

    [Header("Graphics")]
    [SerializeField] TextMeshProUGUI p1ChoiceGUI;
    [SerializeField] TextMeshProUGUI p2ChoiceGUI;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI countdownTimer;

    
    void InitRockPaperScissorsNuke()
    {
        p1Choice = choice.NONE;
        p2Choice = choice.NONE;
        // init the graphics
        winText.GetComponent<TextMeshProUGUI>().enabled = false;
    }


    private void Start()
    {
        InitRockPaperScissorsNuke();
        StartCoroutine("WaitForChoices");
    }

    // returns true if both players have selected a choice,
    // returns false otherise
    bool BothPlayersReady()
    {
        if (p1Choice != choice.NONE && p2Choice != choice.NONE)
        {
            return true;
        }
        return false;
    }


    void DisplayBattleOutcome(battleOutcome outcome)
    {
        Debug.Log("Displaying the battle outcome");

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


    IEnumerator WaitForChoices()
    {
        float counter = RPSNChoiceWaitTime;
        while (!BothPlayersReady() && counter > 0)
        {
            counter -= 0.01f;
            // update the counter
            countdownTimer.SetText(Mathf.FloorToInt(counter).ToString());
            yield return new WaitForSeconds(0.01f);
        }
        battleOutcome result = Battle();
        DisplayBattleOutcome(result);
        
    }

    // Let the player who won choose the minigame
    IEnumerator ChooseMinigame()
    {
        float counter = chooseMinigameWaitTime;
        while (chooseMinigameWaitTime > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
    }



    // randomly chooses rock, paper or scissoras
    choice RandomChoice()
    {
        return (choice)Random.Range(0, 3); 
    }


    // returns who wins the rock paper scissors battle
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
                return battleOutcome.P2WIN;
            }
            else if (p2Choice == choice.NUKE)
            {
                return battleOutcome.TIE;
            }
            else
            {
                return battleOutcome.P1WIN;
            }
        }

        // p1 rock
        if (p1Choice == choice.ROCK)
        {
            // rock beats scissors and antinuke
            if (p2Choice == choice.SCISSORS || p2Choice == choice.ANTINUKE)
            {
                return battleOutcome.P1WIN;
            // rock ties rock
            } else if (p2Choice == choice.ROCK)
            {
                return battleOutcome.TIE;
            }
            // rock loses to everything else
            else
            {
                return battleOutcome.P2WIN;
            }
        }

        // p1 scissors
        if (p1Choice == choice.SCISSORS)
        {
            // scissors beats paper and antinuke
            if (p2Choice == choice.PAPER || p2Choice == choice.ANTINUKE)
            {
                return battleOutcome.P1WIN;
            }
            // scissors ties scissors
            else if (p2Choice == choice.SCISSORS)
            {
                return battleOutcome.TIE;
            }
            // scissors loses to everything else
            else
            {
                return battleOutcome.P2WIN;
            }
        }

        // p1 paper
        if (p1Choice == choice.PAPER)
        {
            // paper beats rock and antinuke
            if (p2Choice == choice.ROCK || p2Choice == choice.ANTINUKE)
            {
                return battleOutcome.P1WIN;
            }
            // paper ties paper
            else if (p2Choice == choice.PAPER)
            {
                return battleOutcome.TIE;
            }
            // paper loses to everything else
            else
            {
                return battleOutcome.P2WIN;
            }
        }
        // THIS CASE SHOULD NEVER HAPPEN. FOR DEBUGGING
        return battleOutcome.INVALID;
    }

    // generates a random subset of minigames without repeats (if possible)
    List<MinigameInfo> RandomMinigamesSubset()
    {
        List<MinigameInfo> possibleChoiceCopy = new List<MinigameInfo>(allPossibleMinigames);
        List<MinigameInfo> result = new List<MinigameInfo>();
        while (result.Count < numberOfMinigamesToChooseFrom)
        {
            // generate a new random index
            int randomInt1 = Mathf.RoundToInt(Random.Range(0, possibleChoiceCopy.Count));
            // add the random element to the result list
            result.Add(possibleChoiceCopy[randomInt1]);
            // remove the element that we added to result from possibleChoiceCopy
            possibleChoiceCopy.RemoveAt(randomInt1);
            
            // if we run out of possible minigames to add, then allow repeats
            if (possibleChoiceCopy.Count == 0)
            {
                while (result.Count < numberOfMinigamesToChooseFrom)
                {
                    int randomInt2 = Mathf.RoundToInt(Random.Range(0, allPossibleMinigames.Count));
                    // add the random element to the result list
                    result.Add(allPossibleMinigames[randomInt2]);
                }
            }
        }
        return result;
    }
}
