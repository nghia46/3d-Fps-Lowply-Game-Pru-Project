using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Gun", order = 0)]
public class Gun : ScriptableObject
{
    [Tooltip("Damage dealt by each bullet")]
    public int Damage = 10;
    [Tooltip("Ther farest the ray can shoot")]
    public float MaxDistance = 100f;
    public float MuzzleFlashDuration;
    [Tooltip("Number of bullets fired per shot")]
    public int NumberOfBulletFire; // Number of bullets fired per shot
    [Tooltip("Angle by which bullets can deviate from the muzzle direction")]
    public float SpreadAngle; // Angle by which bullets can deviate from the muzzle direction
    [Tooltip("Shoot rate while brusting (per/s)")]
    [Range(0,5)]
    public float BrustingRate;
}