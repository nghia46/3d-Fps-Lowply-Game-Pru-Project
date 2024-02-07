using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
    [SerializeField] private Transform cameraOrientation;
    void Update()
    {
        transform.rotation = cameraOrientation.rotation;
    }
}
