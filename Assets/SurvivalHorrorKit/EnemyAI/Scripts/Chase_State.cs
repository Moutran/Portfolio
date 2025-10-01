using UnityEngine;

public class Chase_State : EnemyState
{
    private Enemy_AI enemy;

    public void EnterState(Enemy_AI enemy)
    {
        Debug.Log("Joined Chase");
        this.enemy = enemy;
        enemy.agent.isStopped = false;
        enemy.animator.SetBool("Chase", true);
        enemy.agent.speed = enemy.chaseSpeed;
    }

    public void UpdateState()
    {
        enemy.agent.speed = enemy.chaseSpeed;
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        if (distance <= enemy.attackRange)
        {
            enemy.TransitionToState(new AttackState());
            return;
        }

        /*if (distance > enemy.proximityRange)
        {
            enemy.TransitionToState(new Patrol_State());
            return;
        }*/

        enemy.agent.SetDestination(enemy.player.position);
        Vector3 direction = enemy.player.position - enemy.transform.position;
        direction.y = 0f; // Ignore vertical component
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public void ExitState()
    {
        enemy.animator.SetBool("Chase", false);
        enemy.agent.speed = enemy.walkSpeed;
    }
}
