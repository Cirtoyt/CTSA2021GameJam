using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputs : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    public Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction3D = new Vector3(direction.x, 0, direction.y);

        rb.MovePosition(transform.position + direction3D * speed * Time.fixedDeltaTime);
    }

    public void OnMovement(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
}
