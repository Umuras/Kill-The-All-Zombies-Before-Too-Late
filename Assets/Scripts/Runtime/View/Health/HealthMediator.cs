using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMediator : EventMediator
{
    [Inject]
    public HealthView view { get; set; }
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
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerModel.playerHealth < 100)
            {
                playerModel.playerHealth = 100;
                playerAndWeaponUIModel.UpdatePlayerHealthText(playerModel.playerHealth);
            }

            gameObject.SetActive(false);
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
