using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public List<GameObject> Characters;

    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one shop in scene.");
        else
            instance = this;
    }


    void Start()
    {
       // PlayerTeam.instance.PutIntoTeam(Characters[0]);
    }

}
