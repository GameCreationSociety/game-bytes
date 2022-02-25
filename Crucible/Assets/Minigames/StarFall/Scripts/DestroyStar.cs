using UnityEngine;

namespace StarFall
{
    public class DestroyStar : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
