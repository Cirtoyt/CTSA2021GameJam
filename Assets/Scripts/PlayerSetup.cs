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
            tag = "Brains";
            GetComponent<MeshRenderer>().material.color = new Color(0, 0, 255);
            gameObject.AddComponent<BrainsActions>();

            // Setup model & animator
        }
        else if (PlayerInstanceManager.instance.players.Count == 1)
        {
            // Setup Brawn

            name = "Brawn";
            tag = "Brawn";
            GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
            gameObject.AddComponent<BrawnActions>();

            // Setup model & animator
        }

        PlayerInstanceManager.instance.players.Add(gameObject);

        Destroy(this);
    }

    void Update()
    {
        
    }
}
