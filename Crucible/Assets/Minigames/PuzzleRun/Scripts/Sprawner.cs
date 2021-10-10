using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprawner : MonoBehaviour
{

    public bool player1_obstacle = false;
    public bool player2_obstacle = false;
    public bool player1_button = false;
    public bool player2_button = false;
    public int obstacle_mode = 0;

    void Start()
    {
        // import obstacles
        Obstacle lane1 = GetComponentInChildren<Lane1>();
        Obstacle lane2 = GetComponentInChildren<Lane2>();
        Obstacle lane3 = GetComponentInChildren<Lane3>();
        Obstacle lane4 = GetComponentInChildren<Lane4>();
        Obstacle lane5 = GetComponentInChildren<Lane5>();
        Obstacle lane6 = GetComponentInChildren<Lane6>();

        // decide which player gets obstacle and which gets button:
        obstacle_mode = Random.Range(0, 3);
        if (obstacle_mode == 0) {
            player1_obstacle = true;
            player2_obstacle = true;
        } else if (obstacle_mode == 1) {
            player1_obstacle = true;
            player2_button = true;
        } else {
            player1_button = true;
            player2_obstacle = true;
        }

        // randomly generate a set of 3 obstacles:
        if (player1_obstacle) {
            lane1.height = Random.Range(-2, 3);
            lane2.height = Random.Range(-2, 3);
            lane3.height = Random.Range(-2, 3);
            lane1.isOnScreen = true;
            lane2.isOnScreen = true;
            lane3.isOnScreen = true;

        } else if (player2_obstacle) {
            lane4.height = Random.Range(-2, 3);
            lane5.height = Random.Range(-2, 3);
            lane6.height = Random.Range(-2, 3);
            lane4.isOnScreen = true;
            lane5.isOnScreen = true;
            lane6.isOnScreen = true;
        }

    }

    void Update()
    {
        // if some condition (time passes / no more obstacle on screen):
        // generate a set of heights and update isOnScreen to True
        // for the corresponding obstacle
    }

}
