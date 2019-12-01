using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameInputHelper : MonoBehaviour
{
    static float AxisDeadCutoff = 0.1f;

    static public float GetHorizontalAxis(int PlayerNumber)
    {
        float AxisVal = (PlayerNumber == 1 ? Input.GetAxis("P1_Horizontal") : Input.GetAxis("P2_Horizontal"));
        return (Mathf.Abs(AxisVal) > AxisDeadCutoff ? AxisVal : 0.0f); 
    }

    static public float GetVerticalAxis(int PlayerNumber)
    {
        float AxisVal = (PlayerNumber == 1 ? Input.GetAxis("P1_Vertical") : Input.GetAxis("P2_Vertical"));
        return (Mathf.Abs(AxisVal) > AxisDeadCutoff ? AxisVal : 0.0f); 
    }

    static public bool IsHorizontalAxisInUse(int PlayerNumber)
    {
        float AxisVal = (PlayerNumber == 1 ? Input.GetAxis("P1_Horizontal") : Input.GetAxis("P2_Horizontal"));
        return (Mathf.Abs(AxisVal) > AxisDeadCutoff);
    }

    static public bool IsVerticalAxisInUse(int PlayerNumber)
    {
        float AxisVal = (PlayerNumber == 1 ? Input.GetAxis("P1_Vertical") : Input.GetAxis("P2_Vertical"));
        return (Mathf.Abs(AxisVal) > AxisDeadCutoff);
    }

    static public bool IsButton1Down(int PlayerNumber)
    {
        return PlayerNumber == 1 ? Input.GetButtonDown("P1_Button1") : Input.GetButtonDown("P2_Button1");
    }

    static public bool IsButton1Held(int PlayerNumber)
    {
        return PlayerNumber == 1 ? Input.GetButton("P1_Button1") : Input.GetButton("P2_Button1");
    }

    static public bool IsButton1Up(int PlayerNumber)
    {
        return PlayerNumber == 1 ? Input.GetButtonUp("P1_Button1") : Input.GetButtonUp("P2_Button1");
    }

    static public bool IsButton2Down(int PlayerNumber)
    {
        return PlayerNumber == 1 ? Input.GetButtonDown("P1_Button2") : Input.GetButtonDown("P2_Button2");
    }

    static public bool IsButton2Held(int PlayerNumber)
    {
        return PlayerNumber == 1 ? Input.GetButton("P1_Button2") : Input.GetButton("P2_Button2");
    }

    static public bool IsButton2Up(int PlayerNumber)
    {
        return PlayerNumber == 1 ? Input.GetButtonUp("P1_Button2") : Input.GetButtonUp("P2_Button2");
    }
}
