using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_ChaserAI : MonoBehaviour
{
    public NavMeshAgent agent;

    Vector3 enemyPos;
    public GameObject player1;
    public GameObject player2;

    public LayerMask groundLayer, playerLayer;

    //Which player is AI targeting (closest player)
    private enum target
    {
        PLAYER1,
        PLAYER2
    }
    private target currentTarget;

    public float attackDelay;
    private bool hasAttacked;

    public float attackRange;
    private bool playerInAttackRange;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyPos = transform.position;
    }

    void Update()
    {
        //Targets player 1 - Distance to player 1 is smaller
        if (Vector3.Distance(enemyPos, player1.transform.position) < Vector3.Distance(enemyPos, player2.transform.position))
        {
            currentTarget = target.PLAYER1;
        }
        //Targets player 2 - Distance to player 2 is smaller
        else if (Vector3.Distance(enemyPos, player2.transform.position) < Vector3.Distance(enemyPos, player1.transform.position))
        {
            currentTarget = target.PLAYER2;
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

        if (!hasAttacked)
        {
            //Attack code
            if (currentTarget == target.PLAYER1)
            {
                Debug.Log("Attacking Player 1");
            }
            else if (currentTarget == target.PLAYER2)
            {
                Debug.Log("Attacking Player 2");
            }
            //
            hasAttacked = true;
            Invoke(nameof(resetAttack), attackDelay);
        }
    }
    private void resetAttack()
    {
        hasAttacked = false;
    }
}
