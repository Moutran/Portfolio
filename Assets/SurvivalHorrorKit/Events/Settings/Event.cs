using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/New Event")]
public class Event : ScriptableObject
{
    public Condition[] conditions;
    public bool CheckConditions()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].isMet())
            {
                return false;
            }
        }
        return true;
    }
}
