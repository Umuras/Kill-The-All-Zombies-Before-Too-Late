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
    [Inject]
    public IEnemySpawnerModel enemySpawnerModel { get; set; }

    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    public int enemyHealth { get; set; }
    public int enemyDamage { get; set; }

    public ParticleSystem bloodyEffect { get; set; }
    public ParticleSystem deathEffect { get; set; }
    public ParticleSystem enemyHitEffect { get; set; }

    private int _enemyDeathTimePrize = 5;
    private int _enemyDeathScorePrize = 10;


    public void DecreasingEnemyHealth(int weaponDamage, EnemyView enemyView)
    {
        if (enemyView.enemyHealth > 0)
        {
            enemyView.enemyHealth -= weaponDamage;
            enemyView.bloodyEffect.Play();
            enemyView.enemyHitEffect.Play();
            enemyView.animator.SetTrigger("isDamage");
            ChangeEnemyAudioClipAndPlay(enemyView, enemyView.enemyHitClip);
        }


        if (enemyView.enemyHealth <= 0)
        {
            enemyView.animator.ResetTrigger("isDamage");
            enemyView.animator.SetTrigger("isDead");
            ChangeEnemyAudioClipAndPlay(enemyView,enemyView.enemyDeathClip);
            enemyView.animator.gameObject.GetComponent<BoxCollider>().enabled = false;
            enemyView.enemyIsDead = true;
            enemyView.agent.SetDestination(enemyView.agent.gameObject.transform.position);
            playerAndWeaponUIModel.gameTime += _enemyDeathTimePrize;
            enemySpawnerModel.aliveEnemies.Remove(enemyView.gameObject);
            enemySpawnerModel.deadEnemies.Add(enemyView.gameObject);
            playerAndWeaponUIModel.playerMissionLabel.text = $"Mission: Kill the all zombies \r\n Quantity of Zombies =  {enemySpawnerModel.enemyFolder.childCount - enemySpawnerModel.deadEnemies.Count}";
            playerAndWeaponUIModel.score += _enemyDeathScorePrize;
            playerAndWeaponUIModel.scoreText.text = $"Score = {playerAndWeaponUIModel.score}";
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

    public void ChangeEnemyAudioClipAndPlay(EnemyView enemyView, AudioClip enemyClip)
    {
        enemyView.enemyAudioSource.clip = enemyClip;
        enemyView.enemyAudioSource.Play();
    }
}
