using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingMonitor : MonoBehaviour
{
    private GameObject brains;
    private Transform playerPlacementPosition;

    private bool isHacking;
    private bool inHackingZone;
    // Start is called before the first frame update
    void Start()
    {
        brains = GameObject.FindGameObjectWithTag("Brains");
        playerPlacementPosition = gameObject.transform.GetChild(0);
        isHacking = false;
        inHackingZone = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interactionCheck()
    {
        if (inHackingZone)
        {
            isHacking = !isHacking;
            if (isHacking)
            {
                Debug.Log("Brains is initiating the hack!");
                brains.transform.position = playerPlacementPosition.transform.position;
                brains.transform.LookAt(gameObject.transform);
            }
            else if (!isHacking)
            {
                Debug.Log(name + "Brains stopped hacking!");
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
