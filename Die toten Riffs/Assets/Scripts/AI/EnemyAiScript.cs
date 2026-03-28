using UnityEngine;
using UnityEngine.AI;
public class NavMeshEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float fovAngle = 90f;
    public Transform[] patrolPoints;
    public float waitAtPointTime = 2f;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private int currentPatrolIndex = 0;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float attackCooldown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
        GoToNextPatrolPoint();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= detectionRange && IsPlayerInFoV())
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(player.position);

            if (dist <= attackRange)
            {

                agent.isStopped = true;
                agent.acceleration = 50f;
                Attack();

            }
            else
            {
                agent.isStopped = false;
                agent.acceleration = 8f;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.acceleration = 8f;
            HandlePatrol();
        }
    }

    bool IsPlayerInFoV()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleBetween = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleBetween < fovAngle / 2f)
        {
            if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out RaycastHit hit, detectionRange))
            {
                if (hit.transform == player)
                    return true;
                else
                    return false;
            }
        }
        return false;
    }

    void HandlePatrol()
    {
        agent.speed = patrolSpeed;

        if (patrolPoints.Length == 0) return;

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitAtPointTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                GoToNextPatrolPoint();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            isWaiting = true;
        }
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void Attack()
    {

        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;
        HealthScript PPlayer = GameObject.Find("Player").GetComponent<HealthScript>();
        PPlayer.TakeDamage(5f);

    }
}