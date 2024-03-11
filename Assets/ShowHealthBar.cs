using UnityEngine;
using UnityEngine.UI;

public class ShowHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the slider component
    public GameObject enemyInstance; // Reference to the enemy GameObject

    private EnemyAI enemyAI; // Reference to the EnemyAI script attached to the enemy GameObject

    private void Awake()
    {
        enemyAI = enemyInstance.GetComponent<EnemyAI>(); // Get the EnemyAI component
        healthSlider.maxValue = enemyAI.enemy.MaxHealth; // Set the maximum value of the health slider to the enemy's maximum health
    }

    private void LateUpdate()
    {
        healthSlider.value = enemyAI.CurrentHealth; // Update the value of the health slider with the enemy's current health
    }
}
