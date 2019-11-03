using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectMinigameController : UnitySingleton<SelectMinigameController>
{
    private enum PlayerWon {
        P1,
        P2
    }

    private int selected = 0;
    private bool confirmedGame = false;
    [SerializeField] private float choiceCounter = 10;
    [SerializeField] TextMeshProUGUI chooseMinigameTimer;
    [SerializeField] GameSettings settings;
    [SerializeField] int numberOfMinigamesToChooseFrom;
    [SerializeField] GameObject minigameThumbnailPrefab;

    [SerializeField] TextMeshProUGUI playerSelecting;
    // [SerializeField] GameObject minigameSelectionLocation;
    [SerializeField] List<GameObject> populatableObjects;
    private PlayerWon currentWinner;

    //TODO: in RPSNController, update game state


    void Start()
    {
        // If we overestimated the number of games to choose from
        if (numberOfMinigamesToChooseFrom > GameState.Instance.SelectedMinigames.Length) {
            numberOfMinigamesToChooseFrom = GameState.Instance.SelectedMinigames.Length;

            //TODO: won't need this if dynamic thumbnail display, get all minigame options, spawn thumbnail screens based on how many choices we can select
            // Reset minigame thumbnail array
            populatableObjects.RemoveRange(numberOfMinigamesToChooseFrom - 1, populatableObjects.Count - numberOfMinigamesToChooseFrom + 1);
            // minigameSelectionLocation.transform.localPosition = new Vector3(0, minigameSelectionLocation.transform.localPosition.y, minigameSelectionLocation.transform.localPosition.z);
        }

        //TODO: change UI anchors on screen
        switch (GameState.Instance.LastMetagameFinishState) {
            case LastMetagameFinish.P1WIN:
                currentWinner = PlayerWon.P1;
                playerSelecting.text = "P1";
                break;
            case LastMetagameFinish.P2WIN:
                currentWinner = PlayerWon.P2;
                playerSelecting.text = "P2";
                break;
            default: // TIE or NONE, pick random player to select
                switch (Random.Range(0, 1)) {
                    case 0:
                        currentWinner = PlayerWon.P1;
                        playerSelecting.text = "P1";
                        break;
                    case 1:
                        currentWinner = PlayerWon.P2;
                        playerSelecting.text = "P2";
                        break;
                }
                break;
        }
        PopulateUI();
        StartCoroutine(ChooseMinigame());
    }

    void PopulateUI() {
        //TODO: spawn and resize thumbnails, move them to right locations
        // based on where stuff should be and number of minigames to choose
        // also populate thumbnail and text based on gamestate minigameinfo
        for(int index = 0; index < populatableObjects.Count; index++) {
            var thumbnail = populatableObjects[index];
            thumbnail.GetComponentsInChildren<TextMeshProUGUI>()[0].text = GameState.Instance.SelectedMinigames[index].Name;
            thumbnail.transform.Find("Thumbnail").GetComponent<Image>().sprite = GameState.Instance.SelectedMinigames[index].Thumbnail;
        }
    }

    void CheckKeystroke() {
        switch (currentWinner) {
            case PlayerWon.P1:
                if (Input.GetKey("P1_Button2")) { // Going right
                    if (selected < numberOfMinigamesToChooseFrom - 1) {
                        selected++;
                    }
                } else if (Input.GetKey("P1_Button1")) { // Going left
                    if (selected > 0) {
                        selected--;
                    }
                } else if (Input.GetKey("P1_Button3")) { // Confirm
                    confirmedGame = true;
                }
                break;
            case PlayerWon.P2:
                if (Input.GetKey("P2_Button2")) { // Going right
                    if (selected < numberOfMinigamesToChooseFrom - 1) {
                        selected++;
                    }
                } else if (Input.GetKey("P2_Button1")) { // Going left
                    if (selected > 0) {
                        selected--;
                    }
                } else if (Input.GetKey("P2_Button3")) { // Confirm
                    confirmedGame = true;
                }
                break;
        }
    }

    void LoadSelected() {
        //TODO: select minigamesplayed at index of selectedMinigames
        GameState.Instance.MinigamesPlayed = selected;
        SceneTransitionController.Instance.TransitionToScene("MinigameLauncher");
    }

    IEnumerator ChooseMinigame () {
        while (choiceCounter > 1 && !confirmedGame)
        {
            choiceCounter -= Time.deltaTime;
            chooseMinigameTimer.SetText(Mathf.FloorToInt(choiceCounter).ToString());
            yield return null;
            CheckKeystroke();
        }
        // load selected
        LoadSelected();
    }
}
