using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotpot_HandMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxRot;
    [HideInInspector] public Rigidbody2D rbody;
    private Shotpot_Hand hand;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        hand = GetComponent<Shotpot_Hand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.isBurned())
        {
            rbody.MovePosition(rbody.transform.position + new Vector3(0, hand.getBurnRatio()) * Time.deltaTime * moveSpeed);
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, 2*maxRot * Random.Range(-1.0f,1.0f), 0.4f));
        }
        else
        {
            float HorizontalAxis = MinigameInputHelper.GetHorizontalValue(hand.player);
            float VerticalAxis = MinigameInputHelper.GetVerticalValue(hand.player);
            rbody.MovePosition(rbody.transform.position + new Vector3(HorizontalAxis, VerticalAxis) * Time.deltaTime * moveSpeed);
            rbody.MoveRotation(Mathf.Lerp(rbody.rotation, maxRot * -HorizontalAxis, 0.1f));
        }
    }
}
