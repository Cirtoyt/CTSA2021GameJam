using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Interactables : MonoBehaviour
{
    private LayerMask playerLayerMask;
    private Vector3 objectSize;
    private bool inRange;

    public float interactableRadius;

    void Start()
    {
        playerLayerMask = LayerMask.GetMask("Player");
        objectSize = GetComponent<Collider>().bounds.size;

        SphereCollider interactionTrigger = gameObject.AddComponent<SphereCollider>();
        interactionTrigger.isTrigger = true;
        interactionTrigger.radius += interactableRadius; 
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.layer == playerLayerMask)
       {
            // Display UI prompt
       }
    }
}
