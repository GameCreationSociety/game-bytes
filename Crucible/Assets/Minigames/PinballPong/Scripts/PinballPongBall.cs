using UnityEngine;

namespace PinballPong
{
    public class PinballPongBall : MonoBehaviour
    {

        public GameObject deathFX;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "DeathBarrier")
            {
                other.gameObject.GetComponent<PinballPongDeathBarrier>().BallDrop();
                PinballPongSoundManager.S.PlayDeathSound();
                GameObject fx = Instantiate(deathFX);
                fx.transform.position = gameObject.transform.position + new Vector3(0, 3f, 0);

                Destroy(gameObject);
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == 9) // Walls
                PinballPongSoundManager.S.PlayWallBounceSound();

            if (other.gameObject.layer == 8) // Paddles
                PinballPongSoundManager.S.PlayPaddleBounceSound();
        }
    }
}
