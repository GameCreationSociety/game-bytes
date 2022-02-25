using UnityEngine;

namespace MarbleRoller
{
    public class initiate_End : MonoBehaviour
    {
   
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Movement m = collision.GetComponent<Movement>();
                if (m)
                {
                    if (m.playerNumber == 1)
                    {
                        MinigameController.Instance.FinishGame(LastMinigameFinish.P1WIN);
                    }
                    else if (m.playerNumber == 2)
                    {
                        MinigameController.Instance.FinishGame(LastMinigameFinish.P2WIN);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
           
        }
    }
}
