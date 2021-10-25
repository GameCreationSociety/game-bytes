using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprawner : MonoBehaviour
{
    private float interval;
    private float timePassed = 0;

    public GameObject obstacleRef1;
    public Vector3 obstacleRef1Pos;
    public GameObject obstacleRef2;
    public Vector3 obstacleRef2Pos;
    public GameObject buttonRef1;
    public Vector3 buttonRef1Pos;
    public GameObject buttonRef2;
    public Vector3 buttonRef2Pos;
    public Vector3 laneSeparation = new Vector3(1.59f, 0.0f, 0.0f);

    public GameObject[] obstaclePool = new GameObject[5];
    public GameObject[] buttonPool = new GameObject[3];

    private int obstacle_mode = 0;
    private bool player1_obstacle = false;
    private bool player2_obstacle = false;
    private bool player1_button = false;
    private bool player2_button = false;

    void Start()
    {
        obstacleRef1Pos = obstacleRef1.transform.position;
        obstacleRef2Pos = obstacleRef2.transform.position;
        buttonRef1Pos = buttonRef1.transform.position;
        buttonRef2Pos = buttonRef2.transform.position;
        interval = 3f;

        // decide which player gets obstacle and which gets button:
        getObstacleMode();
        if (player1_obstacle) SprawnObstacles(1);
        if (player2_obstacle) SprawnObstacles(2);
        if (player1_button) SprawnButtons(1);
        if (player2_button) SprawnButtons(2);

    }


    void Update()
    {

        timePassed += Time.deltaTime;
        if (timePassed > interval)
        {
            getObstacleMode();
            if (player1_obstacle) SprawnObstacles(1);
            if (player2_obstacle) SprawnObstacles(2);
            if (player1_button) SprawnButtons(1);
            if (player2_button) SprawnButtons(2);
        }

    }
    
    void getObstacleMode()
    {
        obstacle_mode = Random.Range(0, 3);
        if (obstacle_mode == 0)
        {
            player1_obstacle = true;
            player2_obstacle = true;
            player1_button = false;
            player2_button = false;
        }
        else if (obstacle_mode == 1)
        {
            player1_obstacle = true;
            player2_obstacle = false;
            player1_button = false;
            player2_button = true;
        }
        else
        {
            player1_obstacle = false;
            player2_obstacle = true;
            player1_button = true;
            player2_button = false;
        }
    }

    void SprawnObstacles(int playerNumber)
    {
        int lane1 = Random.Range(0, 3);
        int lane2 = Random.Range(0, 3);
        if (playerNumber == 1)
        {
            GameObject[] curr_obstacles1 = new GameObject[3];
            bool allFlat1 = true;
            for (int i = 0; i < 3; i++)
            {
                if (obstacle_mode == 0 && i == lane1)
                {
                    curr_obstacles1[i] = obstaclePool[2];
                }
                else
                {
                    int obs_i = Random.Range(1, 4);
                    curr_obstacles1[i] = obstaclePool[obs_i];
                    if (obs_i != 2) allFlat1 = false;
                }
            }
            if (allFlat1) 
            {
                int obs_i = Random.Range(0, 1) * 2 + 1;
                int i = Random.Range(0, 3);
                curr_obstacles1[i] = obstaclePool[obs_i]; 
            }
            for (int i = 0; i < 3; i++) 
            {
                Instantiate(curr_obstacles1[i], obstacleRef1Pos + laneSeparation * i, Quaternion.identity);
                timePassed = 0;
            }
        }
        else if (playerNumber == 2)
        {
            GameObject[] curr_obstacles2 = new GameObject[3];
            bool allFlat2 = true;
            for (int i = 0; i < 3; i++)
            {
                if (obstacle_mode == 0 && i == lane2)
                {
                    curr_obstacles2[i] = obstaclePool[2];
                }
                else
                {
                    int obs_i = Random.Range(1, 4);
                    curr_obstacles2[i] = obstaclePool[obs_i];
                    if (obs_i != 2) allFlat2 = false;
                }
            }
            if (allFlat2)
            {
                int obs_i = Random.Range(0, 1) * 2 + 1;
                int i = Random.Range(0, 3);
                curr_obstacles2[i] = obstaclePool[obs_i];
            }
            for (int i = 0; i < 3; i++)
            {
                Instantiate(curr_obstacles2[i], obstacleRef2Pos + laneSeparation * i, Quaternion.identity);
                timePassed = 0;
            }
        }
    }

    void SprawnButtons(int playerNumber)
    {
        int swapIndex = Random.Range(0, 3);
        for (int i = 0; i < 3; i++) {
            GameObject temp = buttonPool[swapIndex];
            buttonPool[swapIndex] = buttonPool[i];
            buttonPool[i] = temp;
        }

        if (playerNumber == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject currButton = buttonPool[i];
                Instantiate(currButton, buttonRef1Pos + laneSeparation * i, Quaternion.identity);
                timePassed = 0;
            }
        }
        if (playerNumber == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject currButton = buttonPool[i];
                Instantiate(currButton, buttonRef2Pos + laneSeparation * i, Quaternion.identity);
                timePassed = 0;
            }
        }
    }

}
