using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scri : MonoBehaviour
{
    private GameObject brains;
    private bool isHacking;
    // Start is called before the first frame update
    void Start()
    {
        brains = GameObject.FindGameObjectWithTag("Brains");
        isHacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
