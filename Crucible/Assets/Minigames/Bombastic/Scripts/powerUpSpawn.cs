using UnityEngine;

namespace Bombastic
{
    public class powerUpSpawn : MonoBehaviour
    {
        //time between a power up being picked up and a new one spawning
        public float resetTime;
        //the powerUp prefab that wil get spawned
        public GameObject powerUp;

        //tracks whether or not spawnPowerUp is already being invoked
        private bool isRunning = false;

        // Start is called before the first frame update
        void Start()
        {
            //instantiates a power up on startup
            Instantiate(powerUp, this.gameObject.transform);
        }

        // Update is called once per frame
        void Update()
        {
            //if spawner has no power up spawn a new one after given number of seconds 
            if (transform.childCount == 0 && !isRunning)
            {
                Invoke("spawnPowerUp", resetTime);
                isRunning = true; //prevents this from being invoked constantly until a new powerUp is created
            }
        }

        void spawnPowerUp()
        {
            //creates new power up as a child of the spawner
            Instantiate(powerUp, this.gameObject.transform);
            isRunning = false;
        }
    }
}
