using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sprawner : MonoBehaviour
{
    public Obstacle currObstacle;
    public List<Obstacle> obstacles = new List<Obstacle>();

    void Start()
    {
        // import obstacles
        FWW fww = GetComponentInChildren<FWW>();
        obstacles.Add(fww);

        //currObstacle = fww;
        //currObstacle.isOnScreen = true;

    }

    void Update()
    {
        // if some condition (time passes / no more obstacle on screen):
        // generate a set of heights and update isOnScreen to True
        // for the corresponding obstacle
    }

}
