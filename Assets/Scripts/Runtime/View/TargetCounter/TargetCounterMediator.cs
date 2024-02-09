using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCounterMediator : EventMediator
{
    [Inject]
    public TargetCounterView view { get; set; }
    [Inject]
    public ITargetModel targetModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        InitTargetCount();
    }

    private void InitTargetCount()
    {
        targetModel.targetQuantity = view.TargetsTransform.childCount;
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
