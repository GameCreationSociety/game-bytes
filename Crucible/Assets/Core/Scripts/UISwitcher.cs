using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public struct SwitcherInfo
{
    // What will be activated when switched to
    [SerializeField] public CanvasGroup Child;

    // What will be selected when switched to
    [SerializeField] public Button SelectOnSwitch;
}

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private int StartIndex = 0;
    private int CurrentIndex = -1;

    // All of the objects that we want to switch between. Index in array corresponds to switch index.
    [SerializeField] SwitcherInfo[] SwitcherChildren = null;

    private void Start()
    {
        Switch(StartIndex);
    }


    private void Update()
    {
        if(Input.anyKeyDown && EventSystem.current.currentSelectedGameObject == null && CurrentIndex >= 0)
        {
            SwitcherChildren[CurrentIndex].SelectOnSwitch.Select();
        }
    }

    public void Switch(int index)
    {
        // Deactivate all children of this transform
        foreach (SwitcherInfo switcherObj in SwitcherChildren)
        {
            switcherObj.Child.alpha = 0;
        }

        // Activate the child at specified index if index is not out of bounds
        if (index >= 0 && index < SwitcherChildren.Length)
        {
            SwitcherChildren[index].Child.alpha = 1;
            EventSystem.current.SetSelectedGameObject(SwitcherChildren[index].SelectOnSwitch.gameObject);
            CurrentIndex = index;
        }
        else
        {
            Debug.Log("Error: Incorrect child index in switcher was passed");
        }
    }
}
