using UnityEngine;

namespace Tempo
{
    public class DrumNote : MonoBehaviour
    {
        public float hitTime;
        public float shrinkRate;

        public void OnEnable()
        {
            this.transform.localScale = new Vector3(2, 2, 1);
        }

        public void initialize(float hitTime, float approachTime)
        {
            this.hitTime = hitTime;
            shrinkRate = (2 - 0.6f) / approachTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObject.transform.localScale.x >= 0.6f)
                gameObject.transform.localScale -= shrinkRate * new Vector3(1, 1, 0) * Time.deltaTime;
        }
    }
}
