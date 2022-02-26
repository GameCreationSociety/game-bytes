using UnityEngine;

namespace PuzzleRun
{
    public class Button : MonoBehaviour
    {
        public int change;
        public Rigidbody2D button;
        public float destroyPos = -10f;
        public int laneNumber;
        bool buttonPressed = false;

        public GameObject buttonRef1;
        public Vector3 buttonRef1Pos;
        public GameObject buttonRef2;
        public Vector3 buttonRef2Pos;
        public Vector3 laneSeparation = new Vector3(1.59f, 0.0f, 0.0f);

        public float lowerbound = 0.5f;
        public float upperbound = 0.2f;

        public Sprite[] spriteArray;

        void Start()
        {
            buttonRef1 = GameObject.Find("sprawnButton1Ref");
            buttonRef2 = GameObject.Find("sprawnButton2Ref");
            button = gameObject.GetComponent<Rigidbody2D>();
            buttonRef1Pos = buttonRef1.transform.position;
            buttonRef2Pos = buttonRef2.transform.position;
        }

        int getLane()
        {
            float xPos = button.transform.position.x;
            if (xPos < buttonRef2Pos.x)
            {
                return (int)((xPos - buttonRef1Pos.x) / laneSeparation.x) + 3;
            }
            return (int)((xPos - buttonRef2Pos.x) / laneSeparation.x) + 2;
        }

        void Update()
        {
            button.velocity += Vector2.down * 0.00015f;

            if (button.transform.position.y < destroyPos)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            laneNumber = getLane();
            Player1 player1 = GameObject.Find("Player1").GetComponent<Player1>();
            Player2 player2 = GameObject.Find("Player2").GetComponent<Player2>();

            if (!buttonPressed)
            {
                Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
                for (int i = 0; i < obstacles.Length; i++)
                {
                    Obstacle curr_obstacle = obstacles[i].GetComponent<Obstacle>();
                    curr_obstacle.height += change;
                    if (curr_obstacle.height > 2)
                    {
                        curr_obstacle.height = 2;
                    }
                    if (curr_obstacle.height < -2)
                    {
                        curr_obstacle.height = -2;
                    }
                    SpriteRenderer spriteRenderer = curr_obstacle.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = spriteArray[curr_obstacle.height + 2];
                }
            }
        }
    }
}