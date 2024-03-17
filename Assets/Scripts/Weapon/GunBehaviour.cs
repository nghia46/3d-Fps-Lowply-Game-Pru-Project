using System;
using System.Collections;
using UnityEngine;

namespace Weapon
{
    public class GunBehaviour : MonoBehaviour
    {
        public Gun gun; // Reference to the gun scriptable object
        [SerializeField] private InputReader input; // Reference to the input reader component
        [SerializeField] private Transform muzzle; // Reference to the muzzle transform
        [SerializeField] private Transform bulletDirection; // Reference to the bullet direction transform
        [SerializeField] private GameObject bulletHolePrefab; // Prefab for the bullet hole effect
        Animator animator; // Reference to the animator component
        public int CurrentBullet; // Current bullet count
        bool isShooting; // Flag to check if the player is shooting
        bool isReloading; // Flag to check if the player is reloading
        bool isAiming; // Flag to check if the player is aiming
        bool canShoot = true; // Flag to prevent shooting during cooldown
        // Awake method is called when the script instance is being loaded
        private void Awake()
        {
            // If the animator reference is not set, use the animator attached to the GameObject
            animator = animator ? animator : GetComponent<Animator>();
        }
        // Start method is called before the first frame update
        private void Start()
        {
            CurrentBullet = gun.MaxMagazine; // Set the current bullet count to the maximum magazine size
            input.FireEvent += HandleFire; // Subscribe to the fire event
            input.FireCancelEvent += HandheldFireCancel; // Subscribe to the fire cancel event
            input.GunAimEvent += HandleAim; // Subscribe to the aim event
            input.GunReloadEvent += HandleReload; // Subscribe to the reload event
        }
        // Update method is called once per frame
        private void Update()
        {
            OnOffCrosshair(); // Call the crosshair on/off method
            GunAnimation(); // Call the gun animation method
            DebugFuntion(); // Call the debug function
            DeactivateMuzzleFlash(); // Deactivate muzzle flash after a delay
        }
        private void DebugFuntion()
        {
            // Draw a ray from the muzzle for visualization
            Debug.DrawRay(bulletDirection.position, bulletDirection.forward * gun.MaxDistance, Color.red);
        }
        // Method to handle gun animation
        private void GunAnimation()
        {
            // Update the animator parameter for aiming
            animator.SetBool("isAiming", isAiming);
        }
        // Method to deactivate muzzle flash after a delay
        private void DeactivateMuzzleFlash()
        {
            // Check if the muzzle flash is active and deactivate it after the specified duration
            if (muzzle.transform.GetChild(0).gameObject.activeSelf)
            {
                StartCoroutine(DeactivateMuzzleFlashDelayed());
            }
        }
        // Method to handle the crosshair on/off
        private void OnOffCrosshair()
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
        // Method to handle the aim event
        private void HandleAim()
        {
            isAiming = !isAiming;
        }
        // Coroutine for burst firing
        private void HandleFire()
        {
            // Check if the object is destroyed before starting the coroutine
            if (!this || !gameObject)
            {
                return;
            }

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
        public void HandleReload()
        {
            Reload();
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
        // Method to initiate shooting
        private void Shoot()
        {
            // If allowed to shoot, start shooting coroutine
            if (canShoot && CurrentBullet > 0)
            {
                StartCoroutine(ShootWithDelay());
            }
        }
        // Method to handle the fire logic
        private void Fire()
        {
            for (int i = 0; i < gun.NumberOfBulletFire; i++) // Fire multiple bullets
            {
                Vector3 direction = CalculateBulletDirection(); // Calculate bullet direction
                // Raycast to detect hits
                if (Physics.Raycast(bulletDirection.position, direction, out RaycastHit hit, gun.MaxDistance))
                {
                    HandleHit(hit); // Handle the hit
                }
            }
        }
        // Method to activate/deactivate muzzle flash
        private void ActiveMuzze(bool isActive)
        {
            muzzle.GetChild(0).gameObject.SetActive(isActive);
        }
        // Method to handle aiming animation
        private void AimingAnimation()
        {
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
        }
        // Method to calculate bullet direction with spread
        private Vector3 CalculateBulletDirection()
        {
            Vector3 direction = bulletDirection.forward; // Initial direction is forward
            // Apply random spread within the specified angle
            direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-gun.SpreadAngle, gun.SpreadAngle), bulletDirection.right) * direction;
            direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-gun.SpreadAngle, gun.SpreadAngle), bulletDirection.up) * direction;
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
            // Check if bullet hole prefab is assigned and if the hit object is still active
            if (bulletHolePrefab == null || !hit.transform.gameObject.activeInHierarchy) return;
            Vector3 gapOffset = hit.normal * 0.01f; // Offset to prevent z-fighting
            // Instantiate bullet hole with appropriate rotation and parent it to the GarbagePool
            GameObject bulletHole = Instantiate(bulletHolePrefab, hit.point + gapOffset, Quaternion.LookRotation(hit.normal, Vector3.up));
            bulletHole.transform.parent = GameObject.Find("GarbagePool").transform;
        }
        // Coroutine to handle shooting logic with delay
        private IEnumerator ShootWithDelay()
        {
            CurrentBullet--; // Reduce the bullet count
            canShoot = false; // Prevent shooting until the delay is over
            EventManager.Instance.StartFireEvent();
            AimingAnimation(); // Call the aiming animation method
            ActiveMuzze(true); // Activate muzzle flash
            Fire(); // Call the fire method
            yield return new WaitForSeconds(0.1f); // Delay between shots
            ActiveMuzze(false); // Deactivate muzzle flash
            canShoot = true;
        }
        // Coroutine for the reloading process
        private IEnumerator ReloadCoroutine()
        {
            EventManager.Instance.StartReloadEvent();
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
        // OnDestroy method is called when the object is destroyed
        private void OnDestroy()
        {
            // Stop all coroutines when the object is destroyed
            StopAllCoroutines();
        }
    }
}