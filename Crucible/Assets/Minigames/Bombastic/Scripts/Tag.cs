using UnityEngine;

namespace Bombastic
{
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
                movementController.defaultMoveSpeed += 1.5f;
                movementController.moveSpeed += 1.5f;

            }
            //the tagged player should become untagged
            else if (col.gameObject.tag == "Player" && isTagged)
            {
                isTagged = false;
                MinigameController.Instance.AddScore(playerNumber, 1);
                bomb.GetComponent<SpriteRenderer>().enabled = false;
                bomb.GetComponent<ParticleSystem>().Stop();
                movementController.defaultMoveSpeed -= 1.5f;
                movementController.moveSpeed -= 1.5f;
            }

        }
    }
}
