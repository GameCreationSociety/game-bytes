using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject obstacleRef1;
    public Vector3 obstacleRef1Pos;
    public GameObject obstacleRef2;
    public Vector3 obstacleRef2Pos;
    public Vector3 laneSeparation = new Vector3(1.59f, 0.0f, 0.0f);
    public int height;
    public Rigidbody2D obstacle;
    public float destroyPos = -10f;
    public int laneNumber;
    private float length;
    public float lowerbound = 0f;
    public float upperbound = 0.2f;

    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.y;
        obstacle = gameObject.GetComponent<Rigidbody2D>();
        obstacle.velocity = Vector2.down * 3;
        obstacleRef1Pos = obstacleRef1.transform.position;
        obstacleRef2Pos = obstacleRef2.transform.position;
    }

    int getLane() {
        float xPos = obstacle.transform.position.x;
        if (xPos < obstacleRef2Pos.x) {
            return (int)((xPos - obstacleRef1Pos.x) / laneSeparation.x)+3;
        }
        return (int)((xPos - obstacleRef2Pos.x) / laneSeparation.x)+2;
    }

    void Update()
    {

        laneNumber = getLane();
        Player1 player1 = GameObject.Find("Player1").GetComponent<Player1>();
        Player2 player2 = GameObject.Find("Player2").GetComponent<Player2>();
        if (player1.transform.position.y - lowerbound < obstacle.transform.position.y && 
            obstacle.transform.position.y < player1.transform.position.y + upperbound ) {
            if (height != 0 && laneNumber == player1.laneNumber) 
            {
                MinigameController.Instance.FinishGame(LastMinigameFinish.LOST);
            }
            if (height != 0 && laneNumber == player2.laneNumber)
            {
                MinigameController.Instance.FinishGame(LastMinigameFinish.LOST);
            }
        }
        if (obstacle.transform.position.y < destroyPos)
        {
            Destroy(gameObject);
         }
    }
}
