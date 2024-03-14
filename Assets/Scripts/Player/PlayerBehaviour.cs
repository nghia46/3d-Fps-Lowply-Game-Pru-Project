using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerValue playerValue;
    public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = playerValue.MaxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (CurrentHealth >= damage)
        {
            CurrentHealth -= damage;
        }
        else
        {
            CurrentHealth = 0; // Ensure health is not negative
            Die();
        }
    }

    private void Die()
    {
        // Implement death logic here
        Debug.Log("Player died!");
        // For example, you can show a game over screen, restart the level, or perform other actions
        // You might want to reset the player's position, reset their health, etc.
    }
}