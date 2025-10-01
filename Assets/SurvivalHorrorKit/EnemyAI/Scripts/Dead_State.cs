using UnityEngine;
using UnityEngine.AI;

public class Dead_State : EnemyState
{
    private Enemy_AI enemy;

    public void EnterState(Enemy_AI enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetTrigger("Death");
        enemy.agent.isStopped = true;
    }

    public void UpdateState()
    {
       //Empty
    }

    public void ExitState()
    {
        //Empty
    }
}
