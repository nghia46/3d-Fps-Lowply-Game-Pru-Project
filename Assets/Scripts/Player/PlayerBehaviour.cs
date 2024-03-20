using Entity;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    public PlayerValue playerValue;
    public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = playerValue.MaxHealth;
    }
    private void Update()
    {
        PlayerHealthBar.Instance.UpdateHealth(CurrentHealth, playerValue.MaxHealth);

    }
    public void Die()
    {
        EventManager.Instance.StartGameOverEvent();
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

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return playerValue.MaxHealth;
    }
}
