using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBomb : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float throwHeightAngle;
    [SerializeField] private float throwStrength;

    private float elapsed;

    void Start()
    {
        elapsed = 0;

        Vector3 rawDirection = GameObject.FindGameObjectWithTag("Brains").GetComponent<PlayerMovement>().direction;
        if (rawDirection == Vector3.zero)
        {
            rawDirection = GameObject.FindGameObjectWithTag("Brains").transform.forward;
        }

        Vector3 throwDirection = new Vector3(rawDirection.x * throwStrength, throwHeightAngle, rawDirection.z * throwStrength);
        GetComponent<Rigidbody>().AddForce(throwDirection, ForceMode.Impulse);
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed >= duration)
        {
            Destroy(gameObject);
        }

        // Find enemies within 3 radius and suck them inwords
    }
}
