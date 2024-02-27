using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public void Play(Vector3 position)
    {
        GameObject explosion = ExplosionParticlePool.Instance.GetExplosionParticle();
        if (explosion != null)
        {
            explosion.transform.position = position;

            // Get the ExplosionBehaviour component and start the coroutine to return it to the pool
            ExplosionBehaviour explosionBehaviour = explosion.GetComponent<ExplosionBehaviour>();
            if (explosionBehaviour != null)
            {
                explosionBehaviour.ReturnToPool();
            }
            else
            {
                Debug.LogWarning("Explosion object is missing ExplosionBehaviour component.");
                ExplosionParticlePool.Instance.ReturnExplosionParticle(explosion);
            }
        }
    }
}