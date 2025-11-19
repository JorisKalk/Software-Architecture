using UnityEngine;
using UnityEngine.Events;

public class GameEventSubscriber : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent;
    [SerializeField]
    private UnityEvent<EventData> onEventPublished;

    private void OnEnable()
    {
        gameEvent.RegisterSubscriber(this);
    }

    private void OnDisable()
    {
        gameEvent.UnregisterSubscriber(this);
    }

    public void OnEventPublished(EventData eventData, int hideMethodInInspector = 0)
    {
        onEventPublished?.Invoke(eventData);
    }
}
