using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandMediator : EventMediator
{
    [Inject]
    public EnemyHandView view { get; set; }
    [Inject]
    public IPlayerModel playerModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (playerModel.playerHealth > 0)
            {
                playerModel.playerHealth -= 15;

                if (playerModel.playerHealth <= 0)
                {
                    playerAndWeaponUIModel.UpdatePlayerHealthText(0);
                    Debug.Log("Player DIEEEE");
                }
                else
                {
                    playerAndWeaponUIModel.UpdatePlayerHealthText(playerModel.playerHealth);
                }
            }
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
