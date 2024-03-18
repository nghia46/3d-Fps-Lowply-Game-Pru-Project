using UnityEngine;
using UnityEngine.SceneManagement;
public class uiEvent : MonoBehaviour
{
    public void PlayAgain()
    {
        EventManager.Instance.StartResetValueEvent();
        SceneManager.LoadScene("Plain");
    }
    public void Exit()
    {
        EventManager.Instance.StartResetValueEvent();
        SceneManager.LoadScene("MainMenu");
    }
}
