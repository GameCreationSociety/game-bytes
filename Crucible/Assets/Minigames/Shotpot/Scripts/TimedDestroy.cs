using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
    public float timeLeft = 1;
	
	// Update is called once per frame
	void Update ()
    {
		timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Destroy(gameObject);
        }
	}
}
