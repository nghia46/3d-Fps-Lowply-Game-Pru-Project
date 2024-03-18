using Entity;
using UnityEngine;
using UnityEngine.UI;

public class ShowHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the slider component
    private IDamageable enemyAI; // Reference to the EnemyAI script attached to the enemy GameObject

    private void Awake()
    {
        enemyAI = GetComponentInParent<IDamageable>(); // Get the EnemyAI component
        healthSlider.maxValue = enemyAI.GetMaxHealth(); // Set the maximum value of the health slider to the enemy's maximum health
    }

    private void LateUpdate()
    {
        healthSlider.value = enemyAI.GetCurrentHealth(); // Update the value of the health slider with the enemy's current health
    }
}