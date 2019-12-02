using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : UnitySingleton<SceneTransitionController>
{
    private Animator TransitionAnimator;
    private string TransitionTarget = "";
    private AudioSource musicPlayer = null;

    void Start()
    {
        TransitionAnimator = GetComponent<Animator>();
        musicPlayer = GetComponent<AudioSource>();
    }

    public IEnumerator FadeOut(float FadeTime)
    {
        if (musicPlayer)
        {
            float startVolume = musicPlayer.volume;

            while (musicPlayer.volume > 0)
            {
                musicPlayer.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            musicPlayer.Stop();
            musicPlayer.volume = startVolume;
        }
    }
    public void TransitionToScene(string ScenePath)
    {
        StartCoroutine(FadeOut(1.0f));
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
