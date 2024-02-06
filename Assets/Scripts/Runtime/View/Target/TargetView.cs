using DG.Tweening;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetView : EventView
{
    public int targetHealth = 100;
    public bool IsStandTarget;
    public bool hasYoyoAnim;
    public TextMeshPro damageText;
    public GameObject damageTextGo;

    protected override void Awake()
    {
        base.Awake();
        if (hasYoyoAnim)
        {
            gameObject.transform.DOLocalMoveZ(1.57f, 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
