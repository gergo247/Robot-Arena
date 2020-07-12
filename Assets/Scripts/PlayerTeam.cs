using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public static PlayerTeam instance;

    List<Entity> Team;
    public float spawnRadius = 10f;
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one playerteam in scene.");
        else
            instance = this;
    }

    public void SpawnTeam()
    {
        foreach (var character in Team)
        {
            Vector2 spawnPos = Vector3.zero;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

            Instantiate(character, spawnPos, Quaternion.identity);
        }
    }

    public void PutIntoTeam(Entity character)
    {
        Team.Add(character);
    }
}
