using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private float damage;
    [SerializeField] private float maxDistance = 100f;
    private bool isShooting;
    private bool isBrusting;
    private bool isAiming;
    private bool canShoot = true; // Variable to track if the gun can shoot

    [SerializeField] private float muzzleFlashDuration; // Duration for which the muzzle flash remains visible
    [Tooltip("Số lượng đạn bắn ra trong 1 lần")]
    [SerializeField] private int numberOfBulletFire;
    [Tooltip("Độ Lệch tâm bắn")]
    [SerializeField] private float spreadAngle;

    private void HandleSingleFire()
    {
        if (!isShooting)
        {
            isShooting = true;
            while (isShooting)
            {
                Shoot();
                isShooting = false;
            }
        }
    }

    private void HandleSingleFireCancel()
    {
        isShooting = false;
    }
    private void HandheldBrustFire()
    {
        if (!isBrusting)
        {
            isBrusting = true;
            StartCoroutine(BurstFireCoroutine());
        }
    }
    private void HandheldBurstFireCancel()
    {
        isBrusting = false;
    }
    private void HandheldAim()
    {
        throw new NotImplementedException();

    }
    private void HandheldAimCancel()
    {
        throw new NotImplementedException();
    }
    private void Awake()
    {
        animator = animator ? animator : GetComponent<Animator>();
    }

    private void Start()
    {
        //Single-fire
        input.SingleFireEvent += HandleSingleFire;
        input.SingleFireCancelEvent += HandleSingleFireCancel;
        //Brust-fire
        input.BurstFireEvent += HandheldBrustFire;
        input.BurstFireCancelEvent += HandheldBurstFireCancel;
        //Aim
        input.GunAimEvent += HandheldAim;
        input.GunAimCancelEvent += HandheldAimCancel;
    }
    void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.forward * 100, Color.red);

        if (muzzle.transform.GetChild(0).transform.gameObject.activeSelf)
        {
            StartCoroutine(DeactivateMuzzleFlashDelayed());
        }
    }
    private IEnumerator BurstFireCoroutine()
    {
        while (isBrusting)
        {
            Shoot();
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator DeactivateMuzzleFlashDelayed()
    {
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzle.transform.GetChild(0).transform.gameObject.SetActive(false);
    }

    void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(ShootWithDelay());
        }
    }

    private IEnumerator ShootWithDelay()
    {
        canShoot = false; // Prevent shooting until the delay is over

        animator.SetTrigger("Shoot");
        muzzle.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < numberOfBulletFire; i++) // Number of bullets fired (adjust as needed)
        {
            Vector3 direction = muzzle.forward;

            // Apply random offset within a cone angle
            direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), muzzle.right) * direction;
            direction = Quaternion.AngleAxis(Random.Range(-spreadAngle, spreadAngle), muzzle.up) * direction;

            if (Physics.Raycast(muzzle.position, direction, out RaycastHit hit, maxDistance))
            {
                // Handle hit logic as before...
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    var enemyAI = hit.transform.GetComponent<EnemyAI>();
                    if (enemyAI != null)
                    {
                        enemyAI.OnDamage(damage);
                    }
                }
                else
                {
                    //Instantiate bullet hole prefab at the hit point
                    if (bulletHolePrefab != null)
                    {
                        Vector3 gapOffset = hit.normal * 0.01f; // Adjust the gap distance as needed
                        Instantiate(bulletHolePrefab, hit.point + gapOffset, Quaternion.LookRotation(hit.normal, Vector3.up), GameObject.Find("GarbagePool").transform);
                    }
                }
            }
        }

        yield return new WaitForSeconds(0.1f); // Adjust the delay time as needed

        muzzle.GetChild(0).gameObject.SetActive(false);
        canShoot = true; // Allow shooting again
    }
}