using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    [HideInInspector]
    public List<GameEventSubscriber> subscribers;

    [HideInInspector]
    public GameObject mostRecentPublisher;
    [HideInInspector]
    public string mostRecentPublisherName;
    [HideInInspector]
    public EventData mostRecentPublishedEventData;

    public void RegisterSubscriber(GameEventSubscriber subscriber)
    {
        subscribers.Add(subscriber);
    }

    public void UnregisterSubscriber(GameEventSubscriber subscriber)
    {
        subscribers.Remove(subscriber);
    }

    public void Publish(EventData eventData, GameObject publisher)
    {
        mostRecentPublisher = publisher;
        mostRecentPublisherName = publisher.name;
        mostRecentPublishedEventData = eventData;

        foreach (GameEventSubscriber subscriber in subscribers)
        {
            subscriber.OnEventPublished(eventData);
        }
    }

    private void OnEnable()
    {
        mostRecentPublisher = null;
        mostRecentPublisherName = "";
        mostRecentPublishedEventData = null;
    }
}

[Serializable]
public abstract class EventData
{
    public string name;
}


public class EventBus<T> where T : EventData
{
    public static event Action<T> OnEventPublished;

    public static void Publish(T eventData)
    {
        OnEventPublished?.Invoke(eventData);
    }
}