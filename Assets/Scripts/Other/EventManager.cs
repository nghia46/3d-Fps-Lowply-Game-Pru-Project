using UnityEngine;
using System;
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action<int, int> ScoreEvent; // Event to handle score
    public event Action FireEvent; // Event to handle fire
    public event Action NeedReloadingEvent; // Event to handle need reloading
    public event Action GameOverEvent;// Event to handle game over
    public event Action ResetValueEvent; // Event to
    public event Action EnemyAttackEvent; 
    public event Action EnemyDieEvent; // Event to
    public event Action ReloadEvent; // Event to handle reload
    public event Action<int,int> CollectibleEvent; // Event to handle by upgrade
    public event Action<int> ByUpgradeEvent; // Event to handle by upgrade

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
    public void StartEnemyDieEvent()
    {
        EnemyDieEvent?.Invoke();
    }
    public void StartEnemyAttackEvent()
    {
        EnemyAttackEvent?.Invoke();
    }
    public void StartCollectibleEvent(int id,int quantity)
    {
        CollectibleEvent?.Invoke(id,quantity);
    }
    public void StartResetValueEvent()
    {
        ResetValueEvent?.Invoke();
    }
    public void StartByUpgradeEvent(int id)
    {
        ByUpgradeEvent?.Invoke(id);
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