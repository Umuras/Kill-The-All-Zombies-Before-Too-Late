using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : StateMachineBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;

    public float chaseSpeed = 6f;

    public float stopChasingDistance = 21f;
    public float attackingDistance = 2.5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initialization

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = animator.gameObject.GetComponent<NavMeshAgent>();

        _agent.speed = chaseSpeed;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.position);
        animator.transform.LookAt(_player);

        float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);

        //Checking if the agent should stop Chasing
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }

        //Checking if the agent should Attack

        if (distanceFromPlayer < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }

}
