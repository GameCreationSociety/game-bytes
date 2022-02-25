using UnityEngine;

namespace PuzzleRun
{
    public class Player1 : MonoBehaviour
    {
        public int laneNumber = 1;
        public Vector3 movement = new Vector3(1.59f, 0.0f, 0.0f);

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (MinigameInputHelper.IsButton1Down(1) && laneNumber > 0) {
                laneNumber -= 1;
                transform.position -= movement;
            }

            if (MinigameInputHelper.IsButton2Down(1) && laneNumber < 2) {
                laneNumber += 1;
                transform.position += movement;
            }

        }
    }
}
