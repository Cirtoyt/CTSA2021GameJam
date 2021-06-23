using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enemy : MonoBehaviour
{
    public int health;

    private void gotHit(int damage_taken)
    {
        health -= damage_taken;
        if (health <= 0)
        {
            destroyEnemy();
        }
    }
    private void destroyEnemy()
    {
        Destroy(gameObject);
    }
}
