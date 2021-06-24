using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ChaserAI : MonoBehaviour
{
    public NavMeshAgent agent;

    Vector3 enemyPos;
    private GameObject player1;
    private GameObject player2;
    GameObject HUD;

    public LayerMask groundLayer, playerLayer;

    //Which player is AI targeting (closest player)
    private enum target
    {
        PLAYER1,
        PLAYER2
    }
    private target currentTarget;

    public float attackDelay;
    private bool canAttack;

    public float attackRange;
    private bool playerInAttackRange;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyPos = transform.position;

        player1 = GameObject.FindGameObjectWithTag("Brains");
        player2 = GameObject.FindGameObjectWithTag("Brawn");
        HUD = GameObject.Find("PlayerHUDPrefab");

        canAttack = true;

    }

    private void Update()
    {
        if (player1 == null)
        {
            player1 = GameObject.FindGameObjectWithTag("Brains");
        }
        if (player2 == null)
        {
            player2 = GameObject.FindGameObjectWithTag("Brawn");
        }
        if (HUD == null)
        {
            HUD = GameObject.Find("PlayerHUDPrefab");
        }
        if (player1 != null && player2 != null)
        {
            //Targets player 1 - Distance to player 1 is smaller
            if (Vector3.Distance(enemyPos, player1.transform.position) < Vector3.Distance(enemyPos, player2.transform.position))
            {
                if (HUD.GetComponent<PlayerHUDController>().player1Health > 0)
                {
                    currentTarget = target.PLAYER1;
                }
            }
            //Targets player 2 - Distance to player 2 is smaller
            else if (Vector3.Distance(enemyPos, player2.transform.position) < Vector3.Distance(enemyPos, player1.transform.position))
            {
                if (HUD.GetComponent<PlayerHUDController>().player2Health > 0)
                {
                    currentTarget = target.PLAYER2;
                }
            }
            //Distances between 2 players and enemy are equal
            else
            {
                currentTarget = target.PLAYER1;
            }

            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

            //Function calls for enemy state
            chasePlayer(currentTarget);
            if (playerInAttackRange)
            {
                attackPlayer(currentTarget);
            } 
        }
    }

    private void chasePlayer(target currentTarget)
    {
        if (currentTarget == target.PLAYER1)
        {
            agent.SetDestination(player1.transform.position);
        }
        else if (currentTarget == target.PLAYER2)
        {
            agent.SetDestination(player2.transform.position);
        }
    }
    private void attackPlayer(target currentTarget)
    {
        //Enemy stops moving
        agent.SetDestination(transform.position);

        if (currentTarget == target.PLAYER1)
        {
            transform.LookAt(player1.transform);
        }
        else if (currentTarget == target.PLAYER2)
        {
            transform.LookAt(player2.transform);
        }

        if (canAttack)
        {
            //Attack code
            if (currentTarget == target.PLAYER1)
            {
                Debug.Log("Attacking Player Brains");
                HUD.GetComponent<PlayerHUDController>().DealDamage(1, 2.0f);
            }
            else if (currentTarget == target.PLAYER2)
            {
                Debug.Log("Attacking Player Brawn");
                HUD.GetComponent<PlayerHUDController>().DealDamage(2, 2.0f);
            }
            //
            canAttack = false;
            StartCoroutine(resetAttack(attackDelay));
        }
    }
    private IEnumerator resetAttack(float attackDelay)
    {
        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
    }
}
