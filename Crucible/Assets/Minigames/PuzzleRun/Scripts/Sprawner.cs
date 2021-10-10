using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprawner : MonoBehaviour
{
    public Obstacle lane1;
    public Obstacle lane2;
    public Obstacle lane3;
    public Obstacle lane4;
    public Obstacle lane5;
    public Obstacle lane6;
    public List<Obstacle> obstacles_pool = new List<Obstacle>();
    public Obstacle[] curr_objects = new Obstacle[6];
    public int obstacles_index = 0;

    public bool player1_obstacle = false;
    public bool player2_obstacle = false;
    public bool player1_button = false;
    public bool player2_button = false;
    public int obstacle_mode = 0;

    void Start()
    {
        // import obstacles
        Wall wall = GetComponentInChildren<Wall>();
        obstacles_pool[0] = wall;
        obstacles_pool[1] = wall;
        obstacles_pool[2] = wall;
        obstacles_pool[3] = wall;
        obstacles_pool[4] = wall;

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
            player2_button = true;
        }

        // randomly generate a set of 3 obstacles:
        if (player1_obstacle) {
            for (int i = 0; i <= 2; i++) {
                obstacles_index = Random.Range(0, 5);
                curr_objects[i] = obstacles_pool[ obstacles_index ];
            }
            lane1 = curr_object[0];
            lane2 = curr_object[1];
            lane3 = curr_object[2];
        } else if (player2_obstacle) {
            for (int i = 3; i <= 5; i++) {
                obstacles_index = Random.Range(0, 5);
                curr_objects[i] = obstacles_pool[ obstacles_index ];
            lane4 = curr_object[3];
            lane5 = curr_object[4];
            lane6 = curr_object[5];
            }
        }

    }

    void Update()
    {
        // if some condition (time passes / no more obstacle on screen):
        // generate a set of heights and update isOnScreen to True
        // for the corresponding obstacle
    }

}
