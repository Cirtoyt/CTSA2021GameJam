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

    private PlayerHUDController hudctrlr;

    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");

        busy = false;

        LightAtkCentre = 1;
        LightAtkSize = new Vector3(1, 2, 1);
        LightAtkDamage = 10;

        myRigidBody = gameObject.GetComponent<Rigidbody>();
        dashing = false;
        heavyAtkSpeed = 0.75f;
        heavyAtkRange = 9;
        heavyAtkDamage = 9;


        SoloUltRadius = 4;
        SoloUltDamage = 30;

        hudctrlr = FindObjectOfType<PlayerHUDController>();
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

                hudctrlr.UpdatePlayer2HeavyAttackGauge(35);
                hudctrlr.UpdateUltGauge(20);

                i++;
            }

            busy = true;
            StartCoroutine(StartAttackDelay(regularAttackDelay));
        }
    }

    public void OnHeavyAttack(InputValue value)
    {
        //if (!busy && hudctrlr.CheckPlayer2HeavyAttackReady())
        if (!busy)
        {
            Debug.Log(name + " heavy attacks!");
            busy = true;

            dashing = true;
            StartCoroutine(Dash());
            hudctrlr.ResetPlayer2HeavyAttackGauge();
        }
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

            else if(collision.gameObject.layer == 11)
            {
                Debug.Log("Hit : " + collision.gameObject.name);
                collision.gameObject.GetComponent<DestructibleObject>().startDestruction();
            }
        }
    }

    public void OnUltimateAttack(InputValue value)
    {
        if (!busy && hudctrlr.CheckUltimateReady() != PlayerHUDController.UltimateTypes.NotReady) // replace true with if ultimate guage is charged
        {
            if (hudctrlr.CheckUltimateReady() == PlayerHUDController.UltimateTypes.Single)
            {
                Debug.Log(name + " uses their solo ultimate!!");

                busy = true;

                // You should move this into another function that is called by an event in the ult's animation
                // and have here just a call to trigger the animation, all of course once the animation is setup

                Collider[] enemiesHit = Physics.OverlapSphere(transform.position, SoloUltRadius, enemyLayer);

                int i = 0;
                while (i < enemiesHit.Length)
                {
                    Debug.Log("Hit : " + enemiesHit[i].gameObject.name + i);
                    enemiesHit[i].GetComponent<Base_Enemy>().gotHit(SoloUltDamage);

                    i++;
                }

                busy = false;
            }
            else if (hudctrlr.CheckUltimateReady() == PlayerHUDController.UltimateTypes.Combo)
            {
                Debug.Log(name + " triggers the combo ultimate with Brains!!!");

                GameObject brains = GameObject.FindGameObjectWithTag("Brains");
                brains.GetComponent<BrainsActions>().busy = true;
                brains.GetComponent<PlayerMovement>().canMove = false;

                // Do something co-ordinated with Brains :S
            }

            hudctrlr.ResetUltimateGauge();
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

        busy = false;
    }
}
