using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brainsLaserCollisions : MonoBehaviour
{
    public GameObject hitEffect;
    private ParticleSystem laserParticleSystem;
    private float laserDamage;
    private PlayerHUDController hudctrlr;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Start()
    {
        laserParticleSystem = GetComponent<ParticleSystem>();
        laserDamage = gameObject.GetComponentInParent<Enemy_SniperAI>().attackDamage;
        hudctrlr = FindObjectOfType<PlayerHUDController>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int events = laserParticleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < events; i++)
        {
            Instantiate(hitEffect, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].normal));
        }

        if (other.layer == 8)
        {
            other.GetComponent<Base_Enemy>().gotHit(20);
            hudctrlr.UpdatePlayer1HeavyAttackGauge(35);
            hudctrlr.UpdateUltGauge(20);
        }
    }
}
