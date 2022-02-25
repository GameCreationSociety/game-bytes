using UnityEngine;

namespace MarbleRoller
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D body;
        public int playerNumber;
        public int rollSpeed;
        public int maxRollSpeed;
        public int airSpeedIncreaseLimit; //Limits how much faster the palyer can speed up in air

        private bool canJump;
        private bool respawnOverLap;

        private float doubleJump;
    

        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
            canJump = true;
            respawnOverLap = true;
            doubleJump = 0;
            airSpeedIncreaseLimit = maxRollSpeed / 3;
        }


        public void ChangeRespawnState(bool newState)
        { 
            respawnOverLap = newState;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {     
            ContactPoint2D point = collision.GetContact(0);
            if (point.point.y < transform.position.y- 0.2f)
            {
                canJump = true;
                doubleJump = 0;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            ContactPoint2D point = collision.GetContact(0);
        
            // Movement is allowed only when the player is touching the floor
            if (point.point.y < transform.position.y - 0.01f)
            {

                ///transform.position += new Vector3(MinigameInputHelper.GetHorizontalAxis(playerNumber) * 5.0f * Time.deltaTime ,0,0);
                if (MinigameInputHelper.GetHorizontalAxis(playerNumber) == 1)
                {
                    body.AddForce(new Vector3(rollSpeed, 0, 0));
                }
                else if (MinigameInputHelper.GetHorizontalAxis(playerNumber) == -1)
                {
                    body.AddForce(new Vector3(-1 * (rollSpeed), 0, 0));
                }
            }
            if (body.velocity.x < airSpeedIncreaseLimit)
            {
                ///transform.position += new Vector3(MinigameInputHelper.GetHorizontalAxis(playerNumber) * 5.0f * Time.deltaTime ,0,0);
                if (MinigameInputHelper.GetHorizontalAxis(playerNumber) == 1)
                {
                    body.AddForce(new Vector3(rollSpeed/2 , 0, 0));
                }
                else if (MinigameInputHelper.GetHorizontalAxis(playerNumber) == -1)
                {
                    body.AddForce(new Vector3(-1 * (rollSpeed/2), 0, 0));
                }
            }
        
        }

        // Update is called once per frame
        void Update()
        {
       
            if (doubleJump > 1) {canJump = false;}

            //sets body velocity to clamped x value, and the normal y value
            body.velocity.Set(Mathf.Clamp(body.velocity.x, -1 * (maxRollSpeed), maxRollSpeed),body.velocity.y);


            if (respawnOverLap) { gameObject.layer = 10; }
            else { gameObject.layer = 0; }

            if (canJump)
            {
                ///if (MinigameInputHelper.GetVerticalAxis(playerNumber) == 1) // from -1 (down) to 1 (up)
                if (MinigameInputHelper.IsButton1Down(playerNumber) == true)
                {
                    ///transform.position += new Vector3(0, MinigameInputHelper.GetVerticalAxis(playerNumber) * 3.0f, 0);
                    body.velocity = new Vector2(body.velocity.x, 10);
                    ///body.AddForce(new Vector3(0, 750, 0));
                    doubleJump += 1;
                }
            }
        }
    }
}