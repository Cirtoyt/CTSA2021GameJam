using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrawnActions : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnRegularAttack(InputValue value)
    {
        Debug.Log(name + " regular attacks!");
    }

    public void OnHeavyAttack(InputValue value)
    {
        Debug.Log(name + " heavy attacks!");
    }

    public void OnUltimateAttack(InputValue value)
    {
        Debug.Log(name + " uses an ultimate!!");
    }

    public void OnInteract(InputValue value)
    {
        Debug.Log(name + " uses an interaction.");
    }
}
