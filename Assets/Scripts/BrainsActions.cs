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

    private PlayerHUDController hudctrlr;
    private Animator anim;
    [SerializeField] private GameObject hackingMonitor;

    private void Start()
    {
        busy = false;
        hudctrlr = FindObjectOfType<PlayerHUDController>();
        anim = GetComponent<Animator>();
        hackingMonitor = GameObject.Find("Control_Panel");
        gun = transform.Find("Brains LaserGun").GetComponent<ParticleSystem>();
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