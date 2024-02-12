using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : StateMachineBehaviour
{
    private NavMeshAgent _agent;
    private Transform _player;

    public float stopAttackingDistance = 2.5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initialization

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = animator.gameObject.GetComponent<NavMeshAgent>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer();

        float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);

        //Checking if the agent should stop Attacking
        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
    private void LookAtPlayer()
    {
        Vector3 direction = _player.position - _agent.transform.position;
        _agent.transform.rotation = Quaternion.LookRotation(direction);

        float yRotation = _agent.transform.eulerAngles.y;
        _agent.transform.rotation = Quaternion.Euler(0,yRotation, 0);
    }
}
