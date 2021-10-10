using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(0, 0, -1);
    public Vector3 speed = new Vector3(0, 1f, 0);
    public int height = 0;
    public bool isOnScreen = false;
    public Sprite[] spriteArray;

    void Start()
    {
        startPosition = transform.position;
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
