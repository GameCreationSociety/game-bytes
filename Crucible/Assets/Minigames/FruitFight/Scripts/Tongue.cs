using UnityEngine;

namespace FruitFight
{
    public class Tongue : MonoBehaviour
    {
        public PlayerMovement player;
        public bool moving;
        public bool retracting;
        private Vector3 tongueDestination;
        private Vector3 tongueStart;
        private float tongueDuration = .958f;
        private float tongueTime = 0.0f;
        private Transform trueParent; 

        // Start is called before the first frame update
        void Start()
        {
            trueParent = transform.parent;
            //UnityEngine.Debug.Log(player.player_num + "tong S:fl!");

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            if (!player.anim.GetCurrentAnimatorStateInfo(0).IsName("Ghost_Armature|TongueOut"))
            {
                //TODO: remove all children
                foreach (Transform child in transform)
                {
                    child.parent = trueParent;
                    //   UnityEngine.Debug.Log("moved child back");

                }
            }

            if (moving)
            {
                tongueTime += Time.deltaTime;
                if (tongueTime >= tongueDuration)
                {
                    //   UnityEngine.Debug.Log(player.player_num + "tongue in");

                    moving = false;



                }
                else if (tongueTime <= 0.5 * tongueDuration)
                {
                    //moving out 
                    //transform.position = Vector3.Lerp(tongueStart, tongueDestination, tongueTime * 2.0f / tongueDuration);
                }
                else
                {
                    retracting = true;
                    //retract tongue
                    // transform.position = Vector3.Lerp(tongueDestination, tongueStart, tongueTime * 2.0f / tongueDuration - 1.0f);

                    //TODO: pull back all stuck objects
                }
                //hardcode y
                // transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            }

            //just follow the player
            else
            {
                //if(player) transform.position = player.transform.position;

                //hardcode y
                // transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }


        public void setDestination(Vector3 dest)
        {
            tongueTime = 0.0f;
            tongueDestination = dest;
            tongueStart = transform.position;
            moving = true;
            retracting = false;
            //clear prev 
            foreach (Transform child in transform)
            {
                child.parent = trueParent;
            }
        }


        void OnTriggerEnter(Collider other)
        {
            //UnityEngine.Debug.Log(player.player_num + "collision!");
            if (moving && (other.gameObject.tag == "fruit" || (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerMovement>() != player && player.transform.parent == trueParent)))
            {
                other.gameObject.transform.parent = transform;
                //UnityEngine.Debug.Log(player.player_num + "caught a fruit!");

            }
        }
    }
}