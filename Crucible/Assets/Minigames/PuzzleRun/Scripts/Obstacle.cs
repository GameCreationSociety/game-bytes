using UnityEngine;

namespace PuzzleRun
{
    public class  Obstacle : MonoBehaviour
    {
        public GameObject obstacleRef1;
        public Vector3 obstacleRef1Pos;
        public GameObject obstacleRef2;
        public Vector3 obstacleRef2Pos;
        //public float speedFactor;
        public Vector3 laneSeparation = new Vector3(1.59f, 0.0f, 0.0f);
        public int height;
        public Rigidbody2D obstacle;
        public float destroyPos = -10f;
        public int laneNumber;
        private float length;
        public float lowerbound = 0.1f;
        public float upperbound = 1f;

        void Start()
        {
            length = GetComponent<SpriteRenderer>().bounds.size.y;
            obstacle = gameObject.GetComponent<Rigidbody2D>();
            obstacleRef1 = GameObject.Find("sprawnObstacle1Ref");
            obstacleRef2 = GameObject.Find("sprawnObstacle2Ref");
            obstacleRef1Pos = obstacleRef1.transform.position;
            obstacleRef2Pos = obstacleRef2.transform.position;
        }

        int getLane()
        {
            float xPos = obstacle.transform.position.x;
            if (xPos < obstacleRef2Pos.x)
            {
                return (int)((xPos - obstacleRef1Pos.x) / laneSeparation.x) + 3;
            }
            return (int)((xPos - obstacleRef2Pos.x) / laneSeparation.x) + 2;
        }

        void Update()
        {
            obstacle.velocity += Vector2.down * 0.00015f;
        
            if (obstacle.transform.position.y < destroyPos)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (height != 0 && collision.gameObject.CompareTag("Player"))
            {
                MinigameController.Instance.FinishGame(LastMinigameFinish.LOST);
            }
        }
    }
}
