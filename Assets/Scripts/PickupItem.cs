using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private GameObject objectiveManagerObject;
    private bool canPickup;

    void Start()
    {
        objectiveManagerObject = GameObject.Find("Objective Manager");
        canPickup = false;

        StartCoroutine(enablePickup());

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canPickup && collision.gameObject.layer == 10)
        {
            objectiveManagerObject.GetComponent<ObjectiveManager>().incrementItems();
            Destroy(gameObject);
        }
    }

    private IEnumerator enablePickup()
    {
        yield return new WaitForSeconds(1);
        canPickup = true;
    }
}
