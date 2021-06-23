using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrawnActions : MonoBehaviour
{
    public LayerMask enemyLayer;

    private Vector3 LightAtkCentre;


    void Start()
    {
        LightAtkCentre = (0, 0, 1);
    }

    void Update()
    {
        
    }

    public void OnRegularAttack(InputValue value)
    {
        Debug.Log(name + " regular attacks!");
        Collider[] enemiesHit = Physics.OverlapBox(gameObject.transform.position + (LightAtkCentre), transform.localScale / 2, Quaternion.identity, enemyLayer);
        Debug.Log(enemiesHit);
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
