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
    private bool targetPlayer1;
    private bool targetPlayer2;

    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    public float attackDelay;
    private bool hasAttacked;

    public float attackRange, player1Distance, player2Distance;
    public bool playerInAttackRange;
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
            targetPlayer1 = true;
            targetPlayer2 = false;
        }
        //Targets player 2 - Distance to player 2 is smaller
        else if (Vector3.Distance(enemyPos, player2.transform.position) < Vector3.Distance(enemyPos, player1.transform.position))
        {
            targetPlayer1 = false;
            targetPlayer2 = true;
        }
        //Distances between 2 players and enemy are equal
        else
        {
            targetPlayer1 = true;
            targetPlayer2 = false;
        }

        chasePlayer(targetPlayer1, targetPlayer2);
    }

    private void chasePlayer(bool player1Targeted, bool player2Targeted)
    {
        if (player1Targeted)
        {
            agent.SetDestination(player1.transform.position);
        }
        else if (player2Targeted)
        {
            agent.SetDestination(player2.transform.position);
        }
    }
    private void attackPlayer(bool player1Targeted, bool player2Targeted)
    {
        agent.SetDestination(transform.position);

        if (player1Targeted)
        {
            transform.LookAt(player1.transform);
        }
        else if (player2Targeted)
        {
            transform.LookAt(player2.transform);
        }
    }
}
