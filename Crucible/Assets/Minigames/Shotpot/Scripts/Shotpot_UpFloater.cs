using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotpot_UpFloater : MonoBehaviour
{
    [SerializeField] float speed = 0.0f;
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float upRandomMod = 0.0f;

	// Use this for initialization
	void Start ()
    {
	    speed = speed + Random.Range(-upRandomMod,upRandomMod);	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    transform.position = transform.position + new Vector3(0,speed * Time.deltaTime,0);
        speed += acceleration * Time.deltaTime;	
	}
}
