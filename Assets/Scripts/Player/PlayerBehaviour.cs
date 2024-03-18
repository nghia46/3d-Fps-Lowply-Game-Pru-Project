using Entity;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerValue playerValue;
    public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = playerValue.MaxHealth;
    }
    public void Die()
    {
        Debug.Log("Player died!");
        EventManager.Instance.StartGameOverEvent();
    }
    public void TakeDamage(int damage)
    {
        if (CurrentHealth >= damage)
        {
            CurrentHealth -= damage;
            PlayerHealthBar.Instance.UpdateHealth(CurrentHealth, playerValue.MaxHealth);
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
