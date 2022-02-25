using System.Collections.Generic;
using UnityEngine;

namespace Tetris_2p
{
    public class Cheese : MonoBehaviour
    {
        public Transform Tile;
        // Start is called before the first frame update
        void Start()
        {
            List<int> cheese = new List<int>();
            List<int> cheese2 = new List<int>();
            Vector3 one = FindObjectOfType<Game_1>().transform.position;
            Vector3 two = FindObjectOfType<Game_2>().transform.position;

            for (int i = 0; i < 10; i++)
            {
                int a = (int)Random.Range(0, 10);
                cheese.Add(a);
                int b = (int)Random.Range(0, 10);
                cheese2.Add(b);
            }
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    if(j != cheese[i])
                    {
                        Transform clone = Instantiate(Tile, one +(float)i / 2 * Vector3.up + (float)j / 2 * Vector3.right, Quaternion.identity);
                        clone.tag = "holes";
                        FindObjectOfType<Game_1>().UpdateCheese(clone, j, i);
                    }
                    if (j != cheese2[i])
                    {
                        var clone = Instantiate(Tile, two + (float)i / 2 * Vector3.up + (float)j / 2 * Vector3.right, Quaternion.identity);
                        clone.tag = "holes";
                        FindObjectOfType<Game_2>().UpdateCheese(clone, j, i);
                    }
                }
            }
            //FindObjectOfType<Game_2>().RandomFill(cheese);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
