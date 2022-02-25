using UnityEngine;

namespace Bombastic
{
    public class shootHook : MonoBehaviour
    {
        // grappling hook prefab
        public GameObject grapplingHook;
        // velocity that hook is shot at
        public float hookVelocity;

        private int playerNumber;
        private GameObject newHook;

        public bool hasHook = false;


        // Update is called once per frame
        void Update()
        {
            // checks for button 2 input every frame
            if (MinigameInputHelper.IsButton2Down(playerNumber) && hasHook){

                // gets player number
                playerNumber = GetComponent<MovementController>().playerNumber;

                // gets direction player is moving
                float xDirection = MinigameInputHelper.GetHorizontalAxis(playerNumber);
                float yDirection = MinigameInputHelper.GetVerticalAxis(playerNumber);

                // only shoot the hook if the player is not sitting still
                if (xDirection != 0 || yDirection != 0){
                    shootHookInDirection(this.gameObject.transform, xDirection, yDirection);
                }
            }
        }

        // creates a hook and shoots it in a direction from a starting point
        void shootHookInDirection(Transform start, float xDirection, float yDirection)
        {
            // gets an appropriate direction vector
            Vector3 directionSpawn = Vector3.zero;
            directionSpawn.x += xDirection;
            directionSpawn.y += yDirection;

            // spawns hook slightly offset from the start point in the direction the hook will be shot
            // this prevents the hook from spawning on top of the player
            GameObject newHook = Instantiate(grapplingHook, start.position + directionSpawn, Quaternion.identity);
            // gets the rigidbody of the new hook
            Rigidbody2D hookRigidbody = newHook.GetComponent<Rigidbody2D>();

            // adds force in the direction given scaled by hookVelocity
            if (xDirection > 0.1){     
                hookRigidbody.AddForce(Vector3.right * hookVelocity, ForceMode2D.Impulse);
            }
            else if (xDirection < -0.1){
                hookRigidbody.AddForce(Vector3.left * hookVelocity, ForceMode2D.Impulse);
            }
            if (yDirection > 0.1){
                hookRigidbody.AddForce(Vector3.up * hookVelocity, ForceMode2D.Impulse);
            }
            else if (yDirection < -0.1){
                hookRigidbody.AddForce(Vector3.down * hookVelocity, ForceMode2D.Impulse);
            }
        }
    }
}
