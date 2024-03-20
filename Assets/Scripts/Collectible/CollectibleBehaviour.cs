using Unity.VisualScripting;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    [SerializeField] private int Id;
    [SerializeField] private int Quantity;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Add score
            EventManager.Instance.StartCollectibleEvent(Id,Quantity);
            // Destroy the collectible
            Destroy(gameObject);
            Debug.LogWarning("Collectible");
        }
    }
}
