using UnityEngine;

public class ByUpgradeManager : MonoBehaviour
{
    public PlayerValue player;
    public Gun gun;
    public void Start()
    {
        EventManager.Instance.ByUpgradeEvent += OnByUpgradeEvent;
    }
    private void OnByUpgradeEvent(int upgradeId)
    {
        switch (upgradeId)
        {
            case 0:
                Debug.Log("Upgrade MaxHealth was triggered");
                player.MaxHealth += 10;
                break;
            case 1:
                Debug.Log("Upgrade 2 was triggered");
                gun.Damage += 5;
                break;
            case 2:
                Debug.Log("Upgrade 3 was triggered");
                gun.MaxMagazine += 5;
                break;
            default:
                Debug.Log("Upgrade ID not found");
                break;
        }
        Debug.Log("ByUpgradeEvent was triggered: " + upgradeId);
    }
}
