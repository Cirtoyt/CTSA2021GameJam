using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GravityBomb : MonoBehaviour
{
    [SerializeField] private float duration = 8;
    [SerializeField] private float throwHeightAngle = 3;
    [SerializeField] private float throwStrength = 5;
    public LayerMask enemyLayer;
    [SerializeField] private float pullForce = 2.2f;

    private float elapsed;
    [SerializeField] private List<Transform> enemiesEffected = new List<Transform>();

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
            foreach (Transform enemy in enemiesEffected)
            {
                enemy.GetComponent<NavMeshAgent>().isStopped = false;
                enemy.GetComponent<Rigidbody>().isKinematic = true;
            }

            Destroy(gameObject);
        }

        // Find enemies within 3 radius and suck them inwords
        foreach (Collider enemy in Physics.OverlapSphere(transform.position, 3, enemyLayer))
        {
            if (!enemiesEffected.Contains(enemy.transform))
            {
                enemiesEffected.Add(enemy.transform);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Transform enemy in enemiesEffected)
        {
            enemy.GetComponent<NavMeshAgent>().isStopped = true;
            enemy.GetComponent<Rigidbody>().isKinematic = false;

            Vector3 pullInDirection = (transform.position - enemy.transform.position).normalized;
            float gravityMultiplier = (2.9f - Vector3.Distance(enemy.transform.position, transform.position))
                                    * (2.9f - Vector3.Distance(enemy.transform.position, transform.position));
            enemy.GetComponent<Rigidbody>().AddForce(pullInDirection * pullForce * gravityMultiplier);
        }
    }
}
