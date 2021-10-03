using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public int laneNumber = 1;
    public Vector3 movement = new Vector3(2f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MinigameInputHelper.IsButton1Down(2) && laneNumber > 0) {
            laneNumber -= 1;
            transform.position -= movement;
        }

        if (MinigameInputHelper.IsButton2Down(2) && laneNumber < 2) {
            laneNumber += 1;
            transform.position += movement;
        }

    }
}
