using UnityEngine;

namespace Shotpot
{
    public class Shotpot_HandMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed= 0.0f;
        [SerializeField] private float maxRot= 0.0f;
        [HideInInspector] public Rigidbody2D rbody = null;
        private Shotpot_Hand hand = null;

        // Start is called before the first frame update
        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            hand = GetComponent<Shotpot_Hand>();
        }

        // Update is called once per frame
        void Update()
        {
            if (hand.isBurned())
            {
                rbody.MovePosition(rbody.transform.position + new Vector3(0, hand.getBurnRatio()) * Time.deltaTime * moveSpeed);
                rbody.MoveRotation(Mathf.Lerp(rbody.rotation, 2*maxRot * Random.Range(-1.0f,1.0f), 0.4f));
            }
            else
            {
                float HorizontalAxis = MinigameInputHelper.GetHorizontalAxis(hand.player);
                float VerticalAxis = MinigameInputHelper.GetVerticalAxis(hand.player);
                rbody.MovePosition(rbody.transform.position + new Vector3(HorizontalAxis, VerticalAxis) * Time.deltaTime * moveSpeed);
                rbody.MoveRotation(Mathf.Lerp(rbody.rotation, maxRot * -HorizontalAxis, 0.1f));
            }
        }
    }
}
