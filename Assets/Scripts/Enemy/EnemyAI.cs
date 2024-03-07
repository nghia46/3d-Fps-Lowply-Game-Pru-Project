using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float stoppingDistance = 10f;
    private ExplosionEffect explosionEffect;
    private Transform player;

    // Variables for color change
    [SerializeField]private Renderer enemyRenderer;
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

        // Change the enemy's color to red temporarily
        StartCoroutine(FlashDamageColor());
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
}
