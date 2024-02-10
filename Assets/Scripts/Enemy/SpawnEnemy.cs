using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy prefab to spawn
    public GameObject enemyPrefab;

    // Number of enemies to spawn
    public int numberOfEnemies = 10;

    // Time between enemy spawns
    public float spawnInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Loop to spawn the specified number of enemies
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Calculate random position within spawn range around (100, 100)
            Vector3 spawnPosition = new Vector3(
                Random.Range(-50,50),
                Random.Range(50,100),
                Random.Range(-50,50)
            );

            // Instantiate enemy prefab at the random position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, this.transform);

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
