using UnityEngine;

namespace Bombastic
{
    public class bombSound : MonoBehaviour
    {
        private bool hasPlayed = false;
        public AudioSource explosion;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (MinigameController.Instance.GetElapsedTime() > 59.9 && !hasPlayed)
            {
                hasPlayed = true;
                explosion.Play();
            }
        }
    }
}
