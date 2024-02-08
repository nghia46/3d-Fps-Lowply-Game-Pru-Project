using UnityEngine;
using UnityEngine.Events;
public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent onEventTriggered;
    void OnEnable()
    {
        gameEvent.AddListener(this);
    }
    void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }
    public void OnEventTriggered()
    {
        Debug.Log("The Event Invoke!");
        onEventTriggered.Invoke();
    }
}