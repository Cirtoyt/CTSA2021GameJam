using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private GameObject objectiveManagerObject;

    void Start()
    {
        objectiveManagerObject = GameObject.Find("Objective Manager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            objectiveManagerObject.GetComponent<ObjectiveManager>().incrementItems();
            Destroy(gameObject);
        }
    }
}
