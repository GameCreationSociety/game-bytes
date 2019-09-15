using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotpot_Hand : MonoBehaviour
{
    [SerializeField] Color burnColor;
    [SerializeField] float burnTime;
    [SerializeField] float centerX;
    [SerializeField] float maxX;
    [SerializeField] float minMass;
    [SerializeField] float maxMass;

    private float currentBurnTime;
    private SpriteRenderer[] sprites;
    private Color[] baseColor;
    private Shotpot_HandMovement mov;
    private SpriteRenderer spr;
    private Rigidbody2D rbody;
    public int player;

    public bool isBurned()
    {
        return currentBurnTime > 0;
    }

    public float getBurnRatio()
    {
        return Mathf.Clamp01(currentBurnTime / burnTime);
    }


    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();    
        mov = GetComponent<Shotpot_HandMovement>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        baseColor = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            baseColor[i] = sprites[i].color;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Shotpot_HotpotLiquid>())
        {
            currentBurnTime = burnTime;
            GetComponent<AudioSource>().Play();
        }
    }

    private void Update()
    {
        rbody.mass = (Mathf.Clamp01((transform.position.x - centerX) /maxX) * maxMass) + minMass;
        if (currentBurnTime > 0)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = Color.Lerp(baseColor[i], burnColor, currentBurnTime / burnTime);
            }
            currentBurnTime -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = baseColor[i];
            }
        }
    }
}
