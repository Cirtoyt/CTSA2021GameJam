using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrawnActions : MonoBehaviour
{
    public LayerMask enemyLayer;
    public Animator anim;

    public bool busy = false;
    [SerializeField] private float regularAttackDelay = 0.35f;
    [SerializeField] private float heavyAttackDelay = 1.0f;
    [SerializeField] private float soloUltDelay = 2.0f;

    private float LightAtkCentre;
    private Vector3 LightAtkSize;
    private int LightAtkDamage;

    private Rigidbody myRigidBody;
    private bool dashing;
    private float heavyAtkSpeed;
    private float heavyAtkRange;
    private int heavyAtkDamage;

    private int SoloUltRadius;
    private int SoloUltDamage;

    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");

        busy = false;

        LightAtkCentre = 1;
        LightAtkSize = new Vector3(1, 2, 1);
        LightAtkDamage = 5;

        myRigidBody = gameObject.GetComponent<Rigidbody>();
        dashing = false;
        heavyAtkSpeed = 0.75f;
        heavyAtkRange = 9;
        heavyAtkDamage = 0;


        SoloUltRadius = 4;
        SoloUltDamage = 30;
    }

    void Update()
    {
        
    }

    public void OnRegularAttack(InputValue value)
    {
        if (!busy)
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

            busy = true;
            StartCoroutine(StartAttackDelay(regularAttackDelay));
        }
    }

    public void OnHeavyAttack(InputValue value)
    {
        if (!busy && true) // replace true with if heavy attack gauge is charged
        {
            Debug.Log(name + " heavy attacks!");
            busy = true;

            dashing = true;
            StartCoroutine(Dash());

            //Invoke("HeavyAttack", 0.0f);
        }
    }

    private void HeavyAttack()
    {
        // Tell animator to animate attack
        // Wait for attack completion
        // Deal damage
        busy = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(dashing)
        {       
            if (collision.gameObject.layer == 8)
            {
                Debug.Log("Hit : " + collision.gameObject.name);
                collision.gameObject.GetComponent<Base_Enemy>().gotHit(heavyAtkDamage);
                Vector3 knockbackForce = (collision.gameObject.transform.position - transform.position).normalized * 250f;
                collision.gameObject.GetComponent<Base_Enemy>().knockBack(knockbackForce);
            }
        }
    }

    public void OnUltimateAttack(InputValue value)
    {
        if (!busy && true) // replace true with if ultimate guage is charged
        {
            Debug.Log(name + " uses an ultimate!!");
            // Check if other player is nearby

            // Otherwise do solo slam
            Collider[] enemiesHit = Physics.OverlapSphere(transform.position, SoloUltRadius, enemyLayer);

            int i = 0;
            while (i < enemiesHit.Length)
            {
                Debug.Log("Hit : " + enemiesHit[i].gameObject.name + i);
                enemiesHit[i].GetComponent<Base_Enemy>().gotHit(SoloUltDamage);

                i++;
            }

            busy = true;
            StartCoroutine(StartAttackDelay(regularAttackDelay));

            // Reset ultimate gauge charge
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

    private IEnumerator Dash()
    {
        gameObject.GetComponent<PlayerMovement>().canMove = false;
        float startTime = Time.time;

        while(Time.time < startTime + heavyAtkSpeed)
        {
            myRigidBody.MovePosition(transform.position + transform.forward * heavyAtkRange * Time.fixedDeltaTime);

            yield return null;
        }

        gameObject.GetComponent<PlayerMovement>().canMove = true;
        dashing = false;

        StartCoroutine(StartAttackDelay(heavyAttackDelay));
    }
}
