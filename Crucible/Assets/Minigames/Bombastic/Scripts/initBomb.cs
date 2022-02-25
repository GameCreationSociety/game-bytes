using UnityEngine;

namespace Bombastic
{
    public class initBomb : MonoBehaviour
    {
        public GameObject p1;
        public GameObject p2;
        public GameObject p1_bomb;
        public GameObject p2_bomb;

        // Start is called before the first frame update
        void Start()
        {
            //flip a coin
            System.Random rnd = new System.Random();
            int x = rnd.Next(1, 3);

            //player 1 has bomb
            if (x == 1)
            {
                p1_bomb.GetComponent<SpriteRenderer>().enabled = true;
                p1_bomb.GetComponent<ParticleSystem>().Play();
                p1.gameObject.GetComponent<Tag>().isTagged = true;
                p1.gameObject.GetComponent<MovementController>().defaultMoveSpeed += 1.5f;
                p1.gameObject.GetComponent<MovementController>().moveSpeed += 1.5f;

                p2_bomb.GetComponent<SpriteRenderer>().enabled = false;
                p2_bomb.GetComponent<ParticleSystem>().Stop();
                p2.gameObject.GetComponent<Tag>().isTagged = false;
                MinigameController.Instance.AddScore(2, 1);
            }
            //player 2 has bomb
            else
            {
                p2_bomb.GetComponent<SpriteRenderer>().enabled = true;
                p2_bomb.GetComponent<ParticleSystem>().Play();
                p2.gameObject.GetComponent<Tag>().isTagged = true;

                p1_bomb.GetComponent<SpriteRenderer>().enabled = false;
                p1_bomb.GetComponent<ParticleSystem>().Stop();
                p1.gameObject.GetComponent<Tag>().isTagged = false;
                MinigameController.Instance.AddScore(1, 1);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
