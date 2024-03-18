using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    // Enemy prefabs to spawn
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    // Number of enemies to spawn
    [SerializeField] private LeverValue value;

    // Time between enemy spawns
    public float spawnInterval = 1f;

    // Range around the central point to spawn enemies
    public float spawnRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Loop to spawn the specified number of enemies
        for (int i = 0; i < value.EnemyQuantity; i++)
        {
            // Calculate random position within spawn range around the spawn points
            Vector3 randomSpawnPosition = GetRandomSpawnPosition();

            // Randomly select an enemy prefab from the array
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Instantiate the randomly selected enemy prefab at the random position
            Instantiate(randomEnemyPrefab, randomSpawnPosition, Quaternion.identity, this.transform);

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Method to calculate a random spawn position within the spawn range around the spawn points
    Vector3 GetRandomSpawnPosition()
    {
        // Get a random spawn point
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Calculate a random position within the spawn range around the spawn point
        Vector3 randomOffset = Random.insideUnitSphere * spawnRange;
        randomOffset.y = 0; // Ensure enemies spawn at the same height
        Vector3 randomSpawnPosition = randomSpawnPoint.position + randomOffset;

        return randomSpawnPosition;
    }
}
