using System.Collections;
using UnityEngine;

namespace StarFall
{
    public class SpawnStars : MonoBehaviour
    {   
        public GameObject Star;
        //public int spawnRate = 30;
        public float disappearTimer = 5f;
        int count = 0;
        public float spawnRate = 0.5f;
    
        private IEnumerator coroutline;

        // Start is called before the first frame update
        void Start()
        {   
            coroutline = spawnIn(spawnRate);
            StartCoroutine(coroutline);

        }

        // Update is called once per frame
        void Update()
        {
            // if(count / spawnRate == 0)
            //     {
            //         GameObject newStars = GameObject.Instantiate(Star);
            //         newStars.transform.position = new Vector3(Random.Range(-10f, 10f), 6f, 0f);
            //         Destroy(newStars, disappearTimer);
            //     }
            // count = count + 1;
        
        }


        private IEnumerator spawnIn(float waitTime)
        {   
            while(true)
            {

                //for(int i = 0; i < 5; i++)
                //{
                GameObject newStars = GameObject.Instantiate(Star);
                newStars.transform.position = new Vector3(Random.Range(-10f, 10f), 6f, 0f);
                Destroy(newStars, disappearTimer);
                yield return new WaitForSeconds(waitTime);
                //}
 
            }
            yield return null;
        }    

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if(collision.collider.tag == "Players")
        //    {
        //        Destroy(Star);        
        //    }
        //}
    
    }
}
