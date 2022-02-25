using UnityEngine;

namespace StarFall
{
    public class StarMovement : MonoBehaviour
    {   
        public float speed = 10f;
        public float sliding = 0.1f;
        public int direction = 0;
    
        // Start is called before the first frame update
        void Start()
        {   
            while(direction == 0)
            {direction = Random.Range(0, 3) - 1;}
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 movement = new Vector3(sliding*direction, 0f, 0f);
            transform.position += movement * Time.deltaTime * speed;
        }
    }
}
