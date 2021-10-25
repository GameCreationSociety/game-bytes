using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Rigidbody2D background;
    public float startPos, length;

    // Start is called before the first frame update
    void Start()
    {
        background = gameObject.GetComponent<Rigidbody2D>();
        background.velocity = Vector2.down * 3;
        startPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.08)
        {
            transform.position = new Vector3(transform.position.x,
                startPos,
                transform.position.z);
        }
    }
}
