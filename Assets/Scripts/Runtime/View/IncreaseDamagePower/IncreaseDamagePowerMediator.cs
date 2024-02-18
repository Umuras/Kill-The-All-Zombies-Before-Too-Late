using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamagePowerMediator : EventMediator
{
    [Inject]
    public IncreaseDamagePowerView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
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
            if (!playerAndWeaponUIModel.multiplyTwoDamageLabel.gameObject.activeInHierarchy)
            {
                weaponModel.pistolDamagePower = weaponModel.pistolDamagePower * view.increaseDamagePowerConstant;
                weaponModel.rifleDamagePower = weaponModel.rifleDamagePower * view.increaseDamagePowerConstant;
            }
            playerAndWeaponUIModel.increaseDamagePowerFinishTime = 20f;
            playerAndWeaponUIModel.multiplyTwoDamageLabel.gameObject.SetActive(true);
            weaponModel.isWeaponIncreaseDamage = true;
            gameObject.SetActive(false);
        }
    }


    public override void OnRemove()
    {
        base.OnRemove();
    }
}
