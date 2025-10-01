using UnityEngine;

public class AttackState : EnemyState
{
    private Enemy_AI enemy;

    public void EnterState(Enemy_AI enemy)
    {
        this.enemy = enemy;
        enemy.agent.isStopped = true;
    }

    public void UpdateState()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.player.position);

        Vector3 direction = enemy.player.position - enemy.transform.position;
        direction.y = 0f; // Ignore vertical component
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        if (distance > enemy.attackRange)
        {
            enemy.TransitionToState(new Chase_State());
            return;
        }

        if (Time.time - enemy.lastAttackTime > enemy.attackCooldown)
        {
            enemy.lastAttackTime = Time.time;
            PerformAttack();
        }
    }

    public void ExitState()
    {
        enemy.agent.isStopped = false;
    }

    private void PerformAttack()
    {
        enemy.animator.SetTrigger("Attack");
    }
}
