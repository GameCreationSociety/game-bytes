using UnityEngine;

namespace PuzzleRun
{
    public class Player2 : MonoBehaviour
    {
        public int laneNumber = 4;
        public Vector3 movement = new Vector3(1.59f, 0.0f, 0.0f);

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (MinigameInputHelper.IsButton1Down(2) && laneNumber > 3) {
                laneNumber -= 1;
                transform.position -= movement;
            }

            if (MinigameInputHelper.IsButton2Down(2) && laneNumber < 5) {
                laneNumber += 1;
                transform.position += movement;
            }

        }
    }
}
