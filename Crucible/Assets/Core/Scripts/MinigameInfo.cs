using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[System.Flags]
public enum MinigameGamemodeTypes
{
    ONEPLAYER = (1 << 0),
    TWOPLAYERCOOP = (1 << 1),
    TWOPLAYERVS = (1 << 2)
};

[System.Serializable]
public enum MinigameSemester
{
    FALL,
    SPRING
};

/** Scriptable object storing all the information necessary to add a minigame into the game. 
 *  If you are not the lead for the minigame project please DO NOT MODIFY this file. 
 *  Talk to the lead if you need something here to change.*/
[CreateAssetMenu(fileName = "MinigameInfo", menuName = "Minigame Info")]
public class MinigameInfo : ScriptableObject
{
    [Header("Description")]
    public string Name;
    [TextArea]public string Description;
    public int Year;
    public MinigameSemester Semester; 
    public Sprite Thumbnail;
    public string CreatorNames;
    public string P1_Objective;
    public string P2_Objective;

    [Header("Controls")]
    public string P1_JoystickDescription;
    public string P1_Button1Description;
    public string P1_Button2Description;

    public string P2_JoystickDescription;
    public string P2_Button1Description;
    public string P2_Button2Description;

    [Header("Gameplay")]
    public SceneReference GameScene;
    public MinigameGamemodeTypes SupportedGameModes;

    [Header("Dev Settings")]
    public bool ExcludeFromGameList;
}
