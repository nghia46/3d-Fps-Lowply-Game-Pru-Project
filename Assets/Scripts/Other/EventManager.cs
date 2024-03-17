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
        ScoreEvent?.Invoke(id, score);
    }
    public void StartFireEvent()
    {
        FireEvent?.Invoke();
    }
    public void StartReloadEvent()
    {
        ReloadEvent?.Invoke();
    }
    public void StartNeedReloadEvent()
    {
        NeedReloadingEvent?.Invoke();
    }
    public void StartGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }
}