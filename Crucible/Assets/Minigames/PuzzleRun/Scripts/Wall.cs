using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{
    void Start()
    {
        startPosition = transform.position;
        height = 1;
    }

    void Update()
    {
        if (isOnScreen) {
            transform.position -= speed * Time.deltaTime;
        }
        else {
            transform.position = startPosition;
        }
    }

}
