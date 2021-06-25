using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkLaserCollisions : MonoBehaviour
{
    public GameObject hitEffect;
    private ParticleSystem laserParticleSystem;
    private float laserDamage;
    private GameObject HUD;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        laserParticleSystem = GetComponent<ParticleSystem>();
        laserDamage = gameObject.GetComponentInParent<Enemy_SniperAI>().attackDamage;
        HUD = GameObject.Find("PlayerHUDPrefab");
    }

    private void OnParticleCollision(GameObject other)
    {
        int events = laserParticleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < events; i++)
        {
            Instantiate(hitEffect, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].normal));
        }

        if(other.tag == "Brains")
        {

            HUD.GetComponent<PlayerHUDController>().DealDamage(1, laserDamage);
        }

        else if (other.tag == "Brawn")
        {
            HUD.GetComponent<PlayerHUDController>().DealDamage(2, laserDamage);
        }
    }
}
