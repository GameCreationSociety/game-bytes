using UnityEngine;

//test
namespace Bombastic
{
    public class MovementController : MonoBehaviour
    {
        public Animator animator;
        public Animator bombAnimator;
        public Transform bombTransform;
        //scaling factors for movement -- not affected by powerups
        public float defaultMoveSpeed;
        public float defaultJumpForce;
        //scaling facotrs for movement -- affected by powerups
        public float moveSpeed;
        float jumpForce;
        //player in control of this object
        public int playerNumber;

        Rigidbody2D thisRigidBody;
        private Vector2 inputVector;
        private Vector3 velVector;

        public bool hasDoubleJump = false;
        bool doubleJumpUsed = false;
        public bool hasJetPack = false;
        public bool hasDash;
        public bool isDashing;
        public float jetPackVelocity = 12.0f;

        public float stunDuration = 0.75f;
        public bool stunned = false;
        public float stunTime = 0;


        bool tagged;
        // Start is called before the first frame update
        void Start()
        {
            thisRigidBody = GetComponent<Rigidbody2D>();
            moveSpeed = defaultMoveSpeed;
            jumpForce = defaultJumpForce;
            animator.SetInteger("playerNumber", playerNumber);
        }

        // Update is called once per frame
        void Update()
        {
            //Don't take any input if stunned
            if (stunned)
            {
                stunTime -= Time.deltaTime;
                if (stunTime <= 0)
                {
                    stunned = false;
                }
                thisRigidBody.velocity = new Vector2(0, thisRigidBody.velocity.y);
            }
            else
            {

                //Horizontal movement. Maintains y velocity
                inputVector = new Vector2(MinigameInputHelper.GetHorizontalAxis(playerNumber) * moveSpeed, thisRigidBody.velocity.y);
                if (inputVector != thisRigidBody.velocity && !isDashing)
                {
                
                    thisRigidBody.velocity = inputVector;
                }

                //Jump input
                if (MinigameInputHelper.IsButton1Down(playerNumber) || MinigameInputHelper.GetVerticalAxis(playerNumber) == 1)
                {

                    //Only jumps if the player is not already jumping or falling
                    if (thisRigidBody.velocity.y < 0.01f && thisRigidBody.velocity.y > -0.01f)
                    {
                        //jump by adding upward force
                        thisRigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                        doubleJumpUsed = false;
                        animator.SetBool("isJumping", true);
                    }
                    //Double jump
                    else if (!doubleJumpUsed && hasDoubleJump)
                    {
                        thisRigidBody.velocity = new Vector3(thisRigidBody.velocity.x, 0, 0);
                        thisRigidBody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                        doubleJumpUsed = true;
                    }

                }
                else if (thisRigidBody.velocity.y < 0.01f && thisRigidBody.velocity.y > -0.01f)
                {
                    animator.SetBool("isJumping", false);
                }

                // Dash
                if (MinigameInputHelper.IsButton2Down(playerNumber) && hasDash && !isDashing)
                {
                    if (this.gameObject.GetComponent<SpriteRenderer>().flipX == false)
                    {
                        thisRigidBody.AddForce(transform.right * 15f, ForceMode2D.Impulse);
                        isDashing = true;
                        Invoke("resetDash", 0.5f);

                    }
                    else
                    {
                        thisRigidBody.AddForce(transform.right * -15f, ForceMode2D.Impulse);
                        isDashing = true;
                        Invoke("resetDash", 0.5f);
                    }
                }


                //Jetpack input
                if (MinigameInputHelper.IsButton1Held(playerNumber) && hasJetPack)
                {
                    velVector = new Vector3(thisRigidBody.velocity.x, jetPackVelocity, 0);
                    thisRigidBody.velocity = velVector;

                }
            }
            //Animation stuff
            tagged = GetComponent<Tag>().isTagged;
            animator.SetFloat("verticalVelocity", thisRigidBody.velocity.y);
            GameObject bomb = GetComponent<Tag>().bomb;

            //Adjust speed for moving platforms
            if (transform.parent != null && transform.parent.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                Vector2 parentVelocity = transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity;
                thisRigidBody.velocity += parentVelocity;
                animator.SetFloat("horizontalSpeed", Mathf.Abs(thisRigidBody.velocity.x - parentVelocity.x));
                if (thisRigidBody.velocity.x - parentVelocity.x > 0.1)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    bomb.transform.localPosition = new Vector3(Mathf.Abs(bomb.transform.localPosition.x), bomb.transform.localPosition.y, bomb.transform.localPosition.z);
                }
                else if (thisRigidBody.velocity.x - parentVelocity.x < -0.1)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    bomb.transform.localPosition = new Vector3(-1f * Mathf.Abs(bomb.transform.localPosition.x), bomb.transform.localPosition.y, bomb.transform.localPosition.z);
                }
            }
            else
            {
                animator.SetFloat("horizontalSpeed", Mathf.Abs(thisRigidBody.velocity.x));
                //Flip the sprite
                if (thisRigidBody.velocity.x > 0.1)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    bomb.transform.localPosition = new Vector3(Mathf.Abs(bomb.transform.localPosition.x), bomb.transform.localPosition.y, bomb.transform.localPosition.z);
                }
                else if (thisRigidBody.velocity.x < -0.1)
                {
                    this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    bomb.transform.localPosition = new Vector3(-1f * Mathf.Abs(bomb.transform.localPosition.x), bomb.transform.localPosition.y, bomb.transform.localPosition.z);
                }
            }
        

            //Explode if game is complete
            float completion = MinigameController.Instance.GetPercentTimePassed();
            if (completion >= 1)
            {
                bombAnimator.SetTrigger("explode");
                //Stop bomb particles
                bombAnimator.gameObject.GetComponent<ParticleSystem>().Stop();
                bombAnimator.gameObject.GetComponent<ParticleSystem>().Clear();
                //Scale the bomb to make the exlosion big
                bombTransform.localScale = new Vector2(2, 2);
   
            }
        
        
        }

        public void setMoveSpeed(float spd)
        {
            moveSpeed = spd;
        }
        public void setJumpForce(float frc)
        {
            jumpForce = frc;
        }
        public void resetDash()
        {
            isDashing = false;
        }
        //Attach to platforms to move with moving platforms
        void OnCollisionStay2D(Collision2D col)
        {
            //UnityEngine.Debug.Log("collision");
            if (col.gameObject.tag == "wall")
            { 
                transform.parent = col.gameObject.transform;
            }
        }
        void  OnCollisionExit2D(Collision2D col)
        {
            if(col.gameObject.tag == "wall")
            {
                transform.parent = null;
            }
        }
    }
}