using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Scriptable object storing all the information necessary to add a minigame into the game. 
 *  If you are not the lead for the minigame project please DO NOT MODIFY this file. 
 *  Talk to the lead if you need something here to change.*/
[CreateAssetMenu(fileName = "MetaMinigameInfo", menuName = "Meta Minigame Info")]
public class MetaMinigameInfo : ScriptableObject
{
    [Header("Description")]
    public string Name;
    [TextArea] public string Description;
    public int Year;
    public MinigameSemester Semester;
    public Sprite Thumbnail;
    public string CreatorNames;

    [Header("Controls")]
    public string P1_JoystickDescription;
    public string P1_Button1Description;
    public string P1_Button2Description;
    public string P1_Button3Description;
    public string P1_Button4Description;
    public string P1_Button5Description;
    public string P1_Button6Description;

    public string P2_JoystickDescription;
    public string P2_Button1Description;
    public string P2_Button2Description;
    public string P2_Button3Description;
    public string P2_Button4Description;
    public string P2_Button5Description;
    public string P2_Button6Description;


    [Header("Gameplay")]
    public SceneReference GameScene;
    public MinigameGamemodeTypes SupportedGameModes;

    [Header("Dev Settings")]
    public bool ExcludeFromGameList;
}
