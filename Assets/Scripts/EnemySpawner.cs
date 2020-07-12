using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRadius = 10f;

    [HideInInspector]
    public Wave currentWave;

    private float nextSpawnTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnWave();
            nextSpawnTime = Time.time + 1f / currentWave.spawnRate;
        }
    }

    void SpawnWave()
    {
        foreach (GameObject eType in currentWave.enemies)
        {
            //if (Random.value <= eType.spawnChance)
            //{
                SpawnEnemy(eType);
           // }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector2 spawnPos = Vector3.zero;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

}
