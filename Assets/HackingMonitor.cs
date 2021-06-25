using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingMonitor : MonoBehaviour
{
    private GameObject brains;
    private Transform playerPlacementPosition;

    private bool isHacking;
    private bool inHackingZone;
    void Start()
    {
        brains = GameObject.FindGameObjectWithTag("Brains");
        playerPlacementPosition = gameObject.transform.GetChild(0);
        isHacking = false;
        inHackingZone = false;
    }

    void Update()
    {

    }

    public void interactionCheck()
    {
        if (inHackingZone)
        {
            isHacking = !isHacking;
            switch (isHacking)
            {
                case true:
                    Debug.Log("Brains is initiating the hack!");
                    brains.transform.position = playerPlacementPosition.transform.position;
                    brains.transform.LookAt(gameObject.transform);
                    break;

                case false:
                    Debug.Log(name + "Brains stopped hacking!");
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Brains walked into the zone");
        BrainsActions brains = other.GetComponent<BrainsActions>();
        if (brains != null)
        {
            inHackingZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BrainsActions brains = other.GetComponent<BrainsActions>();
        if (brains != null)
        {
            inHackingZone = false;
            isHacking = false;
        }
    }
}
