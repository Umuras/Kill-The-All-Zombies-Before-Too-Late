using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMediator : EventMediator
{
    [Inject]
    public EnemyView view { get; set; }
    [Inject]
    public IEnemyModel enemyModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        Init();
    }

    private void Init()
    {
        enemyModel.agent = view.agent;
        enemyModel.animator = view.animator;
        enemyModel.enemyHealth = view.enemyHealth;
        enemyModel.enemyDamage = view.enemyDamage;
    }

    private void Update()
    {
        //if (!enemyModel.enemyIsDead)
        //{
        //    //enemyModel.EnemyStateControl();
        //}
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
