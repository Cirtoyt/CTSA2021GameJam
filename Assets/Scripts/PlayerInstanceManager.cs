using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstanceManager : MonoBehaviour
{
    public static PlayerInstanceManager instance { get; private set; }

    public List<GameObject> players;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        
    }
}
