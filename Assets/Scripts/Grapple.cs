using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float extensionSpeed;
    [SerializeField] private float maxExtensionLength;
    [SerializeField] private float pullSpeed;

    private PlayerHUDController hudctrlr;
    private Transform hook;
    private Transform rope;
    private Transform ropeVisual;
    private Transform brains;
    private BrainsActions brainsActions;
    private Vector3 direction;
    private bool isExtending;
    private bool pullTarget;
    private bool pullBrains;

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
        Vector3 grappleOnBrainsPos = new Vector3(brains.position.x, brains.position.y + 1, brains.position.z);
        transform.position = grappleOnBrainsPos;

        if (isExtending)
        {
            hook.position += direction * extensionSpeed * Time.deltaTime;
            Vector3 ropeDir = (hook.position - transform.position).normalized;
            float ropeLength = Vector3.Distance(transform.position, hook.position);
            rope.position = transform.position + (ropeDir * (ropeLength / 2));
            ropeVisual.localScale = new Vector3(ropeVisual.localScale.x, ropeLength / 2, ropeVisual.localScale.z);

            if (ropeLength > maxExtensionLength)
            {
                CancelGrapple();
            }
        }

        if (pullBrains)
        {

        }

        // Begin to move player transform (ignoring physics) towards grapple hook, stopping just before it (half player width + 10cm or something)
        // , as well as shrink rope cylinder and reposition posortionally between player and hook at all times
        // Once at end, turn on movement again and destroy grapple gameobject

        //CancelGrapple();
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
        }
        else
        {
            Debug.Log("Grapple hit object");
            pullBrains = true;
        }
    }
}
