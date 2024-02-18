using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerSpeedMediator : EventMediator
{
    [Inject]
    public IncreasePlayerSpeedView view { get; set; }
    [Inject]
    public IPlayerMoveModel playerMoveModel { get; set; }
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
            if (!playerAndWeaponUIModel.multiplyTwoSpeedLabel.gameObject.activeInHierarchy)
            {
                playerMoveModel.playerMoveSpeed *= view.increasePlayerSpeedConstant;
            }
            playerAndWeaponUIModel.increasePlayerMoveSpeedFinishTime = 20;
            playerAndWeaponUIModel.multiplyTwoSpeedLabel.gameObject.SetActive(true);
            playerMoveModel.isPlayerSpeedIncreased = true;
            gameObject.SetActive(false);
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
