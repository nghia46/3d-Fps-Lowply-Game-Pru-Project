using System.Collections;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
     // Duration to wait before returning the explosion to the pool
    [SerializeField] private float returnDelay = 1f;

    // Method to start the coroutine to return the explosion to the pool
    public void ReturnToPool()
    {
        StartCoroutine(ReturnExplosionToPool());
    }

    // Coroutine to return the explosion to the pool after a delay
    IEnumerator ReturnExplosionToPool()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(returnDelay);

        // Return the explosion to the pool
        ExplosionParticlePool.Instance.ReturnExplosionParticle(gameObject);
    }
}
