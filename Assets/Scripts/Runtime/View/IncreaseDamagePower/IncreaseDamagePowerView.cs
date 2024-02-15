using DG.Tweening;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamagePowerView : EventView
{
    [HideInInspector]
    public int increaseDamagePowerConstant = 2;
    protected override void Start()
    {
        base.Start();
        transform.DORotate(new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
}
