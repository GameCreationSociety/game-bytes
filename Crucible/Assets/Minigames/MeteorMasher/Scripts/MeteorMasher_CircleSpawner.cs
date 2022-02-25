using System.Collections;
using UnityEngine;

//Spawns Rigidbodies in a circle around itself
namespace MeteorMasher
{
    public class MeteorMasher_CircleSpawner : MonoBehaviour
    {
        //Spawner parameters set in editor
        [SerializeField] private float SpawnRadius = 50;
        [SerializeField] private Rigidbody2D SpawnObject = null;
        [SerializeField] private float SpawnIntervalSeconds = 4;
        [SerializeField] private AnimationCurve SpawnIntensity = null;
        [SerializeField] private float TopBottomAngleAvoidance = 0.4f;

        void Start()
        {
            // The couroutine does all the spawning logic for us, just gotta start it
            StartCoroutine(SpawnObjects());
        }

        //Coroutine used to spawn the objects with a delay
        IEnumerator SpawnObjects()
        {
            // Don't wanna spawn things if the minigame was finished
            while(!MinigameController.Instance.MinigameEnded)
            {
                // Determine how many meteors to spawn (The spawn intensity curve tells us how many to spawn at given point in game)
                int SpawnCount = Mathf.RoundToInt(SpawnIntensity.Evaluate(MinigameController.Instance.GetPercentTimePassed()));

                // Spawn the meteors randomly in a circle around the spawner (avoiding the top and bottom of circle as specified by TopBottomAvoidance)
                // The avoidance is there cause it really sucks to get a surprise meteor coming from the top and bottom edges of the screen
                for(int i = 0; i < SpawnCount; i++)
                {
                    float RandomFlip = Random.Range(0,2) * Mathf.PI;
                    float RandomAngle = Random.Range(TopBottomAngleAvoidance, Mathf.PI - TopBottomAngleAvoidance) - Mathf.PI/2 + RandomFlip;
                    Rigidbody2D body = Instantiate(SpawnObject, transform.position + new Vector3(Mathf.Cos(RandomAngle),Mathf.Sin(RandomAngle),0) * SpawnRadius, Quaternion.identity, null);
                }

                // Wait for a given amount of time
                yield return new WaitForSeconds(SpawnIntervalSeconds);
            }
        }

        // Used in editor to preview the radius of the spawner (for designer convenience only, not in game)
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, SpawnRadius);
        }
    }
}
