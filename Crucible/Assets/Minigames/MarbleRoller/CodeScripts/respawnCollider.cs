using UnityEngine;

namespace MarbleRoller
{
    public class respawnCollider : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            respawnCollider other = collision.GetComponent<respawnCollider>();
            if (other != null)
            {
                Movement m = collision.GetComponentInParent<Movement>();
                m.ChangeRespawnState(false);
            
            }
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
