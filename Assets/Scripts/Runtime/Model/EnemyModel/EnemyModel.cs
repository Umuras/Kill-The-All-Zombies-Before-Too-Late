using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel : IEnemyModel
{
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    public int enemyHealth { get; set; }
    public int enemyDamage { get; set; }
    public bool enemyIsDead { get; set; }

    public ParticleSystem bloodyEffect { get; set; }
    public ParticleSystem deathEffect { get; set; }
    public ParticleSystem enemyHitEffect { get; set; }

    private int _enemyDeathPrize = 5;


    public void DecreasingEnemyHealth(int weaponDamage, EnemyView enemyView)
    {
        if (enemyView.enemyHealth > 0)
        {
            enemyView.enemyHealth -= weaponDamage;
            enemyView.bloodyEffect.Play();
            enemyView.enemyHitEffect.Play();
            enemyView.animator.SetTrigger("isDamage");
        }


        if (enemyView.enemyHealth <= 0)
        {
            enemyView.animator.ResetTrigger("isDamage");
            enemyView.animator.SetTrigger("isDead");
            enemyView.animator.gameObject.GetComponent<BoxCollider>().enabled = false;
            enemyIsDead = true;
            enemyView.agent.SetDestination(enemyView.agent.gameObject.transform.position);
            playerAndWeaponUIModel.gameTime += _enemyDeathPrize;
            WaitDeadAnim(enemyView);
        }
    }

    public async void WaitDeadAnim(EnemyView enemyView)
    {
        await RunDeathEffectAfterAnimFinished(enemyView);
    }

    public async Task RunDeathEffectAfterAnimFinished(EnemyView enemyView)
    {
        await Task.Delay(3200);
        enemyView.deathEffect.Play();
    }
}
