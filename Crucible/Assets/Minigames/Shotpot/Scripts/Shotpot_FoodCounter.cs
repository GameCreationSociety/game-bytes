using UnityEngine;

namespace Shotpot
{
    public class Shotpot_FoodCounter : MonoBehaviour
    {
        [SerializeField] public int player;
        private int scoreCount;
        private CapsuleCollider2D capsule;
    

        public int getScore()
        {
            return MinigameController.Instance.GetScore(player);
        }

        void Start()
        {
            capsule = GetComponent<CapsuleCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Shotpot_Food food = collision.transform.GetComponent<Shotpot_Food>();
            if(food && food.score > 0)
            {
                GameObject instance = Instantiate(Resources.Load("FloatingText", typeof(GameObject))) as GameObject;
                instance.transform.position = collision.transform.position;
                TextMesh text = instance.GetComponent<TextMesh>();
                text.anchor = transform.position.x > 0 ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
                text.text = food.foodName + " +" + food.score.ToString();
                MinigameController.Instance.AddScore(player, food.score);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Shotpot_Food food = collision.transform.GetComponent<Shotpot_Food>();
            if (food && food.score > 0)
            {
                GameObject instance = Instantiate(Resources.Load("BadFloatingText", typeof(GameObject))) as GameObject;
                instance.transform.position = collision.transform.position;
                TextMesh text = instance.GetComponent<TextMesh>();
                text.anchor = transform.position.x > 0 ? TextAnchor.UpperRight : TextAnchor.UpperLeft;
                text.text = food.foodName + " -" + food.score.ToString();
                MinigameController.Instance.AddScore(player, -food.score);
            }
        }

        // Update is called once per frame
        void Update()
        {
            /*scoreCount = 0;
        Collider2D[] overlaps = Physics2D.OverlapCapsuleAll((Vector2)transform.position + capsule.offset, (Vector2)transform.lossyScale * capsule.size, capsule.direction, 0);
        foreach(Collider2D overlap in overlaps)
        {
            Shotpot_Food food = overlap.transform.GetComponent<Shotpot_Food>();
            if(food && food.canBeScored())
            {
                scoreCount += food.score;
            }
        }*/
        }
    }
}
