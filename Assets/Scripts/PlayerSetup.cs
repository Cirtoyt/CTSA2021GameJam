using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Material brainsMaterial;
    public Material brawnMaterial;
    public RuntimeAnimatorController brainsAnimController;
    public RuntimeAnimatorController brawnsAnimController;
    public GameObject brainsGrapple;
    public GameObject gravityBomb;
    public GameObject brainsLaser;

    private PlayerHUDController hudCtrlr;

    void Start()
    {
        hudCtrlr = FindObjectOfType<PlayerHUDController>();

        if (PlayerInstanceManager.instance.players.Count == 0)
        {
            // Setup Brains

            name = "Brains";
            tag = "Brains";
            hudCtrlr.SetPlayer1(transform);
            transform.Find("characterMedium").GetComponent<SkinnedMeshRenderer>().material = brainsMaterial;
            BrainsActions brainsActions = gameObject.AddComponent<BrainsActions>();
            brainsActions.SetGrapple(brainsGrapple);
            brainsActions.SetGravityBomb(gravityBomb);
            GameObject gun = Instantiate(brainsLaser, transform);
            gun.name = "Brains LaserGun";
            GetComponent<Animator>().runtimeAnimatorController = brainsAnimController as RuntimeAnimatorController;

        }
        else if (PlayerInstanceManager.instance.players.Count == 1)
        {
            // Setup Brawn

            name = "Brawn";
            tag = "Brawn";
            hudCtrlr.SetPlayer2(transform);
            transform.Find("characterMedium").GetComponent<SkinnedMeshRenderer>().material = brawnMaterial;
            gameObject.AddComponent<BrawnActions>();
            GetComponent<Animator>().runtimeAnimatorController = brawnsAnimController as RuntimeAnimatorController;
        }

        PlayerInstanceManager.instance.players.Add(gameObject);

        Destroy(this);
    }

    void Update()
    {
        
    }
}
