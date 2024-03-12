using TMPro;
using UnityEngine;

public class ShowBullet : MonoBehaviour
{
    private TextMeshProUGUI bulletTxt;
    public GunBehaviour gunBehaviour;

    private void Awake()
    {
        bulletTxt = GetComponent<TextMeshProUGUI>();
        // Find the GunBehaviour script in the scene
        gunBehaviour = FindFirstObjectByType<GunBehaviour>();
    }

    private void Update()
    {
        // Update the bullet count text every frame
        UpdateBulletCount();
    }

    private void UpdateBulletCount()
    {
        if (gunBehaviour != null && gunBehaviour.CurrentBullet > 0)
        {
            // Retrieve the current bullet count from the GunBehaviour script
            bulletTxt.text = $"Magazine: {gunBehaviour.CurrentBullet}/{gunBehaviour.gun.MaxMagazine}";
        }
        else
        {
            bulletTxt.text = "Magazine: <color=yellow> Realoading... </color>";
        }
    }
}

