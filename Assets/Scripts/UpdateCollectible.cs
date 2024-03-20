using TMPro;
using UnityEngine;
using Weapon;

public class UpdateCollectible : MonoBehaviour
{
    [SerializeField] private LeverValue value;
    private PlayerBehaviour player;
    private GunBehaviour gun;
    private TextMeshProUGUI _txtCoin;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerBehaviour>();
        gun = FindAnyObjectByType<GunBehaviour>();
        _txtCoin = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {

        EventManager.Instance.CollectibleEvent += UpdateCoinToUI;
        _txtCoin.text = $"Coins:<color=Yellow>{value.CoinQuantity}</color>";

    }
    private void Update()
    {
        _txtCoin.text = $"Coins:<color=Yellow>{value.CoinQuantity}</color>";

    }
    private void UpdateCoinToUI(int id, int quantity)
    {
        switch (id)
        {
            case 0:
                value.CoinQuantity += quantity;
                break;
            case 1:
                player.CurrentHealth = player.playerValue.MaxHealth;
                break;
            case 2:
                gun.CurrentBullet = gun.gun.MaxMagazine;
                break;
        }
        _txtCoin.text = $"Coins:<color=Yellow>{value.CoinQuantity}</color>";
    }
}
