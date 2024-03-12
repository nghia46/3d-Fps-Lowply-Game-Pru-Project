using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    // Serialized fields for easy tweaking in the Unity editor
    public Gun gun;
    [SerializeField] private InputReader input; // Reference to the input reader component
    [SerializeField] private Animator animator; // Reference to the animator component
    [SerializeField] private Transform muzzle; // Reference to the muzzle transform
    [SerializeField] private GameObject bulletHolePrefab; // Prefab for the bullet hole effect
    public int CurrentBullet;
    // Private variables to track burst fire and shooting cooldown
    bool isShooting;
    bool isReloading;
    bool isAiming;
    bool canShoot = true;

    // Awake method is called when the script instance is being loaded
    private void Awake()
    {
        // If the animator reference is not set, use the animator attached to the GameObject
        animator = animator ? animator : GetComponent<Animator>();
    }

    // Start method is called before the first frame update
    private void Start()
    {
        CurrentBullet = gun.MaxMagazine;
        // Subscribe to input events for firing and canceling firing
        input.FireEvent += HandleFire;
        input.FireCancelEvent += HandheldFireCancel;
        input.GunAimEvent += HandleAim;
    }

    // Update method is called once per frame
    private void Update()
    {
        // Update the animator parameter for aiming
        animator.SetBool("isAiming", isAiming);

        // Draw a ray from the muzzle for visualization
        Debug.DrawRay(muzzle.position, muzzle.forward * gun.MaxDistance, Color.red);

        // Check if the muzzle flash is active and deactivate it after the specified duration
        if (muzzle.transform.GetChild(0).gameObject.activeSelf)
        {
            StartCoroutine(DeactivateMuzzleFlashDelayed());
        }
    }
    private void LateUpdate()
    {
        if (!isAiming)
        {
            CrosshairBehaviour.Instance.TurnOnCrosshair();
        }
        else
        {
            CrosshairBehaviour.Instance.TurnOffCrosshair();
        }
    }
    private void HandleAim()
    {
        isAiming = !isAiming;
    }
    // Method to handle the fire event
    private void HandleFire()
    {
        // Start burst fire coroutine if not already bursting
        if (!isShooting)
        {
            isShooting = true;
            StartCoroutine(BurstFireCoroutine());
        }
    }

    // Method to handle canceling of fire event
    private void HandheldFireCancel()
    {
        isShooting = false; // Stop burst firing
    }
    // Method to handle reload action
    private void Reload()
    {
        if (!isReloading && CurrentBullet < gun.MaxMagazine)
        {
            isReloading = true;
            StartCoroutine(ReloadCoroutine());
        }
    }
    // Coroutine for the reloading process
    private IEnumerator ReloadCoroutine()
    {
        // Play reload animation or perform any visual feedback
        //animator.SetTrigger("Reload");

        // Wait for the reload animation duration
        yield return new WaitForSeconds(gun.ReloadDuration);

        // Refill the magazine
        CurrentBullet = gun.MaxMagazine;

        // Reset reloading flag
        isReloading = false;
    }
    // Coroutine for burst firing
    private IEnumerator BurstFireCoroutine()
    {
        // Continuously shoot while bursting
        while (isShooting)
        {
            Shoot(); // Call the shoot method
            yield return new WaitForSeconds(gun.BrustingRate); // Wait between shots
        }
    }

    // Coroutine to deactivate muzzle flash after a delay
    private IEnumerator DeactivateMuzzleFlashDelayed()
    {
        yield return new WaitForSeconds(gun.MuzzleFlashDuration);
        muzzle.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Method to initiate shooting
    private void Shoot()
    {
        // If allowed to shoot, start shooting coroutine
        if (canShoot && CurrentBullet > 0)
        {
            StartCoroutine(ShootWithDelay());
        }
        else if (CurrentBullet >= 0)
        {
            Reload();
        }
    }

    // Coroutine to handle shooting logic with delay
    private IEnumerator ShootWithDelay()
    {
        CurrentBullet--;
        canShoot = false; // Prevent shooting until the delay is over
        if (!isAiming)
        {
            CrosshairBehaviour.Instance.TurnOnCrosshair();
            animator.SetTrigger("Shoot"); // Trigger shoot animation
        }
        else
        {
            CrosshairBehaviour.Instance.TurnOffCrosshair();
            animator.SetTrigger("AimShoot");
        }
        muzzle.GetChild(0).gameObject.SetActive(true); // Activate muzzle flash effect

        // Fire multiple bullets
        for (int i = 0; i < gun.NumberOfBulletFire; i++)
        {
            Vector3 direction = CalculateBulletDirection(); // Calculate bullet direction

            // Raycast to detect hits
            if (Physics.Raycast(muzzle.position, direction, out RaycastHit hit, gun.MaxDistance))
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
        direction = Quaternion.AngleAxis(Random.Range(-gun.SpreadAngle, gun.SpreadAngle), muzzle.right) * direction;
        direction = Quaternion.AngleAxis(Random.Range(-gun.SpreadAngle, gun.SpreadAngle), muzzle.up) * direction;
        return direction; // Return the calculated direction
    }

    // Method to handle hit detection
    private void HandleHit(RaycastHit hit)
    {
        // Check if the hit object is an enemy
        if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Enemy")) InstantiateBulletHole(hit);
        var enemyAI = hit.transform.GetComponent<EnemyAI>(); // Get the enemy AI component
        if (enemyAI != null) enemyAI.OnDamage(gun.Damage); // Damage the enemy
    }

    // Method to instantiate bullet hole effect at hit position
    private void InstantiateBulletHole(RaycastHit hit)
    {
        // Check if bullet hole prefab is assigned
        if (bulletHolePrefab == null) return;
        Vector3 gapOffset = hit.normal * 0.01f; // Offset to prevent z-fighting
        // Instantiate bullet hole with appropriate rotation and parent it to the GarbagePool
        Instantiate(bulletHolePrefab, hit.point + gapOffset, Quaternion.LookRotation(hit.normal, Vector3.up), GameObject.Find("GarbagePool").transform);

    }
}
