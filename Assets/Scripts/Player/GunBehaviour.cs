using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    // Serialized fields for easy tweaking in the Unity editor
    [SerializeField] private InputReader input; // Reference to the input reader component
    [SerializeField] private Animator animator; // Reference to the animator component
    [SerializeField] private Transform muzzle; // Reference to the muzzle transform
    [SerializeField] private GameObject bulletHolePrefab; // Prefab for the bullet hole effect
    [Tooltip("Damage dealt by each bullet")]
    [SerializeField] private float damage; // Damage dealt by each bullet
    [Tooltip("Maximum distance a bullet can travel")]
    [SerializeField] private float maxDistance = 100f; // Maximum distance a bullet can travel
    [SerializeField] private float muzzleFlashDuration; // Duration for which the muzzle flash remains visible
    [Tooltip("Number of bullets fired per shot")]
    [SerializeField] private int numberOfBulletFire; // Number of bullets fired per shot
    [Tooltip("Angle by which bullets can deviate from the muzzle direction")]
    [SerializeField] private float spreadAngle; // Angle by which bullets can deviate from the muzzle direction

    // Private variables to track burst fire and shooting cooldown
    private bool isBursting;
    private bool canShoot = true;

    // Awake method is called when the script instance is being loaded
    private void Awake()
    {
        // If the animator reference is not set, use the animator attached to the GameObject
        animator = animator ? animator : GetComponent<Animator>();
    }

    // Start method is called before the first frame update
    private void Start()
    {
        // Subscribe to input events for firing and canceling firing
        input.FireEvent += HandleFire;
        input.FireCancelEvent += HandheldFireCancel;
    }

    // Update method is called once per frame
    private void Update()
    {
        // Check if the muzzle flash is active and deactivate it after the specified duration
        if (muzzle.transform.GetChild(0).gameObject.activeSelf)
        {
            StartCoroutine(DeactivateMuzzleFlashDelayed());
        }
    }

    // Method to handle the fire event
    private void HandleFire()
    {
        // Start burst fire coroutine if not already bursting
        if (!isBursting)
        {
            isBursting = true;
            StartCoroutine(BurstFireCoroutine());
        }
    }

    // Method to handle canceling of fire event
    private void HandheldFireCancel()
    {
        isBursting = false; // Stop burst firing
    }

    // Coroutine for burst firing
    private IEnumerator BurstFireCoroutine()
    {
        // Continuously shoot while bursting
        while (isBursting)
        {
            Shoot(); // Call the shoot method
            yield return new WaitForSeconds(0.1f); // Wait between shots
        }
    }

    // Coroutine to deactivate muzzle flash after a delay
    private IEnumerator DeactivateMuzzleFlashDelayed()
    {
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzle.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Method to initiate shooting
    private void Shoot()
    {
        // If allowed to shoot, start shooting coroutine
        if (canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    // Coroutine to handle shooting logic with delay
    private IEnumerator ShootWithDelay()
    {
        canShoot = false; // Prevent shooting until the delay is over

        animator.SetTrigger("Shoot"); // Trigger shoot animation
        muzzle.GetChild(0).gameObject.SetActive(true); // Activate muzzle flash effect

        // Fire multiple bullets
        for (int i = 0; i < numberOfBulletFire; i++)
        {
            Vector3 direction = CalculateBulletDirection(); // Calculate bullet direction
            RaycastHit hit; // Variable to store hit information

            // Raycast to detect hits
            if (Physics.Raycast(muzzle.position, direction, out hit, maxDistance))
            {
                HandleHit(hit); // Handle the hit
            }
        }

        yield return new WaitForSeconds(0.1f); // Delay between shots

        // Deactivate muzzle flash and allow shooting again
        muzzle.GetChild(0).gameObject.SetActive(false);
        canShoot = true;
    }

    // Method to calculate bullet direction with spread
    private Vector3 CalculateBulletDirection()
    {
        Vector3 direction = muzzle.forward; // Initial direction is forward
        // Apply random spread within the specified angle
        direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), muzzle.right) * direction;
        direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), muzzle.up) * direction;
        return direction; // Return the calculated direction
    }

    // Method to handle hit detection
    private void HandleHit(RaycastHit hit)
    {
        // Check if the hit object is an enemy
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemyAI = hit.transform.GetComponent<EnemyAI>(); // Get the enemy AI component
            if (enemyAI != null)
            {
                enemyAI.OnDamage(damage); // Damage the enemy
            }
        }
        else
        {
            InstantiateBulletHole(hit); // Instantiate bullet hole effect for non-enemy objects
        }
    }

    // Method to instantiate bullet hole effect at hit position
    private void InstantiateBulletHole(RaycastHit hit)
    {
        // Check if bullet hole prefab is assigned
        if (bulletHolePrefab != null)
        {
            Vector3 gapOffset = hit.normal * 0.01f; // Offset to prevent z-fighting
            // Instantiate bullet hole with appropriate rotation and parent it to the GarbagePool
            Instantiate(bulletHolePrefab, hit.point + gapOffset, Quaternion.LookRotation(hit.normal, Vector3.up), GameObject.Find("GarbagePool").transform);
        }
    }
}
