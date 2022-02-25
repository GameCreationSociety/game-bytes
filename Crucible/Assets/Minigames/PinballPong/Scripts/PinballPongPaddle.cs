using UnityEngine;

namespace PinballPong
{
    public class PinballPongPaddle : MonoBehaviour
    {

        private Rigidbody2D rigidbody;

        private float MAX_VELOCITY = 4f;

        public int playerNumber;

        public GameObject leftFlipper;
        public GameObject rightFlipper;

        public float leftBound;
        public float rightBound;

        private Vector3 originalPosition;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            // Don't collide with flippers
            Physics.IgnoreLayerCollision(8, 8, true);
            Physics.IgnoreLayerCollision(8, 9, false);
        }

        // Start is called before the first frame update
        void Start()
        {
            originalPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            MovePaddle();

            if (MinigameInputHelper.IsButton1Down(playerNumber))
            {
                leftFlipper.GetComponent<PinballPongFlipper>().Flip();
            }
            if (MinigameInputHelper.IsButton1Up(playerNumber))
            {
                leftFlipper.GetComponent<PinballPongFlipper>().UnFlip();
            }
            if (MinigameInputHelper.IsButton2Down(playerNumber))
            {
                rightFlipper.GetComponent<PinballPongFlipper>().Flip();
            }
            if (MinigameInputHelper.IsButton2Up(playerNumber))
            {
                rightFlipper.GetComponent<PinballPongFlipper>().UnFlip();
            }

        
        }

        void MovePaddle()
        {
            // Set velocity
            float joystick = MinigameInputHelper.GetHorizontalAxis(playerNumber);

            Vector2 pos = transform.position;
            pos.x = pos.x + joystick * MAX_VELOCITY * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);


            transform.position = pos;
        

        
        }

        // FixedUpdate is called once per physics update
        public void ResetPosition()
        {
            transform.position = originalPosition;
        }
    }
}
