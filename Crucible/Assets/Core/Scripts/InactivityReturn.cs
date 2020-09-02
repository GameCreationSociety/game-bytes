using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Returns to main menu if there is no input for a set time
public class InactivityReturn : MonoBehaviour
{
    [SerializeField] GameSettings settings = null;
    [SerializeField] float timeToReturnSeconds = 120.0f;
    private float returnTime = 0.0f;

    private void Start()
    {
        returnTime = timeToReturnSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.anyKey)
        {
            returnTime -= Time.deltaTime;
            if(returnTime < 0.0f)
            {
                SceneTransitionController.Instance.TransitionToScene(settings.StartScene);
            }
        }
        else
        {
            returnTime = timeToReturnSeconds;
        }
    }
}
