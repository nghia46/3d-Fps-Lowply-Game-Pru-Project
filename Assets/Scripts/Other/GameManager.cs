using System.Collections;
using System.Linq;
using Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] LeverValue Value;
    public int totalEnemies;
    public int enemiesRemaining;
    private bool checkingWin;

    // Time delay before checking for win condition
    public float winCheckDelay = 5f;

    // Update is called once per frame
    void Update()
    {
        // Find all GameObjects with the Enemy component attached
        GameObject[] gameObjectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");

        // Filter GameObjects that have components implementing the IEnemy interface
        IEnemy[] enemies = gameObjectsWithTag
            .Select(go => go.GetComponent<IEnemy>())
            .Where(enemy => enemy != null)
            .ToArray();

        // Update the total number of enemies and remaining enemies
        totalEnemies = enemies.Length;
        enemiesRemaining = totalEnemies;

        if (enemiesRemaining <= 0 && !checkingWin)
        {
            StartCoroutine(CheckWinAfterDelay());
        }
    }

    IEnumerator CheckWinAfterDelay()
    {
        checkingWin = true;
        yield return new WaitForSeconds(winCheckDelay);

        // After the delay, check if there are still no enemies remaining
        if (enemiesRemaining <= 0)
        {
            Value.EnemyQuantity++;
            SceneManager.LoadScene("Shop");
            // You can perform other actions here, like triggering a win condition.
        }

        checkingWin = false;
    }
}
