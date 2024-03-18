using TMPro;
using UnityEngine;
using Weapon;

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
            bulletTxt.text = $"<color=red>{gunBehaviour.CurrentBullet}</color>/{gunBehaviour.gun.MaxMagazine}";
            //EventManager.Instance.StartNeedReloadEvent();
        }
        else
        {
            bulletTxt.text = "<color=yellow>Realoading... </color>";
        }
    }
}