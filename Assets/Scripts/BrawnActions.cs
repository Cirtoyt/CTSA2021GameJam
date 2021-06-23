using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrawnActions : MonoBehaviour
{
    public LayerMask enemyLayer;

    private float LightAtkCentre;
    private Vector3 LightAtkSize;
    bool m_Started;

    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        LightAtkCentre = 1;
        LightAtkSize = new Vector3(1, 2, 1);

        m_Started = true;
    }

    void Update()
    {
        
    }

    public void OnRegularAttack(InputValue value)
    {
        Debug.Log(name + " regular attacks!");
        Collider[] enemiesHit = Physics.OverlapBox(transform.position + (transform.forward * LightAtkCentre), LightAtkSize, Quaternion.LookRotation(transform.forward), enemyLayer);

        int i = 0;
        while (i < enemiesHit.Length)
        {
            Debug.Log("Hit : " + enemiesHit[i].gameObject.name + i);
            
            i++;
        }
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (m_Started)
            Gizmos.DrawWireCube(transform.position + (transform.forward * LightAtkCentre), LightAtkSize);
    }
}
