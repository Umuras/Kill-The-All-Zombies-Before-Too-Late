using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHandMediator : EventMediator
{
    [Inject]
    public EnemyHandView view { get; set; }
    [Inject]
    public IPlayerModel playerModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }
    [Inject]
    public IEnemyModel enemyModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        view.enemyView = gameObject.GetComponentInParent<EnemyView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (playerModel.playerHealth > 0)
            {
                if (view.enemyView != null)
                {
                    enemyModel.ChangeEnemyAudioClipAndPlay(view.enemyView, view.enemyView.enemyAttackClip);
                }
                playerModel.playerHealth -= view.enemyHitDamage;

                if (playerModel.playerHealth <= 0)
                {
                    playerAndWeaponUIModel.UpdatePlayerHealthText(0);
                    playerAndWeaponUIModel.PlayerDeath();
                    PlayPlayerDeathMusic();
                }
                else
                {
                    playerAndWeaponUIModel.UpdatePlayerHealthText(playerModel.playerHealth);
                    PlayPlayerHitMusic();
                    playerModel.PlayerHitEffect();
                }
            }
        }
    }

    private void PlayPlayerHitMusic()
    {
        playerModel.playerAudioSource.clip = playerModel.playerHitClip;
        playerModel.playerAudioSource.Play();
    }

    private void PlayPlayerDeathMusic()
    {
        playerModel.playerAudioSource.clip = playerModel.playerDeathClip;
        playerModel.playerAudioSource.Play();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
