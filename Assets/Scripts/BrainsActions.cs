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
        if (!busy && hudctrlr.CheckPlayer1HeavyAttackReady()) // replace true with if heavy attack guage is charged
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
        // Deal damage to enemy
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
