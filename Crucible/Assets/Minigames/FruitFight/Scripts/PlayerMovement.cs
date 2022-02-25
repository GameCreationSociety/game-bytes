using System.Collections;
using UnityEngine;

namespace FruitFight
{
    public class PlayerMovement : MonoBehaviour
    {
        public int player_num;
        public Tongue tongue;
        public Animator anim;
        public Transform mouth;
        public AudioClip eatingSound;
        public AudioClip jumpingSound;
        public AudioClip tongueSound;

        private bool grounded = true;
        private float horizontalSpeed = .15f;
        // private float rotationSpeed = .2f;
        private float jumpPower = 1500.0f;
        private float fallPower = 100.0f;
        private Rigidbody rb;
        private Vector3 tonguePos;
        private Vector3 tongueDestination;
        private float tongueDistance = 8.5f;
        private Vector3 facingDirection;
        AudioSource audio;

        //private List<GameObject> eating;
        //private float eatingSpeed = 3.0f; 

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            audio = GetComponent<AudioSource>();

            //eating = new List<GameObject>(); 
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void FixedUpdate()
        {
            float moveHorizontal = MinigameInputHelper.GetHorizontalAxis(player_num);
            float moveVertical = MinigameInputHelper.GetVerticalAxis(player_num);

            Vector3 movement = Vector3.Normalize(new Vector3(moveHorizontal, 0.0f, moveVertical));
            anim.SetFloat("Speed", 0);


            //when tongue is not moving, you can run and jump freely 
            // if (!tongue.moving)
            {
                tonguePos = transform.position;

                if (movement.magnitude > 0.1)
                {
                    rb.MovePosition(transform.position + movement * horizontalSpeed);

                    Quaternion q = Quaternion.LookRotation(-1 * movement, Vector3.up);
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Ghost_Armature|TongueOut"))
                        rb.MoveRotation(q);
                    facingDirection = Vector3.Normalize(movement);
                    anim.SetFloat("Speed", moveHorizontal * moveHorizontal + moveVertical * moveVertical);

                }
                if (grounded && MinigameInputHelper.IsButton1Held(player_num))
                {
                    anim.SetBool("Grounded",false);
                    anim.SetTrigger("Jump");
                    audio.PlayOneShot(jumpingSound, 1.0F);
                    grounded = false;
                    rb.AddForce(Vector3.up * jumpPower);
                }
                else
                {
                    anim.ResetTrigger("Jump");

                }
                if (!grounded && rb.transform.position.y < .01)
                {
                    grounded = true;
                    anim.SetBool("Grounded", true);

                }
                if(!grounded)
                {
                    rb.AddForce(Vector3.down * fallPower);
                }
                if (MinigameInputHelper.IsButton2Held(player_num) && ! tongue.moving)
                {
                    anim.SetTrigger("Tongue");
                    audio.PlayOneShot(tongueSound, 1.0F);

                    tongue.setDestination(transform.position + facingDirection * tongueDistance);
                }
                else
                {
                    anim.ResetTrigger("Tongue");

                }
            }

            //       //move fruits being eaten closer to the mouth 
            //       foreach(GameObject fruit in eating)
            //       {
            //           if (fruit != null)
            //           {
            //               Vector3 newPos = Vector3.Lerp(fruit.transform.position, mouth.position, eatingSpeed * Time.deltaTime);
            //               fruit.transform.position = new Vector3(newPos.x, fruit.transform.position.y, newPos.z); 
            //           }
            //       }

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Fruit>() != null)
            {
                Fruit fruit = other.gameObject.GetComponent<Fruit>();
                //other.gameObject.transform.parent = transform;
                //eating.Add(other.gameObject); 
                //StartCoroutine(DestroyObject(other.gameObject, .5f));
                //Destroy(other.gameObject);
                if (!fruit.dying)
                {
                    fruit.Cronch();
                    audio.PlayOneShot(eatingSound, 1.0F);

                    MinigameController.Instance.AddScore(player_num, 1);
                }
                //anim.SetTrigger("Eat");

            }

        }
 

        IEnumerator DestroyObject(GameObject other, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            if (other != null)
            {
                Destroy(other);
                //eating.Remove(other);
            }
        }


    }
}
