using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab of the enemy object to spawn
    public int numberOfEnemies; // The number of enemies to spawn
    public int repeatAmount = 0;
    public Vector3 spawnArea; // The area where enemies can spawn
    public float spawnDelay; // The time delay between spawns

    void Start()
    {
        // Call the SpawnEnemies() function after a delay
        for (int i = 0; i < repeatAmount; i++)
        {
            Invoke("SpawnEnemies", spawnDelay);
        }
        //InvokeRepeating("SpawnEnemies", spawnDelay, spawnDelay);
    }

    void SpawnEnemies()
    {
        // Loop through the number of enemies to spawn
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Generate a random position within the spawn area
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnArea.x, spawnArea.x), 0f, Random.Range(-spawnArea.z, spawnArea.z));

            // Instantiate the enemy prefab at the spawn position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
