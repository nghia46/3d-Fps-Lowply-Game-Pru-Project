using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int, int> ScoreEvent; // Event to handle score
    public event Action FireEvent; // Event to handle fire
    public event Action NeedReloadingEvent; // Event to handle need reloading
    public event Action GameOverEvent;// Event to handle game over
    public event Action ReloadEvent; // Event to handle reload

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartScoreEvent(int id, int score)
    {
        Debug.Log("Score");
        ScoreEvent?.Invoke(id, score);
    }
    public void StartFireEvent()
    {
        Debug.Log("Fire");
        FireEvent?.Invoke();
    }
    public void StartReloadEvent()
    {
        Debug.Log("Reload");
        ReloadEvent?.Invoke();
    }
    public void StartNeedReloadEvent()
    {
        Debug.Log("Need Reload");
        NeedReloadingEvent?.Invoke();
    }
    public void StartGameOverEvent()
    {
        Debug.Log("Game Over");
        GameOverEvent?.Invoke();
    }
}