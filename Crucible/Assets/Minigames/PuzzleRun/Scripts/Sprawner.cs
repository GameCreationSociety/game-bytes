using UnityEngine;

namespace PuzzleRun
{
    public class Sprawner : MonoBehaviour
    {
        private float interval;
        private float timePassed = 0;
        public Vector2 speed;

        public GameObject obstacleRef1;
        public Vector3 obstacleRef1Pos;
        public GameObject obstacleRef2;
        public Vector3 obstacleRef2Pos;
        public GameObject buttonRef1;
        public Vector3 buttonRef1Pos;
        public GameObject buttonRef2;
        public Vector3 buttonRef2Pos;
        public Vector3 laneSeparation = new Vector3(1.59f, 0.0f, 0.0f);

        public GameObject[] obstaclePool = new GameObject[5];
        public GameObject[] buttonPool = new GameObject[3];

        public int obstacle_mode = 0;
        public bool player1_obstacle = false;
        public bool player2_obstacle = false;
        public bool player1_button = false;
        public bool player2_button = false;

        void Start()
        {
            obstacleRef1Pos = obstacleRef1.transform.position;
            obstacleRef2Pos = obstacleRef2.transform.position;
            buttonRef1Pos = buttonRef1.transform.position;
            buttonRef2Pos = buttonRef2.transform.position;
            interval = 3f;
            speed = Vector2.down * 4f;

            // decide which player gets obstacle and which gets button:
            getObstacleMode();
            if (player1_obstacle) SprawnObstacles(1);
            if (player2_obstacle) SprawnObstacles(2);
            if (player1_button) SprawnButtons(1);
            if (player2_button) SprawnButtons(2);

        }


        void Update()
        {
            timePassed += Time.deltaTime;
            speed += Vector2.down * 0.00015f;
            interval -= 0.0001f;

            if (timePassed > interval)
            {
                getObstacleMode();
                if (player1_obstacle) SprawnObstacles(1);
                if (player2_obstacle) SprawnObstacles(2);
                if (player1_button) SprawnButtons(1);
                if (player2_button) SprawnButtons(2);
            }

        }
    
        void getObstacleMode()
        {
            obstacle_mode = Random.Range(0, 3);
            if (obstacle_mode == 0)
            {
                player1_obstacle = true;
                player2_obstacle = true;
                player1_button = false;
                player2_button = false;
            }
            else if (obstacle_mode == 1)
            {
                player1_obstacle = true;
                player2_obstacle = false;
                player1_button = false;
                player2_button = true;
            }
            else
            {
                player1_obstacle = false;
                player2_obstacle = true;
                player1_button = true;
                player2_button = false;
            }
        }

        void SprawnObstacles(int playerNumber)
        {
            int lane1 = Random.Range(0, 3);
            int lane2 = Random.Range(0, 3);
            if (playerNumber == 1)
            {
                GameObject[] curr_obstacles1 = new GameObject[3];
                bool allFlat1 = true;
                for (int i = 0; i < 3; i++)
                {
                    if (obstacle_mode == 0 && i == lane1)
                    {
                        curr_obstacles1[i] = obstaclePool[2];
                    }
                    else
                    {
                        int obs_i = Random.Range(1, 4);
                        curr_obstacles1[i] = obstaclePool[obs_i];
                        if (obs_i != 2) allFlat1 = false;
                    }
                }
                if (allFlat1) 
                {
                    int obs_i = Random.Range(0, 1) * 2 + 1;
                    int i = Random.Range(0, 3);
                    curr_obstacles1[i] = obstaclePool[obs_i]; 
                }
                for (int i = 0; i < 3; i++)
                {
                    GameObject curr_obstacle1 = Instantiate(curr_obstacles1[i], obstacleRef1Pos + laneSeparation * i, Quaternion.identity);
                    curr_obstacle1.GetComponent<Rigidbody2D>().velocity = speed;
                    timePassed = 0;
                }
            }
            else if (playerNumber == 2)
            {
                GameObject[] curr_obstacles2 = new GameObject[3];
                bool allFlat2 = true;
                for (int i = 0; i < 3; i++)
                {
                    if (obstacle_mode == 0 && i == lane2)
                    {
                        curr_obstacles2[i] = obstaclePool[2];
                    }
                    else
                    {
                        int obs_i = Random.Range(1, 4);
                        curr_obstacles2[i] = obstaclePool[obs_i];
                        if (obs_i != 2) allFlat2 = false;
                    }
                }
                if (allFlat2)
                {
                    int obs_i = Random.Range(0, 1) * 2 + 1;
                    int i = Random.Range(0, 3);
                    curr_obstacles2[i] = obstaclePool[obs_i];
                }
                for (int i = 0; i < 3; i++)
                {
                    GameObject curr_obstacle2 = Instantiate(curr_obstacles2[i], obstacleRef2Pos + laneSeparation * i, Quaternion.identity);
                    curr_obstacle2.GetComponent<Rigidbody2D>().velocity = speed;
                    timePassed = 0;
                }
            }
        }

        void SprawnButtons(int playerNumber)
        {
            int swapIndex = Random.Range(0, 3);
            for (int i = 0; i < 3; i++) {
                GameObject temp = buttonPool[swapIndex];
                buttonPool[swapIndex] = buttonPool[i];
                buttonPool[i] = temp;
            }

            if (playerNumber == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject currButton1 = Instantiate(buttonPool[i], buttonRef1Pos + laneSeparation * i, Quaternion.identity);
                    currButton1.GetComponent<Rigidbody2D>().velocity = speed;
                    timePassed = 0;
                }
            }
            if (playerNumber == 2)
            {
                for (int i = 0; i < 3; i++)
                {   
                    GameObject currButton2 = Instantiate(buttonPool[i], buttonRef2Pos + laneSeparation * i, Quaternion.identity);
                    currButton2.GetComponent<Rigidbody2D>().velocity = speed;
                    timePassed = 0;
                }
            }
        }

    }
}
