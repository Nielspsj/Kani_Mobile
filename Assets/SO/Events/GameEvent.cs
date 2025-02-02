using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }    

    public void AddListeners(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListeners(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
