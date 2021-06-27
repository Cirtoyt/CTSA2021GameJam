using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float extensionSpeed = 35;
    [SerializeField] private float maxExtensionLength = 5.5f;
    [SerializeField] private float pullSpeed = 15;
    [SerializeField] private float wallStoppingDistance = 1.6f;
    [SerializeField] private float enemyStoppingDistance = 1.2f;

    private PlayerHUDController hudctrlr;
    private Transform hook;
    private Transform rope;
    private Transform ropeVisual;
    private Transform brains;
    private BrainsActions brainsActions;
    private Vector3 direction;
    private bool isExtending;
    private bool pullTarget;
    private Transform grabbedEnemy;
    private bool pullBrains;
    private Vector3 grappleOnBrainsPos;

    void Start()
    {
        hudctrlr = FindObjectOfType<PlayerHUDController>();
        hook = transform.Find("Hook");
        rope = transform.Find("Rope");
        ropeVisual = rope.Find("Rope Visual");
        brains = GameObject.FindGameObjectWithTag("Brains").transform;
        brainsActions = brains.GetComponent<BrainsActions>();
        direction = brains.GetComponent<PlayerMovement>().direction;
        isExtending = true;
        pullTarget = false;
        pullBrains = false;

        // Rotate grapple forward = in direction of movement raw desired direction, or forward direction if no direction input
        if (direction == Vector3.zero)
        {
            direction = brains.forward;
        }

        rope.rotation = Quaternion.LookRotation(direction);
        hudctrlr.ResetPlayer1HeavyAttackGauge();
    }

    // Update is called once per frame
    void Update()
    {
        grappleOnBrainsPos = new Vector3(brains.position.x, brains.position.y + 1, brains.position.z);

        Vector3 ropeDir = (hook.position - transform.position).normalized;
        float ropeLength = Vector3.Distance(transform.position, hook.position);

        if (isExtending)
        {
            transform.position = grappleOnBrainsPos;
            hook.position += direction * extensionSpeed * Time.deltaTime;

            if (ropeLength > maxExtensionLength)
            {
                CancelGrapple();
            }
        }

        if (pullBrains)
        {
            Vector3 newPos = brains.transform.position + (ropeDir * pullSpeed * Time.deltaTime);

            if (Vector3.Distance(brains.transform.position, hook.position) <= wallStoppingDistance)
            {
                brains.transform.position = newPos;
                CancelGrapple();
            }
            else
            {
                brains.transform.position = newPos;
            }
        }
        else if (pullTarget)
        {
            Vector3 newEnemyPos = grabbedEnemy.position + (-ropeDir * pullSpeed * Time.deltaTime);

            if (Vector3.Distance(brains.transform.position, grabbedEnemy.position) <= enemyStoppingDistance)
            {
                grabbedEnemy.position = newEnemyPos;
                hook.position = new Vector3(newEnemyPos.x, newEnemyPos.y + 1, newEnemyPos.z);
                CancelGrapple();
            }
            else
            {
                grabbedEnemy.position = newEnemyPos;
                hook.position = new Vector3(newEnemyPos.x, newEnemyPos.y + 1, newEnemyPos.z);
            }
        }

        UpdateRope(ropeDir, ropeLength);
    }

    private void UpdateRope(Vector3 ropeDir, float ropeLength)
    {
        
        rope.position = grappleOnBrainsPos + (ropeDir * (ropeLength / 2));
        ropeVisual.localScale = new Vector3(ropeVisual.localScale.x, ropeLength / 2, ropeVisual.localScale.z);
    }

    private void CancelGrapple()
    {
        isExtending = false;
        brainsActions.busy = false;
        brains.GetComponent<PlayerMovement>().canMove = true;
        Destroy(gameObject);
    }

    public void HookHit(Collider other)
    {
        isExtending = false;

        if (other.GetComponent<Base_Enemy>())
        {
            Debug.Log("Grapple hit enemy");
            pullTarget = true;
            grabbedEnemy = other.transform;
        }
        else
        {
            Debug.Log("Grapple hit object");
            pullBrains = true;
        }
    }
}
