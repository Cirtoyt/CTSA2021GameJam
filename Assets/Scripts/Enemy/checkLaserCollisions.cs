using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkLaserCollisions : MonoBehaviour
{
    private ParticleSystem laserParticleSystem;
    private float laserDamage;
    private GameObject HUD;

    private GameObject player1;
    private GameObject player2;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        laserParticleSystem = GetComponent<ParticleSystem>();
        laserDamage = gameObject.GetComponentInParent<Enemy_SniperAI>().attackDamage;
        HUD = GameObject.Find("PlayerHUDPrefab");

        player1 = GameObject.FindGameObjectWithTag("Brains");
        player2 = GameObject.FindGameObjectWithTag("Brawn");
    }

    private void OnParticleCollision(GameObject other)
    {
        int events = laserParticleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < events; i++)
        {

        }

        if(other.tag == "Brains")
        {

            HUD.GetComponent<PlayerHUDController>().DealDamage(1, laserDamage);
        }

        else if (other.tag == "Brawn")
        {
            Debug.Log("Shot Brawn");
            HUD.GetComponent<PlayerHUDController>().DealDamage(2, laserDamage);
        }
    }
}
