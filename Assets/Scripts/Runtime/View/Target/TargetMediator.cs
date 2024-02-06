using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMediator : EventMediator
{
    [Inject]
    public TargetView view { get; set; }
    [Inject]
    public ITargetModel targetModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
