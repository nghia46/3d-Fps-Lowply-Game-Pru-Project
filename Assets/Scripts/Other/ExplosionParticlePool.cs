using UnityEngine;
using System.Collections.Generic;

public class ExplosionParticlePool : MonoBehaviour
{
    public static ExplosionParticlePool Instance { get; private set; }

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int poolSize = 5;

    private Queue<GameObject> explosionPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject explosion = Instantiate(explosionPrefab,this.transform);
            explosion.SetActive(false);
            explosionPool.Enqueue(explosion);
        }
    }

    public GameObject GetExplosionParticle()
    {
        if (explosionPool.Count == 0)
        {
            Debug.LogWarning("Explosion particle pool is empty. Returning null.");
            return null;
        }

        GameObject explosion = explosionPool.Dequeue();
        explosion.SetActive(true);
        return explosion;
    }

    public void ReturnExplosionParticle(GameObject explosion)
    {
        explosion.SetActive(false);
        explosionPool.Enqueue(explosion);
    }
}
