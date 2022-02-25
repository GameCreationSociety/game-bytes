using UnityEngine;

// Meteor that flies towards the center of the screen, needs to have a rigid body component to work
namespace MeteorMasher
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MeteorMasher_Meteor : MonoBehaviour
    {
        // Meteor gravity parameter tweakable in editor
        [SerializeField] private float GravityForce = 1;

        // The rigidbody component we need
        private Rigidbody2D Body;

        private void Start()
        {
            // Grab the rigid body component so that we can add force to it
            Body = GetComponent<Rigidbody2D>();
        }

        //Always gotta use fixed update for physics!
        private void FixedUpdate()
        {
            // Add the force towards the screen. I know it's not physically accurate but it works better from a gameplay standpoint
            Body.AddForce(-transform.position.normalized / transform.position.sqrMagnitude * GravityForce);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // When two meteors collide, we destroy both of them
            if(collision.GetComponent<MeteorMasher_Meteor>())
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
