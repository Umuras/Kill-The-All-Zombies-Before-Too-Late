using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolingState : StateMachineBehaviour
{
    private float _timer;
    public float patrolingTime = 10f;
    public float patrolSpeed = 2f;

    public float detectionArea = 18f;

    private List<Transform> waypointsList = new List<Transform>();

    private Transform _player;
    private NavMeshAgent _agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Initialization

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = animator.gameObject.GetComponent<NavMeshAgent>();

        _agent.speed = patrolSpeed;
        _timer = 0;

        //Get all waypoints and Move to First Waypoint

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoint");
        foreach (Transform waypoint in waypointCluster.transform)
        {
            waypointsList.Add(waypoint);
        }

        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        _agent.SetDestination(nextPosition);

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Transition to Patrol State
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);
        }

        // Transition to Idle State

        _timer += Time.deltaTime;
        if (_timer > patrolingTime)
        {
            animator.SetBool("isPatroling", false);
        }

        //Transition to Chase State
        float distanceFromPlayer = Vector3.Distance(_player.position, animator.transform.position);
        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing",true);
        }

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Stop the agent
        _agent.SetDestination(_agent.transform.position);
    }
}
