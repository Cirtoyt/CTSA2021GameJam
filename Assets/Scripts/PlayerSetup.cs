using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Material brainsMaterial;
    public Material brawnMaterial;

    void Start()
    {
        if (PlayerInstanceManager.instance.players.Count == 0)
        {
            // Setup Brains

            name = "Brains";
            tag = "Brains";
            GetComponent<MeshRenderer>().material = brainsMaterial;
            gameObject.AddComponent<BrainsActions>();

            // Setup model & animator
        }
        else if (PlayerInstanceManager.instance.players.Count == 1)
        {
            // Setup Brawn

            name = "Brawn";
            tag = "Brawn";
            GetComponent<MeshRenderer>().material = brawnMaterial;
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
