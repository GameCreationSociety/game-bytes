using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int change;
    public Rigidbody2D button;
    public float destroyPos = -10f;

    void Start()
    {
        button = gameObject.GetComponent<Rigidbody2D>();
        button.velocity = Vector2.down * 2;
    }

    void Update()
    {
        if (button.transform.position.y < destroyPos)
        {
            Destroy(gameObject);
        }
    }
}
