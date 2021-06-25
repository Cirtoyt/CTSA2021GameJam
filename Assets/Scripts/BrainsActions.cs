using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrainsActions : MonoBehaviour
{
    public bool busy = false;
    private GameObject grapple;
    private GameObject gravityBomb;
    private ParticleSystem gun;
    [SerializeField] private float regularAttackDelay = 0.35f;
    [SerializeField] private float regularAttackDamage = 20;
    [SerializeField] private float heavyAttackDamage = 50;
    [SerializeField] private ParticleSystem laserParticleSystem;

    private PlayerHUDController hudctrlr;
    private Animator anim;
    [SerializeField] private GameObject hackingMonitor;

    private void Start()
    {
        busy = false;
        hudctrlr = FindObjectOfType<PlayerHUDController>();
        anim = GetComponent<Animator>();
        hackingMonitor = GameObject.Find("Control_Panel");
        Instantiate(laserParticleSystem, new Vector3(0, 0 , 0), Quaternion.identity, transform.parent);
        gun = GetComponentInChildren<ParticleSystem>();
    }

    public void OnRegularAttack(InputValue value)
    {
        if (!busy)
        {
            Debug.Log(name + " regular attacks!");
            // Note: Move damage dealing to enemy & ability gauge charging to animation event on moment of impact
            gun.Play();

            busy = true;
            StartCoroutine(StartAttackDelay(regularAttackDelay));
            //hudctrlr.UpdatePlayer1HeavyAttackGauge(15);
            //hudctrlr.UpdateUltGauge(5);
        }
    }

    public void OnHeavyAttack(InputValue value)
    {
        if (!busy && hudctrlr.CheckPlayer1HeavyAttackReady())
        {
            Debug.Log(name + " heavy attacks!");
            busy = true;
            anim.SetBool("isRunning", false);
            GetComponent<PlayerMovement>().canMove = false;
            Vector3 grappleSpawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Instantiate(grapple, grappleSpawnPos, Quaternion.identity);
        }
    }

    public void OnUltimateAttack(InputValue value)
    {
        if (!busy && hudctrlr.CheckUltimateReady() != PlayerHUDController.UltimateTypes.NotReady)
        {
            if (hudctrlr.CheckUltimateReady() == PlayerHUDController.UltimateTypes.Single)
            {
                Debug.Log(name + " uses their solo ultimate!!");

                // Tell anim to play ult throw
                Vector3 gravityBombSpawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                Instantiate(gravityBomb, gravityBombSpawnPos, Quaternion.LookRotation(GetComponent<PlayerMovement>().direction));
            }
            else if (hudctrlr.CheckUltimateReady() == PlayerHUDController.UltimateTypes.Combo)
            {
                Debug.Log(name + " triggers the combo ultimate with Brawn!!!");

                GameObject brawn = GameObject.FindGameObjectWithTag("Brawn");
                brawn.GetComponent<BrawnActions>().busy = true;
                brawn.GetComponent<PlayerMovement>().canMove = false;

                // Do something co-ordinated with Brawn :S
            }
            hudctrlr.ResetUltimateGauge();
        }
    }

    // Write ult impact event
    // Spawns 'gravity bomb' game object
    // Gravity Bomb begins playing sprite system on start
    // Gravity Bomb in update constantly draws enemies in-range aroudn bomb towards itself
    // Also counts up lifespan and destroys itself at time limit, spawning a on-death particle system that plays a splat then kills itself I guess

    public void OnInteract(InputValue value)
    {
        Debug.Log(name + " uses an interaction.");
        hackingMonitor.GetComponentInChildren<HackingMonitor>().interactionCheck();
    }

    public void SetGrapple(GameObject _grapple)
    {
        grapple = _grapple;
    }

    public void SetGravityBomb(GameObject _gravityBomb)
    {
        gravityBomb = _gravityBomb;
    }

    private IEnumerator StartAttackDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        busy = false;
    }
}