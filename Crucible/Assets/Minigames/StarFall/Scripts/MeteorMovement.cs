using UnityEngine;

namespace StarFall
{
    public class MeteorMovement : MonoBehaviour
    {
        public float speed   = 10f;
        public float sliding = 0.1f;
        public int direction = 0;
        public Vector3 velocity;
        public Vector3 pos;
        // Start is called before the first frame update
        void Start()
        {
            pos = transform.position;
            while (direction == 0)
            { direction = Random.Range(0, 3) - 1; }
        }

        // Update is called once per frame
        void Update()
        {
        
            Vector3 movement = new Vector3(sliding * direction, 0f, 0f);
            transform.position += movement * Time.deltaTime * speed;
            velocity = (transform.position - pos) / Time.deltaTime;
            pos = transform.position;
        }
    }
}
