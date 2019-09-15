using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : UnitySingleton<SceneTransitionController>
{
    private Animator TransitionAnimator;
    private string TransitionTarget = "";

    void Start()
    {
        TransitionAnimator = GetComponent<Animator>();
    }

    public void TransitionToScene(string ScenePath)
    {
        if (!IsTransitioning())
        {
            if (TransitionAnimator)
            {
                TransitionTarget = ScenePath;
                TransitionAnimator.Play("TransitionOut");
            }
            else
            {
                SceneManager.LoadScene(ScenePath);
            }
        }
    }

    public void TransitionFinish()
    {
        if (IsTransitioning())
        {
            SceneManager.LoadScene(TransitionTarget);
        }
    }

    public bool IsTransitioning()
    {
        return !TransitionTarget.Equals("");
    }
}
