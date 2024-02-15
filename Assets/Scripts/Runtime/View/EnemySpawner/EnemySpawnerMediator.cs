using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerMediator : EventMediator
{
    [Inject]
    public EnemySpawnerView view { get; set; }
    [Inject]
    public IEnemySpawnerModel enemySpawnerModel { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        Init();
    }

    private void Init()
    {
        enemySpawnerModel.enemyFolder = view.enemyFolder;
        enemySpawnerModel.enemySpawnPoints = view.enemySpawnPoints;
    }

    private void Start()
    {
        InvokeRepeating("EnemySpawner", 5, 5);
    }

    private void EnemySpawner()
    {
        enemySpawnerModel.EnemySpawner();
    }
 
    public override void OnRemove()
    {
        base.OnRemove();
    }
}
