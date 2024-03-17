using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBar : MonoBehaviour
{
    public static PlayerHealthBar Instance;
    private void Awake()
    {
        // Ensure there's only one instance of CrosshairBehaviour
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }
    }
    public Image HealthBar;
    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        HealthBar.fillAmount = currentHealth / maxHealth;
    }
}
