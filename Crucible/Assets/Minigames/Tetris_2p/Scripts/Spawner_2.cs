using System.Collections.Generic;
using UnityEngine;

namespace Tetris_2p
{
    public class Spawner_2 : MonoBehaviour
    {
        public Tetromino_2 block1;
        public Tetromino_2 block2;
        public Tetromino_2 block3;
        public Tetromino_2 block4;
        public Tetromino_2 block5;
        public Tetromino_2 block6;
        public Tetromino_2 block7;
        public List<Tetromino_2> order1;
        public List<Tetromino_2> order2;

        // Start is called before the first frame update
        void Start()
        {
            AddBlocks(order1, block1, block2, block3, block4,
                block5, block6, block7);
            order1.Shuffle();

            AddBlocks(order2, block1, block2, block3, block4,
                block5, block6, block7);
            order2.Shuffle();
            SpawnNext();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpawnNext()
        {
            Vector2 pos = FindObjectOfType<Game_2>().GridPosition(transform.position
                                                                  + 38 * Vector3.up / 4 + 9 * Vector3.right / 4);
            if (FindObjectOfType<Game_2>().GetGridPosition(pos) == null)
            {
                //FindObjectOfType<Game_2>().SetHoldTime(true);
                if (order1[0].whereSpawn)
                {
                    if (order1[0].O)
                    {
                        Instantiate(order1[0], transform.position + 38 * Vector3.up / 4
                                                                  + 9 * Vector3.right / 4 + Vector3.up / 4, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(order1[0], transform.position + 38 * Vector3.up / 4
                                                                  + 9 * Vector3.right / 4 + Vector3.down / 4, Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(order1[0], transform.position + 38 * Vector3.up / 4
                                                              + 9 * Vector3.right / 4 + Vector3.left / 4, Quaternion.identity);
                }
                order1.RemoveAt(0);
                if (order2.Count != 0)
                {
                    order1.Add(order2[0]);
                    order2.RemoveAt(0);
                }
                else
                {
                    AddBlocks(order2, block1, block2, block3, block4,
                        block5, block6, block7);
                    order2.Shuffle();
                    order1.Add(order2[0]);
                    order2.RemoveAt(0);
                }
            }
            else
            {
                MinigameController.Instance.FinishGame(LastMinigameFinish.P1WIN);
            }
        }

        private void AddBlocks(List<Tetromino_2> T, Tetromino_2 a1,
            Tetromino_2 a2, Tetromino_2 a3, Tetromino_2 a4,
            Tetromino_2 a5, Tetromino_2 a6, Tetromino_2 a7)
        {
            T.Add(a1);
            T.Add(a2);
            T.Add(a3);
            T.Add(a4);
            T.Add(a5);
            T.Add(a6);
            T.Add(a7);
        }
    }
}