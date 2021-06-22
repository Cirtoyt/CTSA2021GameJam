using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    
    void Start()
    {
        if (PlayerInstanceManager.instance.players.Count == 0)
        {
            // Setup Brains

            name = "Brains";
            GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
             

            // Setup model & animator
        }
        else if (PlayerInstanceManager.instance.players.Count == 1)
        {
            // Setup Brawn

            name = "Brawn";
            GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);


            // Setup model & animator
        }

        PlayerInstanceManager.instance.players.Add(gameObject);

        Destroy(this);
    }

    void Update()
    {
        
    }
}
