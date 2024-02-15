using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyEvent
{
    ChooseEnemyCharacter
}

public class EnemyMediator : EventMediator
{
    [Inject]
    public EnemyView view { get; set; }
    [Inject]
    public IEnemyModel enemyModel { get; set; }

    public override void OnRegister()
    {

    }

    public override void OnRemove()
    {

    }
}
