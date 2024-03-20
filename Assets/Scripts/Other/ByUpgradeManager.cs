using UnityEngine;

public class ByUpgradeManager : MonoBehaviour
{
    public PlayerValue player;
    public Gun gun;
    public LeverValue lever;
    public void Start()
    {
        EventManager.Instance.ByUpgradeEvent += OnByUpgradeEvent;
    }
    private void OnByUpgradeEvent(int upgradeId)
    {
        switch (upgradeId)
        {
            case 0:
                if (TakeCoin(5))
                {
                    Debug.Log("Upgrade MaxHealth was triggered");
                    player.MaxHealth += 10;
                }
                break;
            case 1:
                if (TakeCoin(10))
                {
                    Debug.Log("Upgrade Damage was triggered");
                    gun.Damage += 5;
                }

                break;
            case 2:
                if (TakeCoin(7))
                {
                    Debug.Log("Upgrade MaxMagazine was triggered");
                    gun.MaxMagazine += 5;
                }
                break;
            default:
                Debug.Log("Upgrade ID not found");
                break;
        }
    }
    private bool TakeCoin(int quantity)
    {
        if (lever.CoinQuantity >= quantity)
        {
            lever.CoinQuantity -= quantity;
            return true;
        }
        return false;
    }
}
