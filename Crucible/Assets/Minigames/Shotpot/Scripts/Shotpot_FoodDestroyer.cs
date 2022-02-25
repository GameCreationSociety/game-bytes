using UnityEngine;

namespace Shotpot
{
    public class Shotpot_FoodDestroyer : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.GetComponent<Shotpot_Food>()||collision.transform.GetComponentInParent<Shotpot_Food>())
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
