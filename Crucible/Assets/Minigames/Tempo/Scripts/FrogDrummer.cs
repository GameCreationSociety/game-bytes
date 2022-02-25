using UnityEngine;

namespace Tempo
{
    public class FrogDrummer : MonoBehaviour
    {
        public Sprite[] sprites;
        public SpriteRenderer render;
        // Start is called before the first frame update

        public void selectSprite(int spriteNum)
        {
            render.sprite = sprites[spriteNum];
        }
        void Start()
        {
            render = gameObject.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
