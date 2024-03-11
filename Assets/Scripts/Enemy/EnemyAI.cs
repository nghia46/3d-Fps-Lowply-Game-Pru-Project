using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] public Enemy enemy;
    private ExplosionEffect explosionEffect;
    private Transform player;
    public int CurrentHealth;
    // Variables for color change
    [SerializeField] private Renderer enemyRenderer;
    private void Awake()
    {
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
    }

    public void OnDamage(int damage)
    {
        if (enemy.MaxHealth >= damage)
        {
            CurrentHealth -= damage;
        }
        if (CurrentHealth <= 0)
        {
            // /CurrentHealth = 0; // Ensure health is not negative
            explosionEffect.Play(transform.position);
            Destroy(gameObject); // Destroy the enemy GameObject
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
}
