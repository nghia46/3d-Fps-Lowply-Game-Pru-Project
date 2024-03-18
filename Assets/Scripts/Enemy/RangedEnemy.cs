using System.Collections;
using Entity;
using UnityEngine;

public class RangedEnemy : MonoBehaviour, IDamageable, IEnemy
{
    [SerializeField] public Enemy enemy;
    [SerializeField] private Renderer enemyRenderer;
    public float CurrentHealth;
    private ExplosionEffect explosionEffect;
    private GameObject player;
    private bool canAttack;
     
    private void Awake()
    {
        canAttack = true;
        CurrentHealth = enemy.MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        explosionEffect = GetComponent<ExplosionEffect>();
    }
    private void Update()
    {
        transform.LookAt(player.transform);

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > enemy.StoppingDistance)
        {
            transform.Translate(enemy.Speed * Time.deltaTime * Vector3.forward);
        }
        else
        {
            // Player is within attack range, check if can attack
            if (canAttack)
            {
                // Perform the attack
                Attack();
                // Set attack cooldown
                StartCoroutine(AttackCooldown());
            }
        }
    }
    private IEnumerator AttackCooldown()
    {
        // Set canAttack flag to false to prevent further attacks during cooldown
        canAttack = false;
        // Wait for attackCooldown duration
        yield return new WaitForSeconds(enemy.AttackCooldown);
        // Set canAttack flag to true after cooldown period
        canAttack = true;
    }
    public void TakeDamage(int damage)
    {
        if (enemy.MaxHealth >= damage)
        {
            CurrentHealth -= damage;
        }
        if (CurrentHealth <= 0)
        {
            Die();
        }
        // Change the enemy's color to red temporarily
        StartCoroutine(FlashDamageColorWhenDamage());
    }
    private void OnDestroy()
    {
        EventManager.Instance.StartScoreEvent(enemy.Id, enemy.Score);
    }
    IEnumerator FlashDamageColorWhenDamage()
    {
        // Change the color to red
        enemyRenderer.material.color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.05f);

        // Reset the color back to the original color
        enemyRenderer.material.color = Color.white;
    }
    public void DealDamage(IDamageable target, int damage)
    {
        target.TakeDamage(damage);
    }
    public void Die()
    {
        // /CurrentHealth = 0; // Ensure health is not negative
        explosionEffect.Play(transform.position);
        Destroy(gameObject); // Destroy the enemy GameObject
    }
    public void Attack()
    {
        // Perform attack logic here
        if (player.TryGetComponent<IDamageable>(out var playerBehaviour))
        {
            print("Attack!");
            // Deal damage to the player
            playerBehaviour.TakeDamage(enemy.Damage);
        }
    }

    public float GetCurrentHealth()
    {
        return CurrentHealth;
    }

    public float GetMaxHealth()
    {
        return enemy.MaxHealth;
    }
}
