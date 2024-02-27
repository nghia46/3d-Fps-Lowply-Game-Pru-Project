using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float stoppingDistance = 10f;
    private ExplosionEffect explosionEffect;
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explosionEffect = GetComponent<ExplosionEffect>();
    }

    private void Update()
    {
        transform.LookAt(player);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector3.forward);
        }
    }

    public void OnDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else
        {
            health = 0; // Ensure health is not negative
            explosionEffect.Play(transform.position);
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }
}
