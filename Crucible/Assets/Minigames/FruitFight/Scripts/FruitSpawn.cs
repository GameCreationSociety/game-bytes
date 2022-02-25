using UnityEngine;

namespace FruitFight
{
    public class FruitSpawn : MonoBehaviour
    {
        public GameObject[] fruit_prefabs;
        public float x_min; 
        public float x_max;
        public float z_min;
        public float z_max;
        public float y;
        public float interval_min;
        public float interval_max;
        AudioSource audio; 

        // Start is called before the first frame update
        void Start()
        {
            audio = GetComponent<AudioSource>();

            Invoke("SpawnRandom", UnityEngine.Random.Range(interval_min, interval_max));
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void SpawnRandom()
        {

            float x = UnityEngine.Random.Range(x_min, x_max);
            float z = UnityEngine.Random.Range(z_min, z_max);

            Instantiate(fruit_prefabs[UnityEngine.Random.Range(0, fruit_prefabs.Length)], new Vector3(x, y, z), Quaternion.identity);
            audio.Play();

            Invoke("SpawnRandom", UnityEngine.Random.Range(interval_min, interval_max));

        }
    }
}
