using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Tetris_2p
{
    public class Spawner_1 : MonoBehaviour
    {
        public Tetromino_1 block1;
        public Tetromino_1 block2;
        public Tetromino_1 block3;
        public Tetromino_1 block4;
        public Tetromino_1 block5;
        public Tetromino_1 block6;
        public Tetromino_1 block7;
        public List<Tetromino_1> order1;
        public List<Tetromino_1> order2;

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

            Vector2 pos = FindObjectOfType<Game_1>().GridPosition(transform.position
                                                                  + 38 * Vector3.up / 4 + 9 * Vector3.right / 4);
            if (FindObjectOfType<Game_1>().GetGridPosition(pos) == null)
            {
                //FindObjectOfType<Game_1>().SetHoldTime(true);
                if (order1[0].whereSpawn)
                {
                    if(order1[0].O)
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
                MinigameController.Instance.FinishGame(LastMinigameFinish.P2WIN);
            }
        }

        /*public bool CheckSpawn(Tetromino_1 t)
    {
        print(t.transform.position);
        foreach (Transform mino in t.transform)
        {
            Vector2 a = new Vector2(mino.position.x - 10f, mino.position.y);

            Vector2 pos = FindObjectOfType<Game_1>().GridPosition(a);
            print(pos);
            if (FindObjectOfType<Game_1>().GetGridPosition(pos) != null)
            {
                return false;
            }
        }
        return true;
    }*/

        private void AddBlocks(List<Tetromino_1> T, Tetromino_1 a1,
            Tetromino_1 a2, Tetromino_1 a3, Tetromino_1 a4,
            Tetromino_1 a5, Tetromino_1 a6, Tetromino_1 a7)
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

    static class Ext
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}