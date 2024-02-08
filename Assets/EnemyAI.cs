using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private float stoppingDistance = 5f; // Adjust this value to set the stopping distance

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Rotate to face the player
        transform.LookAt(player);

        // Calculate the distance between enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Move towards the player if the distance is greater than the stopping distance
        if (distanceToPlayer > stoppingDistance)
        {
            float moveSpeed = 7f; // Adjust this value to control the speed of movement
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Stop moving when within stopping distance
            Debug.Log("Enemy stopped in front of the player.");
        }
    }
}
