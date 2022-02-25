using UnityEngine;

namespace MarbleRoller
{
    public class Respawn : MonoBehaviour
    {

        public Transform player1;
        public Transform player2;

        private int lives1;
        private int lives2;

        // Start is called before the first frame update
        void Start()
        {
            lives1 = 2; lives2 = 2;

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Movement m = other.GetComponent<Movement>();
                if (m)
                {
                    if (m.playerNumber == 1)
                    {
                        m.ChangeRespawnState(true); /// tells movement that player1 has respawned
                        
                        ///Lose Life, and check if all lives are lost
                        lives1 -= 1;
                        if (lives1 > 0) { player1.position = player2.position; }
                        else {MinigameController.Instance.FinishGame(LastMinigameFinish.P2WIN);}
                    }
                    else if (m.playerNumber == 2)
                    {

                        m.ChangeRespawnState(true); ///tells movement that player 2 has respawned

                        ///Lose Life, and check if all lives are lost
                        lives2 -= 1;
                        if (lives2 > 0) { player2.position = player1.position; }
                        else { MinigameController.Instance.FinishGame(LastMinigameFinish.P1WIN); }
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
