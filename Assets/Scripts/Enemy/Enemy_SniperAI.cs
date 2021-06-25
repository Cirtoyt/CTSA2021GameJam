﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_SniperAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask groundLayer, playerLayer;
    public float attackDamage;
    public float attackDelay;
    public float turnSpeed;
    private ParticleSystem laserParticleSystem;

    private Vector3 enemyPos;
    private GameObject player1;
    private GameObject player2;
    private GameObject HUD;
    private Animator anim;
    private Quaternion lookRotation;
    private Vector3 direction;

    //Which player is AI targeting (closest player)
    private enum target
    {
        PLAYER1,
        PLAYER2
    }

    private target currentTarget;
    [SerializeField] private bool canAttack;
    private bool playerInAttackRange;
    private int runMode;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyPos = transform.position;
        laserParticleSystem = GetComponentInChildren<ParticleSystem>();

        player1 = GameObject.FindGameObjectWithTag("Brains");
        player2 = GameObject.FindGameObjectWithTag("Brawn");
        HUD = GameObject.Find("PlayerHUDPrefab");
        anim = GetComponent<Animator>();

        canAttack = false;
        runMode = Random.Range(1, 3);
        anim.SetInteger("runMode", runMode);
        StartCoroutine(resetAttack(attackDelay));
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
                else
                {
                    currentTarget = target.PLAYER2;
                }
            }
            //Targets player 2 - Distance to player 2 is smaller
            else if (Vector3.Distance(enemyPos, player2.transform.position) < Vector3.Distance(enemyPos, player1.transform.position))
            {
                if (HUD.GetComponent<PlayerHUDController>().player2Health > 0)
                {
                    currentTarget = target.PLAYER2;
                }
                else
                {
                    currentTarget = target.PLAYER1;
                }
            }
            //Distances between 2 players and enemy are equal
            else
            {
                currentTarget = target.PLAYER1;
            }

            //Function calls for enemy state
            aimAtPlayer(currentTarget);

            if (canAttack)
            {
                shoot(currentTarget);
            }
        }
    }

    private void aimAtPlayer(target currentTarget)
    {
        //anim.SetBool("aiming", true);
        if (currentTarget == target.PLAYER1)
        {
            direction = (player1.transform.position - transform.position).normalized;
        }
        else if (currentTarget == target.PLAYER2)
        {
            direction = (player2.transform.position - transform.position).normalized;
        }

        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void shoot(target currentTarget)
    {
        //Enemy stops moving
        //anim.SetBool("isRunning", false);
        //agent.SetDestination(transform.position);

        //Attack code
        //if (currentTarget == target.PLAYER1)
        //{
            //Debug.Log("Attacking Player Brains");
            //HUD.GetComponent<PlayerHUDController>().DealDamage(1, attackDamage);
        //}
        //else if (currentTarget == target.PLAYER2)
        //{
            //Debug.Log("Attacking Player Brawn");
            //HUD.GetComponent<PlayerHUDController>().DealDamage(2, attackDamage);
        //}
        laserParticleSystem.Play();

        canAttack = false;
        StartCoroutine(resetAttack(attackDelay));
    }

    private IEnumerator resetAttack(float attackDelay)
    {
        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
    }
}
