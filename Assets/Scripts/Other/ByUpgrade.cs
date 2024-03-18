using System.Collections;
using Entity;
using Unity.VisualScripting;
using UnityEngine;

public class ByUpgrade : MonoBehaviour, IDamageable
{
    [SerializeField] private Renderer Renderer;
    [SerializeField] private int UpdateId;
    public float CurrentHealth;
    private ExplosionEffect explosionEffect;
    private GameObject player;

    private void Awake()
    {
        CurrentHealth = 30;
        player = GameObject.FindGameObjectWithTag("Player");
        explosionEffect = GetComponent<ExplosionEffect>();
    }
    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        transform.LookAt(player.transform);
    }

    public void TakeDamage(int damage)
    {

        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        // Change the enemy's color to red temporarily
        StartCoroutine(FlashDamageColorWhenDamage());
    }
    IEnumerator FlashDamageColorWhenDamage()
    {
        // Change the color to red
        Renderer.material.color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.05f);

        // Reset the color back to the original color
        Renderer.material.color = Color.white;
    }
    public void DealDamage(IDamageable target, int damage)
    {
        target.TakeDamage(damage);
    }
    public void Die()
    {
        // /CurrentHealth = 0; // Ensure health is not negative
        EventManager.Instance.StartByUpgradeEvent(UpdateId);
        explosionEffect.Play(transform.position);
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return 30;
    }
    public void OnDestroy()
    {
    }
}
