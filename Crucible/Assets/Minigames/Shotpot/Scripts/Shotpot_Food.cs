using UnityEngine;

namespace Shotpot
{
    public class Shotpot_Food : MonoBehaviour
    {
        public int score = 1;
        public string foodName = "Garbage";

        public bool canBeScored()
        {
            return true;
        }
    }
}
