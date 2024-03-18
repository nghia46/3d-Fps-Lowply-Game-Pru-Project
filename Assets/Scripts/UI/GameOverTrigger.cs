using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] PlayerValue playerValue;
    [SerializeField] PlayerValue DefaultsPlayerValue;
    [SerializeField] LeverValue LeverValue;
    [SerializeField] LeverValue DefaultsLeverValue;
    [SerializeField] Gun gun;
    [SerializeField] Gun DefaultsGunValue;
    [SerializeField] GameObject gameOverPanel;
    private void Start()
    {
        EventManager.Instance.GameOverEvent += OnGameOver;
        EventManager.Instance.ResetValueEvent += OnResetValue;
    }
    private void OnGameOver()
    {
        OnCursor();
        ActiveGameOverPanel();
        ResetAllValuesToDefault();
    }
    private void OnResetValue()
    {
        ResetAllValuesToDefault();
    }
    private void ActiveGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    private static void OnCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResetAllValuesToDefault()
    {
        playerValue.MaxHealth = DefaultsPlayerValue.MaxHealth;
        gun.Damage = DefaultsGunValue.Damage;
        gun.MaxMagazine = DefaultsGunValue.MaxMagazine;
        LeverValue.Score = 0;
        LeverValue.EnemyQuantity = DefaultsLeverValue.EnemyQuantity;
    }
    private void OnDisable()
    {
        EventManager.Instance.GameOverEvent -= OnGameOver;
    }
}
