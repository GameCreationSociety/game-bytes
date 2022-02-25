using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Shotpot
{
    [System.Serializable]
    public struct FoodItem
    {
        public Shotpot_Food foodToSpawn;
        public float weight;
    }

    [System.Serializable]
    public struct FoodWave
    {
        public int foodCount;
        public float waitTime;
    }


    public class Shotpot_FoodSpawner : MonoBehaviour
    {
        [Header("Food Settings")]
        [SerializeField] private FoodItem[] foodsToSpawn = null;
        [SerializeField] private FoodWave[] foodWaves = null;
        [SerializeField] private Collider2D spawnZone = null;
        private int waveCounter = 0;

        IEnumerator SpawnFoodos()
        {
            yield return new WaitForSeconds(foodWaves[waveCounter].waitTime);
            SpawnWave(foodWaves[waveCounter]);
            waveCounter++;
            if(waveCounter < foodWaves.Length)
            {
                yield return SpawnFoodos();
            }
        }

        FoodItem ChooseFood(FoodItem[] probs)
        {
            float total = 0;

            foreach (FoodItem elem in probs)
            {
                total += elem.weight;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i].weight)
                {
                    return probs[i];
                }
                else
                {
                    randomPoint -= probs[i].weight;
                }
            }
            return probs[probs.Length - 1];
        }

        void SpawnWave(FoodWave wave)
        {
            for(int i = 0; i < wave.foodCount; i++)
            {
                Vector3 min = spawnZone.bounds.min;
                Vector3 max = spawnZone.bounds.max;
                Instantiate(ChooseFood(foodsToSpawn).foodToSpawn, new Vector3(Random.Range(min.x, max.x),Random.Range(min.y, max.y),Random.Range(min.z, max.z)) , new Quaternion());
            }
        }

        void Start()
        {
            StartCoroutine(SpawnFoodos());
        }
    }
}