using UnityEngine;
using System.Linq;
using Entity; // Import the namespace where the IEnemy interface is declared

public class EnemyCount : MonoBehaviour
{
    public int enemyCount;
    // Update is called once per frame
    void Update()
    {
        // Find all GameObjects with the specified tag
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");

        // Filter GameObjects that have components implementing the IEnemy interface
        IEnemy[] enemies = gameObjectsWithTag
            .Select(go => go.GetComponent<IEnemy>())
            .Where(enemy => enemy != null)
            .ToArray();

        // Count the number of enemies found
        enemyCount = enemies.Length;
    }
}
