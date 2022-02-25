using UnityEngine;

namespace Bombastic
{
    public class platformMoveX : MonoBehaviour
    {
        Rigidbody2D thisRigidBody;

        public float speed;
        public float leftBound;
        public float rightBound;

        public bool moveRightFirst;
        bool moveRight;
        Vector3 movementVector;

        // Start is called before the first frame update
        void Start()
        {
            thisRigidBody = GetComponent<Rigidbody2D>();
            bool moveRight = moveRightFirst;
        }

        // Update is called once per frame
        void Update()
        {
            if (moveRight)
            {
                movementVector = Vector3.right * speed;
            } else
            {
                movementVector = Vector3.left * speed;
            }
            thisRigidBody.velocity = movementVector;
            if (transform.position.x >= rightBound) moveRight = false;
            if (transform.position.x <= leftBound) moveRight = true;

        }
    }
}
