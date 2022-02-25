using UnityEngine;
using UnityEngine.UI;

namespace Tempo
{
    public class FeedbackText : MonoBehaviour
    {
        public Color initialColor, finalColor;
        public Vector3 initialOffset, finalOffset;
        public float fadeDuration;
        public Text text;
        private float fadeStartTime;

        // Start is called before the first frame update
        void Start()
        {
            fadeStartTime = Time.time;
        }

        // Update is called once per frame
        void Update()
        {
            float progress = (Time.time - fadeStartTime) / fadeDuration;
            if (progress <= 1)
            {
                //lerp factor is from 0 to 1, so we use (FadeExitTime-Time.time)/fadeDuration
                text.transform.localPosition = Vector3.Lerp(initialOffset, finalOffset, progress);
                text.color = Color.Lerp(initialColor, finalColor, progress);
            }
            else Destroy(gameObject);
        }
    }
}
