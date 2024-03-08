using UnityEngine;

public class GunTransformBehaviour : MonoBehaviour
{
    public Transform cameraRotation;
    void Update()
    {
        this.transform.rotation = cameraRotation.rotation;
    }
}