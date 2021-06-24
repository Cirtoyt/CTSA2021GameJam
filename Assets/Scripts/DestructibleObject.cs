using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public GameObject interactionObject;

    public void startDestruction()
    {
        GetComponent<Collider>().enabled = false;
        // Play cool VFX here
        // Interact with item

        Destroy(gameObject, 2);
    }
}
