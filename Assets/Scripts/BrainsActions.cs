using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BrainsActions : MonoBehaviour
{
    public bool busy = false;
    public Animator anim;
    [SerializeField] private float regularAttackDelay = 0.35f;
    [SerializeField] private float regularAttackDamage = 20;
    [SerializeField] private float heavyAttackDamage = 50;

    private PlayerHUDController hudctrlr;

    private void Start()
    {
        busy = false;
        hudctrlr = FindObjectOfType<PlayerHUDController>();
    }

    public void OnRegularAttack(InputValue value)
    {
        if (!busy)
        {
            Debug.Log(name + " regular attacks!");
            busy = true;
            StartCoroutine(StartAttackDelay(regularAttackDelay));
            // Note: Move damage dealing to enemy & ability gauge charging to animation event on moment of impact
            hudctrlr.UpdatePlayer1HeavyAttackGauge(15);
            hudctrlr.UpdateUltGauge(5);
        }
    }

    public void OnHeavyAttack(InputValue value)
    {
        if (!busy && hudctrlr.CheckPlayer1HeavyAttackReady())
        {
            Debug.Log(name + " heavy attacks!");
            busy = true;
            Invoke("HeavyAttack", 0.0f);
        }
    }

    private void HeavyAttack()
    {
        GetComponent<PlayerMovement>().canMove = false;
        // Spawn grapple gameobject
        // In Start, rotate grapple forward = in direction of movement raw desired direction
        // In update, move grapple hook (sphere) in that direction & check collider (slightly smaller than sphere visual) if it's hitting a collider
        // , as well as scale rope cylinder and reposition between player position and grapple hook position
        // If collider hits, change update via bool
        // Begin to move player transform (ignoring physics) towards grapple hook, stopping just before it (half player width + 10cm or something)
        // , as well as shrink rope cylinder and reposition posortionally between player and hook at all times
        // Once at end, turn on movement again and destroy grapple gameobject

        hudctrlr.ResetPlayer1HeavyAttackGauge();
        busy = false;
    }

    public void OnUltimateAttack(InputValue value)
    {
        if (!busy && hudctrlr.CheckUltimateReady() != PlayerHUDController.UltimateTypes.NotReady)
        {
            if (hudctrlr.CheckUltimateReady() == PlayerHUDController.UltimateTypes.Single)
            {
                Debug.Log(name + " uses their solo ultimate!!");

                // Tell anim to play ult throw
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
    }

    private IEnumerator StartAttackDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        busy = false;
    }
}
