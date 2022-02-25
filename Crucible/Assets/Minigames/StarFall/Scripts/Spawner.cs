using UnityEngine;

namespace StarFall
{
    public class Spawner : MonoBehaviour
    {   
        public GameObject[] stars;
        public float startDelay = 1f;
        public float spawnInterval = 1.5f;
    
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("spawnIn", startDelay, spawnInterval);
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        void spawnIn()
        {
            int starIndex = Random.Range(0, stars.Length);
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 6f, 0f);
            Instantiate(stars[starIndex], spawnPosition, stars[starIndex].transform.rotation);

        }

    }
}
