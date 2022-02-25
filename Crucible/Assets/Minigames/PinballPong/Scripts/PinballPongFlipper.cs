using System.Collections;
using UnityEngine;

namespace PinballPong
{
    public class PinballPongFlipper : MonoBehaviour
    {
        private float flipTime = 0.2f;

        private static int flipSteps = 10;

        public float flipAngle = 45f;
        public static Vector3 offset = new Vector3 (0f, 0.25f, 0);

        private Rigidbody2D rb;
        private Transform t;

        private static Vector3 zAxis = new Vector3(0f, 0f, 1f);

        private bool isFlipping;
        private bool isUnflipping;
        public bool isFlipped;

        public Coroutine mostRecentFlip;

        void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
            t = gameObject.GetComponent<Transform>();
            // Don't collide with other flippers or paddle
            Physics.IgnoreLayerCollision(8, 8, true);
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Flip()
        {
            StartCoroutine(DoFlip(mostRecentFlip));
        }

        public void UnFlip()
        {
            StartCoroutine(DoUnFlip(mostRecentFlip));
        }

        private IEnumerator DoFlip(Coroutine mostRecentFlip)
        {
            while (isUnflipping)
                yield return new WaitForSeconds(0.1f);

            if (isFlipped)
                yield break;

            isFlipping = true;
            isFlipped = true;
            for (int i = 0; i < flipSteps; i++)
            {
                rb.MoveRotation(rb.rotation + flipAngle / flipSteps);
                t.position += offset / flipSteps;
                yield return new WaitForSeconds(flipTime / flipSteps);
            }
            isFlipping = false;
        }

        private IEnumerator DoUnFlip(Coroutine mostRecentFlip)
        {
            while (isFlipping)
                yield return new WaitForSeconds(0.1f);

            if (!isFlipped)
                yield break;

            isUnflipping = true;
            isFlipped = false;
            for (int i = 0; i < flipSteps; i++)
            {
                rb.MoveRotation(rb.rotation - flipAngle / flipSteps);
                t.position -= offset / flipSteps;
                yield return new WaitForSeconds(flipTime / flipSteps);
            }
            isUnflipping = false;
        }
    }
}
