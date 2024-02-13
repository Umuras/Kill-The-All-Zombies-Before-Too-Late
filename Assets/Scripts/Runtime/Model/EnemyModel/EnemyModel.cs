using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public ParticleSystem bloodyEffect { get; set; }
    public ParticleSystem deathEffect { get; set; }
    public ParticleSystem enemyHitEffect { get; set; }


    public void DecreasingEnemyHealth(int weaponDamage)
    {
        if (enemyHealth > 0)
        {
            enemyHealth -= weaponDamage;
            bloodyEffect.Play();
            enemyHitEffect.Play();
        }


        if (enemyHealth <= 0)
        {
            animator.SetTrigger("isDead");
            animator.gameObject.GetComponent<BoxCollider>().enabled = false;
            enemyIsDead = true;
            agent.SetDestination(agent.gameObject.transform.position);
            WaitDeadAnim();
        }
    }

    public async void WaitDeadAnim()
    {
        await RunDeathEffectAfterAnimFinished();
    }

    public async Task RunDeathEffectAfterAnimFinished()
    {
        await Task.Delay(3200);
        deathEffect.Play();
    }
}
