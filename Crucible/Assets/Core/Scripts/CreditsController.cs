using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : UnitySingleton<CreditsController>
{
    [SerializeField] private GameSettings Settings = null;
    [SerializeField] private TextMeshProUGUI MinigameNameText = null;
    [SerializeField] private TextMeshProUGUI CreatorNameText = null;
    [SerializeField] private Image MinigameThumbnail = null;
    [SerializeField] private int transitionTime = 5;
    private bool transitionFlag;
    private bool startFlag;

    private MinigameInfo[] ShuffledMinigames;
    private int NextMinigameToShowIndex = 0;


    private void Awake()
    {
        ShuffledMinigames = Settings.GetShuffledMinigames();
        transitionFlag = true;
        startFlag = true;
    }

    private void Update() {
        // If we're not at the last game
        if (transitionFlag) {
            StartCoroutine(ShowNextGame(transitionTime));
            transitionFlag = false;
        }
    }

    public void GoBack()
    {
        SceneTransitionController.Instance.TransitionToScene(Settings.StartScene);
    }

    private void PopulateUI(MinigameInfo miniGame) {
        MinigameNameText.text = miniGame.Name;
        CreatorNameText.text = miniGame.CreatorNames;
        MinigameThumbnail.sprite = miniGame.Thumbnail;
    }

    public MinigameInfo GetNextMinigame()
    {
        // TODO: what do we do when we get through them all?
        MinigameInfo Minigame = ShuffledMinigames[NextMinigameToShowIndex];
        NextMinigameToShowIndex = (NextMinigameToShowIndex + 1) % ShuffledMinigames.Length;
        return Minigame;
    }

    IEnumerator ShowNextGame(int transitionTime) {
        // If we're not on the first minigame
        if (!startFlag) {
            // Fade previous game out for one second
            for (float i = 1; i >= 0; i -= Time.deltaTime) {
                // set color with i as alpha
                MinigameNameText.color = new Color(1, 1, 1, i);
                CreatorNameText.color = new Color(1, 1, 1, i);
                MinigameThumbnail.color = new Color(1, 1, 1, i);
                yield return null;
            }
        } else {
            startFlag = false;
        }

        // Get next minigame info
        PopulateUI(GetNextMinigame());

        // Fade next game in for one second
        for (float i = 0; i <= 1; i += Time.deltaTime) {
            // set color with i as alpha
            MinigameNameText.color = new Color(1, 1, 1, i);
            CreatorNameText.color = new Color(1, 1, 1, i);
            MinigameThumbnail.color = new Color(1, 1, 1, i);
            yield return null;
        }

        // Wait before next transition
        yield return new WaitForSeconds(transitionTime);
        transitionFlag = true;
    }

}
