using UnityEngine;

namespace Tetris_2p
{
    public class Tetromino_2 : MonoBehaviour
    {
        float fallTime;
        private bool isVerticalReset = true;
        private float time = 0.0f;
        private float nextMove = 0.075f;
        private float nextMoveDelta = 0.075f;
        public float fallSpeed = 1;
        public bool whereSpawn;
        public bool O;
        // Start is called before the first frame update
        void Start()
        {
            fallTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            CheckUserInput();
        }

        void CheckUserInput()
        {
            time = time + Time.deltaTime;
            //holds
            /*if (Input.GetKeyDown(KeyCode.RightShift)
            || Input.GetKeyDown(KeyCode.LeftShift) 
            && FindObjectOfType<Game>().GetHoldTime())
        {
            enabled = false;
            FindObjectOfType<Game>().SetNull(this);
            transform.rotation = Quaternion.identity;
            transform.position = FindObjectOfType<Game>().transform.position + 6 * Vector3.right + 9 * Vector3.up;
            if (!FindObjectOfType<Game>().GetHold())
            {
                FindObjectOfType<Game>().SetHold(this);
                FindObjectOfType<Spawner>().SpawnNext();
            }
            else
            {
                Tetromino t = FindObjectOfType<Game>().GetHoldTetromino();
                t.enabled = true;
                FindObjectOfType<Game>().SetHold(this);
                if (t.whereSpawn)
                {
                    t.transform.position += 38 * Vector3.up / 4
                    + 9 * Vector3.right / 4 + Vector3.down / 4;
                }
                else
                {
                    t.transform.position = 38 * Vector3.up / 4
                    + 9 * Vector3.right / 4 + Vector3.left / 4;
                }
            }
            FindObjectOfType<Game>().SetHoldTime(false);
        }*/
            //go right (joystick right)
            if (MinigameInputHelper.GetHorizontalAxis(2) > 0 && time > nextMove)
            {
                nextMove = time + nextMoveDelta;
                transform.position += Vector3.right / 2;
                if (!CheckIsValidPosition())
                {
                    transform.position += Vector3.left / 2;
                }
                else
                {
                    FindObjectOfType<Game_2>().UpdateGrid(this);
                }
                nextMove = nextMove - time;
                time = 0.0F;
            }
            //go left (joystick left)
            else if (MinigameInputHelper.GetHorizontalAxis(2) < 0 && time > nextMove)
            {
                nextMove = time + nextMoveDelta;
                transform.position += Vector3.left / 2;
                if (!CheckIsValidPosition())
                {
                    transform.position += Vector3.right / 2;
                }
                else
                {
                    FindObjectOfType<Game_2>().UpdateGrid(this);
                }
                nextMove = nextMove - time;
                time = 0.0F;
            }
            //rotate (joystick up)
            else if (isVerticalReset && MinigameInputHelper.GetVerticalAxis(2) > 0)
            {
                transform.Rotate(0, 0, -90);
                foreach (Transform mino in transform)
                {
                    mino.Rotate(0, 0, -90);
                }
                if (!CheckIsValidPosition())
                {
                    transform.Rotate(0, 0, 90);
                    foreach (Transform mino in transform)
                    {
                        mino.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    FindObjectOfType<Game_2>().UpdateGrid(this);
                }
            }
            //rotate other direction (other button)
            else if (MinigameInputHelper.IsButton2Down(2))
            {
                transform.Rotate(0, 0, 90);
                foreach (Transform mino in transform)
                {
                    mino.Rotate(0, 0, 90);
                }
                if (!CheckIsValidPosition())
                {
                    transform.Rotate(0, 0, -90);
                    foreach (Transform mino in transform)
                    {
                        mino.Rotate(0, 0, -90);
                    }
                }
                else
                {
                    FindObjectOfType<Game_2>().UpdateGrid(this);
                }
            }
            //hard drop (other button)
            else if (MinigameInputHelper.IsButton1Down(2))
            {
                while (CheckIsValidPosition())
                {
                    transform.position += Vector3.down / 2;
                }
                transform.position += Vector3.up / 2;
                FindObjectOfType<Game_2>().UpdateGrid(this);
                enabled = false;

                FindObjectOfType<Game_2>().DecreaseAboveRows();

                FindObjectOfType<Spawner_2>().SpawnNext();

            }
            //go down (joystick down)
            else if (((MinigameInputHelper.GetVerticalAxis(2) < 0) || Time.time - fallTime >= fallSpeed) && time > nextMove)
            {
                nextMove = time + nextMoveDelta;
                transform.position += Vector3.down / 2;
                fallTime = Time.time;
                if (!CheckIsValidPosition())
                {
                    transform.position += Vector3.up / 2;
                    enabled = false;
                    FindObjectOfType<Game_2>().UpdateGrid(this);

                    FindObjectOfType<Game_2>().DecreaseAboveRows();

                    FindObjectOfType<Spawner_2>().SpawnNext();
                    //FindObjectOfType<Spawner>().SpawnPreview();
                }
                else
                {
                    FindObjectOfType<Game_2>().UpdateGrid(this);
                }
                nextMove = nextMove - time;
                time = 0.0F;
            }

            isVerticalReset = !MinigameInputHelper.IsVerticalAxisInUse(2);
        }
    

        bool CheckIsValidPosition()
        {
            foreach (Transform mino in transform)
            {
                Vector2 pos = FindObjectOfType<Game_2>().IntRound(mino.position);
                Vector2 posCheck = FindObjectOfType<Game_2>().GridPosition(mino.position);
                if (!FindObjectOfType<Game_2>().CheckIfInsideGrid(pos))
                {
                    return false;
                }
                if (FindObjectOfType<Game_2>().GetGridPosition(posCheck) != null &&
                    FindObjectOfType<Game_2>().GetGridPosition(posCheck).parent != transform)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
