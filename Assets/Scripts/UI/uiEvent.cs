using UnityEngine;
using UnityEngine.SceneManagement;
public class uiEvent : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
