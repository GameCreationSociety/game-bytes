using UnityEngine;

namespace Tempo
{
    public class GuitarNote : MonoBehaviour
    {
        public float hitTime;
        public bool isChord;
        public Vector3 velocity;

        public void OnEnable()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }

        public void initialize(float hitTime, bool isChord, float approachTime)
        {
            this.hitTime = hitTime;
            this.isChord = isChord;
            velocity = new Vector3(0, 8.5f, 0) / approachTime;
            if (isChord)
                gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 255, 255);
        }

        // Update is called once per frame
        void Update()
        {
            gameObject.transform.localPosition += velocity * Time.deltaTime;
        }
    }
}
