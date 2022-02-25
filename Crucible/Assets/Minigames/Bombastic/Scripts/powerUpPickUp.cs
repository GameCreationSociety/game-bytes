using UnityEngine;

namespace Bombastic
{
    public class powerUpPickUp : MonoBehaviour
    {
        //amount that playes speed is boosted by
        public float speedBoost;

        //when something collides with this trigger, this function is called
        void OnTriggerEnter2D(Collider2D col)
        {
            //destorys powerup when a player picks it up, clears existing powerups, then applies the powerup
            if (col.gameObject.tag == "Player") {
                PowerUpPlayer powerUpPlayer = col.gameObject.GetComponent<PowerUpPlayer>();
                System.Random rnd = new System.Random();
                int powerUpType = rnd.Next(1, 6);
                powerUpPlayer.setPowerUp(powerUpType);
                Destroy(this.gameObject);
            }
  
        }
    }
}
