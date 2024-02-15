using DG.Tweening;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseGameTimeView : EventView
{
    [HideInInspector]
    public int extraGameTime = 60;
    protected override void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 3, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }
}
