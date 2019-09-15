using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/** Class used to launch a new minigame and update all the minigame data in the UI.
 *  If you are not the lead for the minigame project please DO NOT MODIFY this file. 
 *  Talk to the lead if you need something here to change.*/
public class MinigameLauncherController : UnitySingleton<MinigameLauncherController>
{
    [Header("UI Setup")]
    [SerializeField] private TextMeshProUGUI MinigameNameText = null;
    [SerializeField] private TextMeshProUGUI CreatorNameText = null;
    [SerializeField] private TextMeshProUGUI MinigameDescriptionText = null;
    [SerializeField] private TextMeshProUGUI P1ReadyText = null;
    [SerializeField] private TextMeshProUGUI P2ReadyText = null;
    [SerializeField] private TextMeshProUGUI GamesWonText = null;
    [SerializeField] private TextMeshProUGUI CountdownText = null;
    [SerializeField] private TextMeshProUGUI P1Objective= null;
    [SerializeField] private TextMeshProUGUI P2Objective = null;
    [SerializeField] private TextMeshProUGUI P1JoystickText = null;
    [SerializeField] private TextMeshProUGUI P1Button1Text = null;
    [SerializeField] private TextMeshProUGUI P1Button2Text = null;
    [SerializeField] private TextMeshProUGUI P2JoystickText = null;
    [SerializeField] private TextMeshProUGUI P2Button1Text = null;
    [SerializeField] private TextMeshProUGUI P2Button2Text = null;
    [SerializeField] private Image MinigameThumbnail  = null;
    [SerializeField] private float CountdownTimerDuration = 3;

    [Header("Debug")]
    [SerializeField] private MinigameInfo DEBUG_MinigamePreview = null;
    private float CountdownTimer;

    bool P1Ready = false;
    bool P2Ready = false;

    void Start()
    {
        if (GameState.Instance.IsGameStateValid())
        {
            GameState.Instance.CurrentMinigame = GameState.Instance.SelectedMinigames[GameState.Instance.MinigamesPlayed];
            PopulateUI(false);
        }
        else if(Application.isEditor && DEBUG_MinigamePreview)
        {
            GameState.Instance.CurrentMinigame = DEBUG_MinigamePreview;
            PopulateUI(true);
        }
    }

    public void PopulateUI(bool IsInDebugMode)
    {
        if (GameState.Instance.IsGameStateValid() || IsInDebugMode)
        {
            MinigameNameText.SetText(GameState.Instance.CurrentMinigame.Name);
            CreatorNameText.SetText("by " + GameState.Instance.CurrentMinigame.CreatorNames);
            MinigameDescriptionText.SetText(GameState.Instance.CurrentMinigame.Description);
            P1ReadyText.SetText("Player 1" + (P1Ready ? " " : " NOT ") + "ready" + (P1Ready ? " " : "\n Press Button 1 To be ready "));
            P2ReadyText.SetText("Player 2" + (P2Ready ? " " : " NOT ") + "ready" + (P2Ready ? " " : "\n Press Button 1 To be ready "));
            
            P1Objective.SetText(GameState.Instance.CurrentMinigame.P1_Objective);
            P2Objective.SetText(GameState.Instance.CurrentMinigame.P2_Objective);

            P1JoystickText.SetText(GameState.Instance.CurrentMinigame.P1_JoystickDescription);
            P2JoystickText.SetText(GameState.Instance.CurrentMinigame.P2_JoystickDescription);

            P1Button1Text.SetText(GameState.Instance.CurrentMinigame.P1_Button1Description);
            P2Button1Text.SetText(GameState.Instance.CurrentMinigame.P2_Button1Description);

            P1Button2Text.SetText(GameState.Instance.CurrentMinigame.P1_Button2Description);
            P2Button2Text.SetText(GameState.Instance.CurrentMinigame.P2_Button2Description);

            if (GameState.Instance.Gamemode == MinigameGamemodeTypes.TWOPLAYERVS)
            {
                GamesWonText.SetText(GameState.Instance.MinigamesWonByP1.ToString() + " Score " + GameState.Instance.MinigamesWonByP2.ToString());
            }
            else
            {
                GamesWonText.SetText("Wins: " + GameState.Instance.MinigamesWon.ToString());
            }

            MinigameThumbnail.sprite = GameState.Instance.CurrentMinigame.Thumbnail;
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("P1_Button1"))
        {
            P1Ready = true;
            P1ReadyText.SetText("Player 1" + (P1Ready ? " " : " NOT ") + "ready" + (P1Ready ? " " : "\n Press Button 1 To be ready "));
        }
        if (Input.GetButtonDown("P1_Button2"))
        {
            P1Ready = false;
            P1ReadyText.SetText("Player 1" + (P1Ready ? " " : " NOT ") + "ready" + (P1Ready ? " " : "\n Press Button 1 To be ready "));
        }
        if (Input.GetButtonDown("P2_Button1"))
        {
            P2Ready = true;
            P2ReadyText.SetText("Player 2" + (P2Ready ? " " : " NOT ") + "ready" + (P2Ready ? " " : "\n Press Button 1 To be ready "));
        }
        if (Input.GetButtonDown("P2_Button2"))
        {
            P2Ready = false;
            P2ReadyText.SetText("Player 2" + (P2Ready ? " " : " NOT ") + "ready" + (P2Ready ? " " : "\n Press Button 1 To be ready "));
        }

        if(P1Ready && P2Ready)
        {
            CountdownTimer -= Time.deltaTime;
            if(CountdownTimer <= 0.0f)
            {
                StartMinigame();
            }
            else
            {
                CountdownText.SetText( Mathf.CeilToInt(CountdownTimer).ToString() );
            }
        }
        else
        {
            CountdownText.SetText("");
            CountdownTimer = CountdownTimerDuration;
        }
    }

    public void StartMinigame()
    {
        if (GameState.Instance.CurrentMinigame)
        {
            SceneTransitionController.Instance.TransitionToScene(GameState.Instance.CurrentMinigame.GameScene.ScenePath);
        }
    }
}
