using System.Collections;
using Entity;
using UnityEngine;

public class EnemyAI : MonoBehaviour, ICombatant, IDamageable
{
    [SerializeField] public Enemy enemy;
    [SerializeField] PlayerBehaviour playerBehaviour;
    [SerializeField] private Renderer enemyRenderer;
    private ExplosionEffect explosionEffect;
    private Transform player;
    public int CurrentHealth;
    private float attackCooldown = 2f; // Cooldown period between attacks
    // Variables for color change
    private bool canAttack;
    private void Awake()
    {
        canAttack = true;
        CurrentHealth = enemy.MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explosionEffect = GetComponent<ExplosionEffect>();
    }
    private void Update()
    {
        transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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
    private void Attack()
    {
        // Perform attack logic here
        playerBehaviour = FindAnyObjectByType<PlayerBehaviour>();
        if (playerBehaviour != null)
        {
            print("Attack!");
            // Deal damage to the player
            playerBehaviour.TakeDamage(enemy.Damage);
        }
    }
    private IEnumerator AttackCooldown()
    {
        // Set canAttack flag to false to prevent further attacks during cooldown
        canAttack = false;
        // Wait for attackCooldown duration
        yield return new WaitForSeconds(attackCooldown);
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
        StartCoroutine(FlashDamageColor());
    }
    private void OnDestroy()
    {
        EventManager.Instance.StartScoreEvent(enemy.Id, enemy.Score);
    }
    IEnumerator FlashDamageColor()
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
}
