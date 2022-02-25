using UnityEngine;

namespace Bombastic
{
    public class SFXPlayer : MonoBehaviour
    {
        //create variable for each sound effect
        public AudioSource PlatformCollision;
        public AudioSource PlayerCollision;
        public AudioSource powerUp;
        Rigidbody2D thisRigidBody;
        bool isFalling;

        void Start()
        {
            thisRigidBody = GetComponent<Rigidbody2D>();
            isFalling = false;
        }

        void Update()
        {
            if (thisRigidBody.velocity.y < -5)
            {
                isFalling = true;
            }
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            //play sound when touch wall/floor
            if (col.gameObject.tag == "wall" && isFalling && transform.position.y > col.gameObject.transform.position.y + 0.75f)
            {
                PlatformCollision.Play();
                isFalling = false;
            }
            //play sound when touch player
            else if (col.gameObject.tag == "Player")
            {
                PlayerCollision.Play();
            }

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            //destorys powerup when a player picks it up, clears existing powerups, then applies the powerup
            if (col.gameObject.tag == "powerUp" && !this.gameObject.GetComponent<PowerUpPlayer>().hasPowerUp)
            {
                powerUp.Play();
                this.gameObject.GetComponent<PowerUpPlayer>().hasPowerUp = true;
            }

        }

    }
}