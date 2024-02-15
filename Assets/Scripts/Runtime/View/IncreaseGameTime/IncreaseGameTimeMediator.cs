using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseGameTimeMediator : EventMediator
{
    [Inject]
    public IncreaseGameTimeView view { get; set; }
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
            playerAndWeaponUIModel.gameTime += view.extraGameTime;
            gameObject.SetActive(false);
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
