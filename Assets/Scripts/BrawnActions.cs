using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrawnActions : MonoBehaviour
{
    public LayerMask enemyLayer;

    private float LightAtkCentre;
    private Vector3 LightAtkSize;
    private int LightAtkDamage;

    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");

        LightAtkCentre = 1;
        LightAtkSize = new Vector3(1, 2, 1);
        LightAtkDamage = 5;
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
            enemiesHit[i].GetComponent<Base_Enemy>().gotHit(LightAtkDamage);

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
}
