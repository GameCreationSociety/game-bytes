using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Default Scenes")]
    public SceneReference MinigameLauncherScene;
    public SceneReference MinigameEndScene;
    public SceneReference StartScene;
    public SceneReference RockPaperNukeScene;

    [Header("Minigames")]
    public MinigameInfo[] AvailableMinigames;

    [Header("Graphics")]
    public float GameEndGraphicsWait;
    public Transform WinGraphicsPrefab;
    public Transform LoseGraphicsPrefab;
    public Transform P1WinGraphicsPrefab;
    public Transform TieGraphicsPrefab;
    public Transform P2WinGraphicsPrefab;
    public Transform StartGameTransition;
    public Transform EndGameTransition;

    public MinigameInfo[] GetShuffledMinigames()
    {
        MinigameInfo[] ShuffledMinigames = new MinigameInfo[AvailableMinigames.Length];
        AvailableMinigames.CopyTo(ShuffledMinigames, 0);
        for (int i = 0; i < ShuffledMinigames.Length; i++)
        {
            int j = Random.Range(0, ShuffledMinigames.Length - 1);
            MinigameInfo jInfo = ShuffledMinigames[j];
            ShuffledMinigames[j] = ShuffledMinigames[i];
            ShuffledMinigames[i] = jInfo;
        }
        return ShuffledMinigames;
    }

    #if UNITY_EDITOR 
    [Button]
    public void RefreshAvailableMinigames()
    {
        string[] Guids = AssetDatabase.FindAssets("t:" + typeof(MinigameInfo).Name);  //FindAssets uses tags check documentation for more info
        MinigameInfo[] AllMinigames = new MinigameInfo[Guids.Length];
        int TotalLength = 0;
        for (int i = 0; i < Guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(Guids[i]);
            MinigameInfo Minigame = AssetDatabase.LoadAssetAtPath<MinigameInfo>(path);
            if (!Minigame.ExcludeFromGameList)
            {
                TotalLength++;
            }
            AllMinigames[i] = Minigame;
        }

        int c = 0;
        AvailableMinigames = new MinigameInfo[TotalLength];
        for (int i = 0; i < Guids.Length; i++)
        {
            if (!AllMinigames[i].ExcludeFromGameList)
            {
                AvailableMinigames[c] = AllMinigames[i];
                c++;
            }
        }

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    #endif
}
