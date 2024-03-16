using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int, int> ScoreEvent;
    public event Action FireEvent;
    public event Action ReloadingEvent;
    public event Action GameOverEvent;

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
        ReloadingEvent?.Invoke();
    }
    public void StartGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }
}