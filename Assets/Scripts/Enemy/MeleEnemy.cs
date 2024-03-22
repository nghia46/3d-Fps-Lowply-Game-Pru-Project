using System.Collections;
using Entity;
using UnityEngine;
using UnityEngine.AI;

public class MeleEnemy : MonoBehaviour, IDamageable, IEnemy
{
    [SerializeField] public Enemy enemy;
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Animator animator;
    private NavMeshAgent navMeshAgent;
    public float CurrentHealth;
    private ExplosionEffect explosionEffect;
    private GameObject player;
    private bool canAttack;

    private void Awake()
    {
        canAttack = true;
        CurrentHealth = enemy.MaxHealth;
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        explosionEffect = GetComponent<ExplosionEffect>();
    }
    private void Start()
    {
        navMeshAgent.stoppingDistance = enemy.StoppingDistance;
        navMeshAgent.speed = enemy.Speed;
    }
    private void Update()
    {
        //LookAtPlayer();
        AttackWithRange();
    }

    private void AttackWithRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > enemy.StoppingDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);
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

        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
        // Change the enemy's color to red temporarily
        StartCoroutine(FlashDamageColorWhenDamage());
    }
    private void OnDestroy()
    {
        EventManager.Instance.StartEnemyDieEvent();
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
        SpawnRandomCollectible();
        // /CurrentHealth = 0; // Ensure health is not negative
        explosionEffect.Play(transform.position);
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    private void SpawnRandomCollectible()
    {
        int collectible = Random.Range(0, 3);
        switch (collectible)
        {
            case 0:
                Instantiate(enemy.HealthCollectible, this.transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(enemy.CoinCollectible, this.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(enemy.AmmoCollectible, this.transform.position, Quaternion.identity);
                break;
        }
    }

    public void Attack()
    {
        // Perform attack logic here
        if (player.TryGetComponent<IDamageable>(out var playerBehaviour))
        {
            // Deal damage to the player
            animator.SetTrigger("Attack");
            playerBehaviour.TakeDamage(enemy.Damage);
            EventManager.Instance.StartEnemyAttackEvent();
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
