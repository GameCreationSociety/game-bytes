using UnityEngine;

namespace Sporshmallow
{
    public class ShieldScript : MonoBehaviour
    {
        GameObject p1;
        GameObject p2;
        // Start is called before the first frame update
        void Start()
        {
            p1 = GameObject.Find("Player1");
            p2 = GameObject.Find("Player2");
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Physics2D.IgnoreCollision(p1.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(p2.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
