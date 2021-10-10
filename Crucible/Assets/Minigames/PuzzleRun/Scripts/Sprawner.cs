using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprawner : MonoBehaviour
{
    public Player1 player1;
    public Player2 player2;
    public float player1_y;
    public float player2_y;
    public bool player1_obstacle = false;
    public bool player2_obstacle = false;
    public bool player1_button = false;
    public bool player2_button = false;
    public int obstacle_mode = 0;

    public Obstacle lane1;
    public Obstacle lane2;
    public Obstacle lane3;
    public Obstacle lane4;
    public Obstacle lane5;
    public Obstacle lane6;
    public Obstacle[] curr_obstacles = new Obstacle[6];

    void Start()
    {
        // import players
        player1 = GetComponent<Player1>();
        player2 = GetComponent<Player2>();  
        player1_y = player1.transform.position.y;
        player2_y = player2.transform.position.y;    

        // import obstacles
        lane1 = GetComponentInChildren<Lane1>();
        lane2 = GetComponentInChildren<Lane2>();
        lane3 = GetComponentInChildren<Lane3>();
        lane4 = GetComponentInChildren<Lane4>();
        lane5 = GetComponentInChildren<Lane5>();
        lane6 = GetComponentInChildren<Lane6>();

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

        curr_obstacles[0] = lane1;
        curr_obstacles[1] = lane2;
        curr_obstacles[2] = lane3;
        curr_obstacles[3] = lane4;
        curr_obstacles[4] = lane5;
        curr_obstacles[5] = lane6;

    }

    void Update()
    {   
        // check if obstacle is at bottom:
        if (player1_obstacle) {
            float obstacle_y = lane1.transform.position.y;
            if (obstacle_y <= player1_y) {
                int player1_lane = player1.laneNumber;
                //if (curr_obstacles[player1_lane].height != 0) {
                //    MinigameController.Instance.FinishGame(LastMinigameFinish.TIE);
                //} else {
                    // generate new set of obstacles
                //}
            }
        } else if (player2_obstacle) {

        }

        
        // if some condition (time passes / no more obstacle on screen):
        // generate a set of heights and update isOnScreen to True
        // for the corresponding obstacle
    }

}
