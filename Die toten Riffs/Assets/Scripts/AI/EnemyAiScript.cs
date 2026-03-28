using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemy : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float fovAngle = 90f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= detectionRange && IsPlayerInFoV())
        {
            agent.SetDestination(player.position);  // Automatically pathfinds!

            if (dist <= attackRange)
            {
                agent.isStopped = true;
                Attack();
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = true; // Idle
        }
    }

    bool IsPlayerInFoV()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angleBetween = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleBetween < fovAngle / 2f)
        {
            // Optional: Raycast to check if a wall is blocking line of sight
            if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out RaycastHit hit, detectionRange))
            {
                if (hit.transform == player)
                    return true; // Clear line of sight
                else
                    return false; // Wall is blocking
            }
        }

        return false;
    }

    void Attack()
    {
        Debug.Log("NavMesh Enemy Attacks!");
    }
}