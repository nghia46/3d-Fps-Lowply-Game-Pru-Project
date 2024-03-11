using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int, int> ScoreEvent;
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
}