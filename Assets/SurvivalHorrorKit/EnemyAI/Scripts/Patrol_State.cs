using UnityEngine;
using UnityEngine.AI;

public class Patrol_State : EnemyState
{
    private Enemy_AI enemy;

    public void EnterState(Enemy_AI enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("Patrol", true);
        enemy.agent.isStopped = false;
        enemy.agent.speed = enemy.walkSpeed;
        SetNewDestination();
    }

    public void UpdateState()
    {
        enemy.agent.speed = enemy.walkSpeed;
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.position);
        Debug.DrawLine(enemy.transform.position, enemy.agent.destination, Color.green);
        bool isCrouching = enemy.player.gameObject.GetComponent<FirstPersonController>().isCrouching;

        // Transition to Chase if player is seen
        if (distanceToPlayer <= enemy.proximityRange && !isCrouching)
        {
            bool isHiding = enemy.player.gameObject.GetComponent<FirstPersonController>().isHiding;
            if (!isHiding)
            {
                enemy.TransitionToState(new Chase_State());
                return;
            }
        }

        // Rotate toward movement direction if needed
        Vector3 velocity = enemy.agent.velocity;
        if (velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, Time.deltaTime * 8f);
        }

        // Patrol point reached
        if (!enemy.agent.pathPending &&
            enemy.agent.remainingDistance <= enemy.agent.stoppingDistance + 0.1f &&
            (!enemy.agent.hasPath || enemy.agent.velocity.sqrMagnitude < 0.1f))
        {
            SetNewDestination();
        }
    }

    public void ExitState()
    {
        enemy.animator.SetBool("Patrol", false);
    }

    private void SetNewDestination()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * enemy.patrolRadius;
            randomDirection += enemy.transform.position;
            randomDirection.y = enemy.transform.position.y;

            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, enemy.patrolRadius, NavMesh.AllAreas))
            {
                enemy.agent.SetDestination(hit.position);
                enemy.agent.isStopped = false;
                Debug.DrawRay(hit.position, Vector3.up * 2, Color.cyan, 2f);
                return;
            }
        }

        Debug.LogWarning("PatrolState: Could not find a valid patrol point.");
    }
}
