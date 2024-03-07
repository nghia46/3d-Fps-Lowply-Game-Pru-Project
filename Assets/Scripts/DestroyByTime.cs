using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 2f; // Time delay before destroying the game object

    // Start is called before the first frame update
    void Start()
    {
        // Call the DestroyObject method after the specified delay
        Destroy(gameObject, destroyDelay);
    }
}
