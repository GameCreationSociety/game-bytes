using UnityEngine;
using UnityEngine.UI;

namespace PuzzleRun
{
    public class startGame : MonoBehaviour
    {
        public Image image;

        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 0;
        }

        // Update is called once per frame
        void Update()
        {
            image = GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a -= 0.01f;
            image.color = tempColor;

            if (image.color.a <= 0)
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }

        }
    }
}
