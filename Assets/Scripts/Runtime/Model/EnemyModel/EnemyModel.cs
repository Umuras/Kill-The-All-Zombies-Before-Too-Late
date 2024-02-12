using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : IEnemyModel
{
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }

    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    public int enemyHealth { get; set; }
    public int enemyDamage { get; set; }
    public bool enemyIsDead { get; set; }

    public void EnemyStateControl()
    {
        agent.SetDestination(playerMoveModel.characterController.transform.position);
        if (agent.velocity == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else
        {
            float distance = Vector3.Distance(playerMoveModel.characterController.transform.position, agent.transform.position);
            Debug.Log(distance);

            if (distance >= 20 )
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isRunning", false);
            }
            
            if (distance > 5 && distance < 20)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
            }
            else if (distance < 3)
            {
                animator.SetBool("isAttacking", true);
            }
        }
    }

    public void DecreasingEnemyHealth(int weaponDamage)
    {
        if (enemyHealth > 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                animator.SetTrigger("isDamage");
            }
            enemyHealth -= weaponDamage;
        }


        if (enemyHealth <= 0)
        {
            animator.SetTrigger("isDead");
            enemyIsDead = true;
            agent.SetDestination(agent.gameObject.transform.position);
        }
    }
}
