using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EventEntry
{
    public Event gameEvent;
    public UnityEvent onTriggered; 
}
