using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int change;
    public Rigidbody2D button;
    public float destroyPos = -10f;
    public int laneNumber;
    bool buttonPressed = false;

    public GameObject buttonRef1;
    public Vector3 buttonRef1Pos;
    public GameObject buttonRef2;
    public Vector3 buttonRef2Pos;
    public Vector3 laneSeparation = new Vector3(1.59f, 0.0f, 0.0f);

    public Sprite[] spriteArray;
    void Start()
    {
        button = gameObject.GetComponent<Rigidbody2D>();
        button.velocity = Vector2.down * 3;
    }

    int getLane()
    {
        float xPos = button.transform.position.x;
        if (xPos < buttonRef2Pos.x)
        {
            return (int)((xPos - buttonRef1Pos.x) / laneSeparation.x) + 3;
        }
        return (int)((xPos - buttonRef2Pos.x) / laneSeparation.x) + 2;
    }

    void Update()
    {
        laneNumber = getLane();
        Player1 player1 = GameObject.Find("Player1").GetComponent<Player1>();
        Player2 player2 = GameObject.Find("Player2").GetComponent<Player2>();
        if (!buttonPressed &&
            player1.transform.position.y < button.transform.position.y && 
            button.transform.position.y < player1.transform.position.y + 0.5f)
        {
            Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
            if (laneNumber == player1.laneNumber)
            {
                buttonPressed = true;
                for (int i = 0; i < obstacles.Length; i++) {
                    Obstacle curr_obstacle = obstacles[i].GetComponent<Obstacle>();
                    curr_obstacle.height += change;
                    if (curr_obstacle.height > 2) {
                        curr_obstacle.height = 2;  
                    }
                    if (curr_obstacle.height < -2)
                    {
                        curr_obstacle.height = -2;
                    }
                    SpriteRenderer spriteRenderer = curr_obstacle.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = spriteArray[curr_obstacle.height + 2];
                }
            }
            if (laneNumber == player2.laneNumber)
            {
                buttonPressed = true;
                for (int i = 0; i < obstacles.Length; i++)
                {
                    Obstacle curr_obstacle = obstacles[i].GetComponent<Obstacle>();
                    curr_obstacle.height += change;
                    if (curr_obstacle.height > 2)
                    {
                        curr_obstacle.height = 2;
                    }
                    if (curr_obstacle.height < -2)
                    {
                        curr_obstacle.height = -2;
                    }
                    SpriteRenderer spriteRenderer = curr_obstacle.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = spriteArray[curr_obstacle.height + 2];
                }
            }
        }

        if (button.transform.position.y < destroyPos)
        {
            Destroy(gameObject);
        }
    }
}
