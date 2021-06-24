using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public Material brainsMaterial;
    public Material brawnMaterial;
    public RuntimeAnimatorController brainsAnimController;
    public RuntimeAnimatorController brawnsAnimController;

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
            gameObject.AddComponent<BrainsActions>();
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
