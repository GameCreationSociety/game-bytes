using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float DestroyTime;

    // Update is called once per frame
    void Update()
    {
        DestroyTime -= Time.deltaTime;
        if(DestroyTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
