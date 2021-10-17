using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int height;
    public Rigidbody2D obstacle;
    public float destroyPos = -10f;

    void Start()
    {
        obstacle = gameObject.GetComponent<Rigidbody2D>();
        obstacle.velocity = Vector2.down * 2;
    }

    void Update()
    {
        if (obstacle.transform.position.y < destroyPos)
        {
            Destroy(gameObject);
         }
    }
}
