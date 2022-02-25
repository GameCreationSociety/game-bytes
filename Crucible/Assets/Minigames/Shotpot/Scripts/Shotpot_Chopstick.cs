using UnityEngine;

namespace Shotpot
{
    public class Shotpot_Chopstick : MonoBehaviour
    {
        [SerializeField] float rotSpeed= 0.0f;
        [SerializeField] float rotLimit= 0.0f;
        [SerializeField] float startRotLimit= 0.0f;
        float startRot= 0.0f;
        private Rigidbody2D rbody;
        private Shotpot_Hand hand;

        private void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            hand = GetComponentInParent<Shotpot_Hand>();
            startRot = rbody.rotation;
        }

        // Update is called once per frame
        void Update()
        {
       
            if( MinigameInputHelper.IsButton1Held(hand.player) && !hand.isBurned())
            {
                rbody.MoveRotation(Mathf.Lerp(rbody.rotation, rotLimit, rotSpeed*Time.deltaTime));
            }   
            else
            {
                rbody.MoveRotation(Mathf.Lerp(rbody.rotation, startRot + startRotLimit, rotSpeed*Time.deltaTime));
            }
        }
    }
}
