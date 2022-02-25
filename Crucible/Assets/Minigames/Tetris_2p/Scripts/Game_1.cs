using UnityEngine;

namespace Tetris_2p
{
    public class Game_1 : MonoBehaviour
    {
        public static int width = 10;
        public static int height = 30;
        public Transform Tile;
        public Transform Holes;
        public static Transform[,] grid = new Transform[width, height];
        private bool holdTime = true;
        private bool hold = false;
        private Tetromino_1 t = null;
        // Start is called before the first frame update
        void Start()
        {
            float positionX = Tile.transform.position.x + transform.position.x;
            float positionY = Tile.transform.position.y + transform.position.y;
            //TileArray = new Transform[GridX][GridY];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Vector3 newPosition = new Vector3(positionX + (float)i / 2, positionY + (float)j / 2, 0);
                    Instantiate(Tile, newPosition, new Quaternion());
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateGrid(Tetromino_1 t)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (grid[x, y] != null)
                    {
                        if (grid[x, y].parent == t.transform)
                        {
                            grid[x, y] = null;
                        }
                    }
                }
            }

            foreach (Transform mino in t.transform)
            {
                Vector2 pos = GridPosition(mino.position);
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
        public void UpdateCheese(Transform T, int x, int y)
        {
            if (grid[x, y] != null)
            {
                grid[x, y] = null;
            }
            grid[x, y] = T;
        }

        public Transform GetGridPosition(Vector2 pos)
        {
            if (pos.y > height || pos.x < 0 || pos.x > width)
            {
                return null;
            }
            else
            {
                return grid[(int)pos.x, (int)pos.y];
            }
        }

        public bool CheckIfInsideGrid(Vector2 pos)
        {
            return (pos.x >= transform.position.x 
                    && pos.x < transform.position.x + 5
                    && pos.y >= transform.position.y);
        }

        public Vector2 GridPosition(Vector2 pos)
        {
            int x = (int)Mathf.Round(pos.x * 2 - 2 * transform.position.x);
            int y = (int)Mathf.Round(pos.y * 2 - 2 * transform.position.y);
            return new Vector2(x, y);
        }






        public void IncreaseAboveRows()
        {
            for (int i = 20; i > -1; i--)
            {
                IncreaseRow(i);
            }
        }

        public void IncreaseRow(int y)
        {
            for (int i = 0; i < 10; i++)
            {
                grid[i, y + 1] = grid[i, y];
                if (grid[i, y] != null)
                {
                    grid[i, y].position += Vector3.up / 2;
                }
                grid[i, y] = null;
            }
        }

        private void moveCheese(int x)
        {
            FindObjectOfType<Game_2>().IncreaseAboveRows();
            for (int i = 0; i < 10; i++)
            {
                if (i != x)
                {
                    var clone = Instantiate(Holes, FindObjectOfType<Game_2>().transform.position
                                                   + (float)i / 2 * Vector3.right, Quaternion.identity);
                    FindObjectOfType<Game_2>().UpdateCheese(clone, i, 0);
                }
            }
        }





        private bool CheckRowFull(int y)
        {
            for(int i = 0; i < 10; i++)
            {
                if (grid[i, y] == null)
                    return false;
            }
            return true;
        }

        private void ClearRow(int y)
        {
            if (grid[0, y].CompareTag("holes") ||
                grid[1, y].CompareTag("holes"))
            {
                int x = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (!grid[i, y].CompareTag("holes"))
                        x = i;
                }
                moveCheese(x);
                MinigameController.Instance.AddScore(1, 1);
            }
            for (int j = 0; j < 10; j++)
            {
                Destroy(grid[j, y].gameObject);
                grid[j, y] = null;
            }
        }
        private void DecreaseRow(int y)
        {
            for (int i = 0; i < 10; i++)
            {
                grid[i, y - 1] = grid[i, y];
                if (grid[i, y] != null)
                {
                    grid[i, y].position += Vector3.down / 2;
                }
                grid[i, y] = null;
            }
        }

        public void DecreaseAboveRows()
        {
            for (int i = 0; i < 25; i++)
            {
                if (CheckRowFull(i))
                {
                    ClearRow(i);
                    for (int j = i; j < 22; j++)
                    {
                        DecreaseRow(j + 1);
                    }
                    i--;
                }
            }
        }

        public void SetNull(Tetromino_1 tet)
        {
            foreach (Transform mino in tet.transform)
            {
                Vector2 pos = GridPosition(mino.position);
                grid[(int)pos.x, (int)pos.y] = null;
            }
        }

        public bool GetHold()
        {
            return hold;
        }

        public Tetromino_1 GetHoldTetromino()
        {
            return t;
        }

        public bool GetHoldTime()
        {
            return hold;
        }

        public void SetHold(Tetromino_1 tet)
        {
            hold = true;
            t = tet;
        }

        public void SetHoldTime(bool b)
        {
            holdTime = b;
        }

        public Vector2 IntRound(Vector2 pos)
        {
            return new Vector2(Mathf.Floor(pos.x), Mathf.Floor(pos.y));
        }
    }
}