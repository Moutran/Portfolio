using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<DayEvents> allDayEvents; // Editable in inspector
    private List<EventEntry> activeEvents = new();
    private DayNightManager dayNightManager;

    private int currentDay = -1;

    private void Start()
    {
        dayNightManager = DayNightManager.Instance;
    }

    private void Update()
    {
        int day = dayNightManager.daysSurvived;
        if (day != currentDay)
        {
            currentDay = day;
            UpdateActiveEvents(day);
        }


        for (int i = activeEvents.Count - 1; i >= 0; i--)
        {
            EventEntry entry = activeEvents[i];
            if (entry.gameEvent.CheckConditions())
            {
                entry.onTriggered.Invoke();
                activeEvents.RemoveAt(i); // Remove if one-shot; skip this line if repeatable
            }
        }
    }

    private void UpdateActiveEvents(int day)
    {
        activeEvents.Clear();
        var dayEntry = allDayEvents.Find(d => d.day == day);
        if (dayEntry != null)
            activeEvents.AddRange(dayEntry.events);
    }
}
