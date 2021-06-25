using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public bool holdsItem;

    private ParticleSystem smokeEffect;
    [SerializeField] private GameObject item;

    private void Start()
    {
        smokeEffect = gameObject.GetComponentInChildren<ParticleSystem>();    
    }

    public void startDestruction()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        smokeEffect.Play();
        
        if(holdsItem)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, smokeEffect.main.duration + smokeEffect.main.startLifetime.constantMax);
    }
}
