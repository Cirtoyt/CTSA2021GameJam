using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    private Grapple grappleScript;
    void Start()
    {
        grappleScript = GetComponentInParent<Grapple>();
    }

    private void OnTriggerEnter(Collider other)
    {
        grappleScript.HookHit(other);
    }
}
