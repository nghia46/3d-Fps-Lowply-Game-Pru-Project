using UnityEngine;

public class GameSettingTrigger : MonoBehaviour
{
    [SerializeField] GameObject SettingPanel;
    private void Awake()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingPanel.SetActive(!SettingPanel.activeSelf);
            SwitchCursor();
        }
        Time.timeScale = SettingPanel.activeSelf ? 0 : 1;

    }
    private void SwitchCursor()
    {
        if (Cursor.visible && Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
