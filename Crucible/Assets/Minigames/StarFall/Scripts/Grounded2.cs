using UnityEngine;

namespace StarFall
{
    public class Grounded2 : MonoBehaviour
    {   
        GameObject Player;
        // Start is called before the first frame update
        void Start()
        {
            Player = gameObject.transform.parent.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.tag == "StarFall-Ground")
            {
                print("Here");
                Player.GetComponent<Player2Movement>().isGrounded = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.collider.tag == "StarFall-Ground")
            {
                Player.GetComponent<Player2Movement>().isGrounded = false;
            }
        }
    }
}
