using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject model;
    public GameObject ps;
    public bool dying = false; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cronch()
    {
        Destroy(model);
        ps.SetActive(true);
        Invoke("SelfDestruct", 2.0f);
        dying = true; 
    }

    void SelfDestruct()
    {
        Destroy(gameObject); 
    }
}
