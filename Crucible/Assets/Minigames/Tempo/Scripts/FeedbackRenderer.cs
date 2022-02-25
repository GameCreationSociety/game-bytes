using UnityEngine;

namespace Tempo
{
    public class FeedbackRenderer : MonoBehaviour
    {
        public FeedbackText excellent;
        public FeedbackText good;
        public FeedbackText bad;
        public FeedbackText miss;

        public void renderExcellent()
        {
            Instantiate(excellent, transform.position, Quaternion.identity);
        }

        public void renderGood()
        {
            Instantiate(good, transform.position, Quaternion.identity);
        }

        public void renderBad()
        {
            Instantiate(bad, transform.position, Quaternion.identity);
        }

        public void renderMiss()
        {
            Instantiate(miss, transform.position, Quaternion.identity);
        }
    }
}
