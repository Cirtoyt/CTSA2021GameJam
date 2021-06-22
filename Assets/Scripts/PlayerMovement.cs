using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4;
    public float turnSpeed = 8.6f;
    public bool canMove = true;

    private Rigidbody rb;

    public Vector3 direction;

    void Start()
    {
        canMove = true; //false at start of game before control is given

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canMove && direction != Vector3.zero)
        {
            Quaternion desiredRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, turnSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(transform.position + direction * walkSpeed * Time.fixedDeltaTime);
        }
    }

    public void OnMovement(InputValue value)
    {
        Vector2 direction2D = value.Get<Vector2>();

        direction = new Vector3(direction2D.x, 0, direction2D.y);
    }
}
