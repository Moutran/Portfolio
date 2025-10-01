using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions / PlayerHealthBelowCondition")]
public class PlayerHealthBelowCondition : Condition
{
    public new ConditionType ConditionType = ConditionType.Player;
    public float threshold;

    public override bool isMet()
    {
        FirstPersonController player = FirstPersonController.Instance;
        if(player.currentHealth <= threshold)
        {
            return true;
        }
        return false;
    }
}
