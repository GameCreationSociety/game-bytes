using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWW : Obstacle
{

    void Start()
    {
        startPosition = transform.position;
        heights[0] = 0;
        heights[1] = 1;
        heights[2] = 1;
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
