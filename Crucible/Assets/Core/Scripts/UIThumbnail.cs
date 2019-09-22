using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIThumbnail : MonoBehaviour
{
    [SerializeField] private Image ThumbnailImage = null;
    [SerializeField] private UIThumbnailSpawner Spawner = null;
    [SerializeField] private float MoveDistance = 0.0f;
    [SerializeField] private float MoveSpeed = 5.0f;
    private RectTransform RectTransformComponent = null;

    void Start()
    {
        RectTransformComponent = GetComponent<RectTransform>();
        SetNextThumbnail();
    }

    void Update()
    {
        RectTransformComponent.anchoredPosition += new Vector2(0, MoveSpeed * Time.deltaTime);
        if( Mathf.Abs(RectTransformComponent.anchoredPosition.y - Spawner.RectTransformComponent.anchoredPosition.y) > MoveDistance)
        {
            SetNextThumbnail();
            RectTransformComponent.anchoredPosition = new Vector2(RectTransformComponent.anchoredPosition.x, Spawner.RectTransformComponent.anchoredPosition.y);
        }
    }

    void SetNextThumbnail()
    {
        MinigameInfo Minigame = Spawner.GetNextMinigame();
        ThumbnailImage.sprite = Minigame.Thumbnail;
    }
}
