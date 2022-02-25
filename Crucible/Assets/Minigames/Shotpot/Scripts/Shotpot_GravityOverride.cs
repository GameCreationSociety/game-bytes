using UnityEngine;

namespace Shotpot
{
    public class Shotpot_GravityOverride : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Physics2D.gravity = new Vector2(0,-19.62f);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
