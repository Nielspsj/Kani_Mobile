using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    public UnityEvent reaction;

    private void OnEnable()
    {
        gameEvent.AddListeners(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveListeners(this);
    }

    public virtual void OnEventRaised()
    {
        reaction.Invoke();
    }
}
