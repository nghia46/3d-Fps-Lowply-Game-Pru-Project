using Camera;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    private void Start()
    {
        EventManager.Instance.GameOverEvent += OnGameOver;
    }
    private void OnGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverPanel.SetActive(true);
    }
    private void OnDisable()
    {
        EventManager.Instance.GameOverEvent -= OnGameOver;
    }
}
