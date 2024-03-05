using UnityEngine;

public class GunTest : MonoBehaviour
{
    public Transform cameraRotaion;
    void Update()
    {
        this.transform.rotation = cameraRotaion.rotation;
    }
}