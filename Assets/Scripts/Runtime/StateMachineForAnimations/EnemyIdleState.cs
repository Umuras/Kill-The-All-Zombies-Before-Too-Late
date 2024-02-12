using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
    private float _timer;
    public float idleTime = 0f;

    private Transform _player;

    public float playerDetectionAreaRadius = 18f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Transition to Patrol State -- //

        _timer += Time.deltaTime;
        if (_timer > idleTime)
        {
            animator.SetBool("isPatroling", true);
        }

        // -- Transition to Chase State -- //

        float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);
        if (distanceFromPlayer < playerDetectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }
    }
}
