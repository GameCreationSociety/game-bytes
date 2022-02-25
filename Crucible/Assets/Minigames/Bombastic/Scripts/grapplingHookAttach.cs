using UnityEngine;

namespace Bombastic
{
    public class grapplingHookAttach : MonoBehaviour
    {
        private Rigidbody2D thisRigidBody;

        // number of seconds betweeen a hook hitting a wall and it despawning
        public float despawnTime;
    
        // runs on initialization of object
        void Start()
        {
            // instantiates rigid body
            thisRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        }

        // runs whenever hook collides with something
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "wall")
            {
                // sticks hook in place if it hits a wall
                thisRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
                Invoke("despawn", despawnTime);
            }
            else if (col.gameObject.tag == "Player")
            {
                // sticks hook to player if it hits one by the player its parent (needs fixed)
                this.gameObject.transform.parent = col.gameObject.transform;
                Invoke("despawn", despawnTime);
            }

        }

        // despawns hook
        void despawn()
        {
            Destroy(this.gameObject);
        }
    }
}
