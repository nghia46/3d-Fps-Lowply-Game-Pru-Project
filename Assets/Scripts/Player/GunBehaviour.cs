using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunBehaviour : MonoBehaviour
{
    //[SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputReader input;
    [SerializeField] private Transform cameraOrientation;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float damage;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask hitLayers;
    private bool isShooting;
    [SerializeField] private float muzzleFlashDuration; // Duration for which the muzzle flash remains visible


    private void HandleFire()
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

    private void HandleFireCancel()
    {
        isShooting = false;
    }
    private void Start()
    {
        input.FireEvent += HandleFire;
        input.FireCancelEvent += HandleFireCancel;
    }
    void Update()
    {
        Debug.DrawRay(muzzle.position, muzzle.forward * 100, Color.red);

        if (muzzle.transform.GetChild(0).transform.gameObject.activeSelf)
        {
            StartCoroutine(DeactivateMuzzleFlashDelayed());
        }
        transform.rotation = cameraOrientation.rotation;
    }
    IEnumerator DeactivateMuzzleFlashDelayed()
    {
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzle.transform.GetChild(0).transform.gameObject.SetActive(false);
    }

    void Shoot()
    {
        muzzle.transform.GetChild(0).transform.gameObject.SetActive(true);
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, maxDistance, hitLayers))
        {
            hit.transform.gameObject.GetComponent<EnemyAI>().OnDamage(damage);
        }
    }
}
