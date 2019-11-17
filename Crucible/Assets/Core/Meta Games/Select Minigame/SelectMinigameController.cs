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
    [SerializeField] List<GameObject> populatableThumbnails;
    [SerializeField] GameObject selectionBox;
    List<MinigameInfo> gamesToChooseFrom;
    private PlayerWon currentWinner;
    bool isPressed = false;

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

        // gamesToChooseFrom = GameState.Instance.SelectedMinigames;

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
            populatableThumbnails[index].GetComponent<Image>().sprite = GameState.Instance.SelectedMinigames[index].Thumbnail;
        }
    }

    void CheckKeystroke() {
        switch (currentWinner) {
            case PlayerWon.P1:
                if (Input.GetAxis("P1_Horizontal") == 0 && isPressed) isPressed = false;
                else if (isPressed) return;
                if (Input.GetAxis("P1_Horizontal") > 0) { // Going right
                    if (selected < numberOfMinigamesToChooseFrom - 1) {
                        selected++;
                        selectionBox.transform.position = new Vector3(populatableObjects[selected].transform.position.x, selectionBox.transform.position.y, selectionBox.transform.position.z);
                        isPressed = true;
                    }
                } else if (Input.GetAxis("P1_Horizontal") < 0) { // Going left
                    if (selected > 0) {
                        selected--;
                        selectionBox.transform.position = new Vector3(populatableObjects[selected].transform.position.x, selectionBox.transform.position.y, selectionBox.transform.position.z);
                        isPressed = true;
                    }
                } else if (Input.GetButtonDown("P1_Button1")) { // Confirm
                    confirmedGame = true;
                }
                break;
            case PlayerWon.P2:
                if (Input.GetAxis("P2_Horizontal") == 0 && isPressed) isPressed = false;
                else if (isPressed) return;
                if (Input.GetAxis("P2_Horizontal") > 0) { // Going right
                    if (selected < numberOfMinigamesToChooseFrom - 1) {
                        selected++;
                        selectionBox.transform.position = new Vector3(populatableObjects[selected].transform.position.x, selectionBox.transform.position.y, selectionBox.transform.position.z);
                        isPressed = true;
                    }
                } else if (Input.GetAxis("P2_Horizontal") < 0) { // Going left
                    if (selected > 0) {
                        selected--;
                        selectionBox.transform.position = new Vector3(populatableObjects[selected].transform.position.x, selectionBox.transform.position.y, selectionBox.transform.position.z);
                        isPressed = true;
                    }
                } else if (Input.GetButtonDown("P2_Button1")) { // Confirm
                    confirmedGame = true;
                    isPressed = true;
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
        selectionBox.SetActive(false);
        // load selected
        LoadSelected();
    }

        // generates a random subset of minigames without repeats (if possible)
    List<MinigameInfo> RandomMinigamesSubset()
    {
        List<MinigameInfo> possibleChoiceCopy = new List<MinigameInfo>(GameState.Instance.SelectedMinigames);
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
                    int randomInt2 = Mathf.RoundToInt(Random.Range(0, GameState.Instance.SelectedMinigames.Length));
                    // add the random minigame to the result list
                    result.Add(GameState.Instance.SelectedMinigames[randomInt2]);
                }
            }
        }
        return result;
    }
}
