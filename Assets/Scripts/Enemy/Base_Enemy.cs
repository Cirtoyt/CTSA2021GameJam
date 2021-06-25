using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Base_Enemy : MonoBehaviour
{
    public int health;
    public bool knockedDown;

    public void gotHit(int damage_taken)
    {
        health -= damage_taken;
        if (health <= 0)
        {
            destroyEnemy();
        }
    }
    private void destroyEnemy()
    {
        Destroy(gameObject);
    }

    public void knockBack(Vector3 knockbackForce)
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.angularDrag = 10.0f;

        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        rb.AddForce(knockbackForce);
        StartCoroutine(standUp(1.0f));
    }

    private IEnumerator standUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        Destroy(GetComponent<Rigidbody>());
    }
}
