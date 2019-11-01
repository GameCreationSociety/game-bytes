using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectMinigameController : UnitySingleton<SelectMinigameController>
{
    private int selected = -1;
    [SerializeField] private float choiceCounter = 10;
    [SerializeField] TextMeshProUGUI chooseMinigameTimer;
    [SerializeField] KeyCode Choice1;
    [SerializeField] KeyCode Choice2;
    [SerializeField] GameSettings settings;
    private List<MinigameInfo> minigameOptions;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChooseMinigame());
    }

    // Update is called once per frame
    void CheckKeystroke() {
        if (Input.GetKey(Choice1)) {
            selected = 1;
        } else if (Input.GetKey(Choice2)) {
            selected = 2;
        }
    }

    void LoadSelected() {
        switch (selected) {
            case 2:
                GameState.Instance.MinigamesPlayed = 1; 
                break;
            default: // or case 1
                GameState.Instance.MinigamesPlayed = 0;
                break;
        }
        Debug.Log(GameState.Instance.MinigamesPlayed);
        SceneTransitionController.Instance.TransitionToScene("MinigameLauncher");
    }

    IEnumerator ChooseMinigame () {
        while (selected == -1 && choiceCounter > 1)
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
