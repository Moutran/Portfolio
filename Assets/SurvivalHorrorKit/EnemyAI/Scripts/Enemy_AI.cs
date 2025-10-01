using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI : MonoBehaviour
{
    [Header("References")]
    [HideInInspector] public Transform player;
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;

    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float chaseSpeed = 7f;
    public float stoppingDistance = 2f;
    public float patrolRadius = 10f;
    public float proximityRange = 15f;

    [Header("Health Settings")]
    public float health;
    public float maxHealth;

    [Header("Attack Settings")]
    public float damage = 15f;
    public float attackCooldown = 2f;
    [HideInInspector] public float lastAttackTime;
    public float attackRange = 2.5f;
    public float attackRadius = 0.5f;
    public bool hasHitPlayerThisSwing = false;

    public float visionRange = 15f;
    public GameObject checkPosition;

    private EnemyState currentState;

    void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>().gameObject.transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        TransitionToState(new Patrol_State());

        health = maxHealth;

        agent.updateRotation = false;
        agent.speed = walkSpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        currentState?.UpdateState();
    }

    public void TransitionToState(EnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

        int random = Random.Range(0, 100);
        animator.SetInteger("GetHit", random);
    }

    public void ResetHitAnimation()
    {
        animator.SetInteger("GetHit", 50);
    }

    void Die()
    {
        TransitionToState(new Dead_State());
        Destroy(gameObject, 5f);
    }

    public void DoAttackSphereCast()
    {
        if (hasHitPlayerThisSwing) return;

        Vector3 center = transform.position + transform.forward * (attackRange * 0.5f);
        Collider[] hits = Physics.OverlapSphere(center, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                FirstPersonController player = hit.GetComponent<FirstPersonController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                    hasHitPlayerThisSwing = true;
                    Debug.Log("Enemy hit player with OverlapSphere");
                    break; // Stop after first valid hit
                }
            }
        }

        // Debug view
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red, 1f);
        Debug.DrawLine(center - Vector3.up * 0.1f, center + Vector3.up * 0.1f, Color.yellow, 1f); // marker for OverlapSphere center
    }

    public void ResetAttackHit()
    {
        hasHitPlayerThisSwing = false;
    }

    public void CheckLineOfSight()
    {
        Debug.Log("Check Fire");
        Vector3 directionToPlayer = player.position - transform.position;

        Ray ray = new Ray(checkPosition.transform.position, directionToPlayer.normalized);
        RaycastHit hit;
        Debug.DrawLine(checkPosition.transform.position, player.position, Color.red);

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, visionRange))
        {
            if (hit.transform == player)
            {
                Debug.Log("Player is visible. Chase!");
                TransitionToState(new Chase_State());
            }
            else
            {
                Debug.Log("Player is hidden behind: " + hit.transform.name);
            }
        }
    }
}
