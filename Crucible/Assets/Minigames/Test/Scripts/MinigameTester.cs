using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTester : MonoBehaviour
{
    void Update()
    {
        if (MinigameInputHelper.IsButton1Down(1) || MinigameInputHelper.IsButton1Down(2))
        {
            if (MinigameController.Instance.CurrentGamemode != MinigameGamemodeTypes.TWOPLAYERVS)
            {
                MinigameController.Instance.FinishGame(LastMinigameFinish.WON);
            }
            else
            {
                if (MinigameInputHelper.IsButton1Down(1))
                {
                    MinigameController.Instance.FinishGame(LastMinigameFinish.P1WIN);
                }
                else
                {
                    MinigameController.Instance.FinishGame(LastMinigameFinish.P2WIN);
                }
            }
        }
    }
}
