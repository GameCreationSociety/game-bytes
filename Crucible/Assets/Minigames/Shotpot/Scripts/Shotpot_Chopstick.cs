using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotpot_Chopstick : MonoBehaviour
{
    [SerializeField] float rotSpeed;
    [SerializeField] float rotLimit;
    [SerializeField] float startRotLimit;
    float startRot;
    private Rigidbody2D rbody;
    private Shotpot_Hand hand;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        hand = GetComponentInParent<Shotpot_Hand>();
        startRot = rbody.rotation;
    }

    // Update is called once per frame
    void Update()
    {
       
        if( MinigameInputHelper.IsButton1Held(hand.player) && !hand.isBurned())
        {
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, rotLimit, rotSpeed*Time.deltaTime));
        }   
        else
        {
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, startRot + startRotLimit, rotSpeed*Time.deltaTime));
        }
    }
}
