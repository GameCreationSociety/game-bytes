using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoveUp : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 5.0f;
    private RectTransform RectTransformComponent;

    void Start()
    {
        RectTransformComponent = GetComponent<RectTransform>();
    }

    void Update()
    {
        RectTransformComponent.anchoredPosition += new Vector2(0, MoveSpeed * Time.deltaTime);
    }
}
