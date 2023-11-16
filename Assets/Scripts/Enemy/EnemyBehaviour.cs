using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float wanderRadius = 10f; // Adjust the value as needed
    [SerializeField] float patrolSpeed = 1f;
    [SerializeField] float chaseSpeed = 3.5f;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }

        else if(distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            animator.SetTrigger("isChasing");
        }

        else
        {
            isProvoked = false;
            animator.SetBool("isAttacking", false);
            animator.ResetTrigger("isChasing");
            Patrol();
        }
    }

    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
            navMeshAgent.speed = chaseSpeed;
        }

        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
        animator.SetBool("isAttacking", false);
    }

    private void AttackTarget()
    {
        Debug.Log("attacking");
        animator.SetBool("isAttacking", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Calculate a random position on the NavMesh within a radius.
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            Vector3 finalPosition = hit.position;

            // Set the calculated position as the destination.
            navMeshAgent.SetDestination(finalPosition);
            navMeshAgent.speed = patrolSpeed;
        }
    }
}
