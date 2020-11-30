using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;

public class Tag : MonoBehaviour
{
    //tracks whether or not a player is tagged
    public bool isTagged;

    public GameObject bomb;

    int playerNumber;

    //this players sprite renderer component
    SpriteRenderer thisSpriteRenderer;
    MovementController movementController;

    // Start is called before the first frame update
    void Start()
    {
        //Get the player number from the MovementController script
        playerNumber = this.gameObject.GetComponent<MovementController>().playerNumber;
        movementController = GetComponent<MovementController>();

        thisSpriteRenderer = GetComponent<SpriteRenderer>();
        /*
        //sets the player that starts tagged to the appropriate sprite
        if (isTagged)
        {
            bomb.GetComponent<SpriteRenderer>().enabled = true;
            bomb.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            //Set the player's score to 1 since they're currently winning
            bomb.GetComponent<SpriteRenderer>().enabled = false;
            bomb.GetComponent<ParticleSystem>().Stop();
            MinigameController.Instance.AddScore(playerNumber, 1);
        }
        */
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //when two players collide the untagged player should become tagged and stunned
        if (col.gameObject.tag == "Player" && !isTagged)
        {
            isTagged = true;
            MinigameController.Instance.AddScore(playerNumber, -1);
            bomb.GetComponent<SpriteRenderer>().enabled = true;
            bomb.GetComponent<ParticleSystem>().Play();
            movementController.stunned = true;
            movementController.stunTime = movementController.stunDuration;

        }
        //the tagged player should become untagged
        else if (col.gameObject.tag == "Player" && isTagged)
        {
            isTagged = false;
            MinigameController.Instance.AddScore(playerNumber, 1);
            bomb.GetComponent<SpriteRenderer>().enabled = false;
            bomb.GetComponent<ParticleSystem>().Stop();
        }

    }
}
