using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : ScriptableObject
{
    public enum ConditionType {Player, Enemy, Trigger}
    public abstract bool isMet();
}
