using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Interactables : MonoBehaviour
{
    private LayerMask playerLayerMask;
    private Vector3 objectSize;
    private bool inRange;

    public float interactableRadius;
    //Used to specify interactable type
    public enum interactableType
    {
        PUSH,
        PULL,
        COLLECTABLE,
        RETURN
    }
    public interactableType type;

    void Start()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        objectSize = GetComponent<Collider>().bounds.size;

        SphereCollider interactionTrigger = gameObject.AddComponent<SphereCollider>();
        interactionTrigger.isTrigger = true;
        interactionTrigger.radius += interactableRadius; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == playerLayerMask)
        {
            // Display UI prompt


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == playerLayerMask)
        {
            // Remove UI prompt
        }
    }
}
