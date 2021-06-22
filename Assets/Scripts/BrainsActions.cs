using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrainsActions : MonoBehaviour
{
    public bool busy = false;
    [SerializeField] private float regularAttackDelay = 0.35f;
    [SerializeField] private float heavyAttackDelay;

    void Start()
    {
        busy = false;
    }

    void Update()
    {
        
    }

    public void OnRegularAttack(InputValue value)
    {
        if (!busy)
        {
            Debug.Log(name + " regular attacks!");
            busy = true;
            StartCoroutine(StartAttackDelay(regularAttackDelay));
        }
    }

    public void OnHeavyAttack(InputValue value)
    {
        if (!busy && true) // replace true with if heavy attack guage is charged
        {
            Debug.Log(name + " heavy attacks!");
            busy = true;
            Invoke("HeavyAttack", 0.0f);
        }
    }

    private void HeavyAttack()
    {
        // Tell animator to animate attack
        // Wait for attack completion
        // Deal damage
        busy = false;
    }

    public void OnUltimateAttack(InputValue value)
    {
        if (!busy && true) // replace true with if ultimate guage is charged
        {
            Debug.Log(name + " uses an ultimate!!");
            // Check if other player is nearby
        }
    }

    public void OnInteract(InputValue value)
    {
        Debug.Log(name + " uses an interaction.");
    }

    private IEnumerator StartAttackDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        busy = false;
    }
}
