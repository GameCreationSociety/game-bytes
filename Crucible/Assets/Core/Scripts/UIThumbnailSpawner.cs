using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIThumbnailSpawner : MonoBehaviour
{
    [SerializeField] private GameSettings Settings = null;
    private MinigameInfo[] ShuffledMinigames;
    private int NextMinigameToShowIndex = 0;
    public RectTransform RectTransformComponent;


    private void Awake()
    {
        RectTransformComponent = GetComponent<RectTransform>();
        ShuffledMinigames = Settings.GetShuffledMinigames();
    }

    public MinigameInfo GetNextMinigame()
    {
        MinigameInfo Minigame = ShuffledMinigames[NextMinigameToShowIndex];
        NextMinigameToShowIndex = (NextMinigameToShowIndex+1) % ShuffledMinigames.Length;
        return Minigame;
    }
}
