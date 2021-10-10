using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane3 : Obstacle
{
    SpriteRenderer m_SpriteRenderer;
    void Start()
    {
        startPosition = transform.position;
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = spriteArray[height + 2];
    }

    void Update()
    {
        if (isOnScreen)
        {
            transform.position -= speed * Time.deltaTime;
        }
        else
        {
            transform.position = startPosition;
        }
        //m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = spriteArray[height + 2];
    }

}
