using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemySpawnerEvent
{
    StartWaveAndSetEnemyVisible,
    StopSetEnemyVisible
}

public class EnemySpawnerMediator : EventMediator
{
    [Inject]
    public EnemySpawnerView view { get; set; }
    [Inject]
    public IEnemySpawnerModel enemySpawnerModel { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(EnemySpawnerEvent.StartWaveAndSetEnemyVisible, OnStartWaveAndSetEnemyVisible);
        dispatcher.AddListener(EnemySpawnerEvent.StopSetEnemyVisible, OnStopSetEnemyVisible);
        Init();
    }

    private void Init()
    {
        enemySpawnerModel.InitializeEnemiesList();
        enemySpawnerModel.enemyFolder = view.enemyFolder;
        enemySpawnerModel.enemySpawnPoints = view.enemySpawnPoints;
    }

    public void OnStartWaveAndSetEnemyVisible()
    {
        Invoke("StartNewWave", 0);
        InvokeRepeating("SetEnemyVisible", 1, 0.2f);
    }

    private void StartNewWave()
    {
        enemySpawnerModel.StartNewWave();
    }
    private void SetEnemyVisible()
    {
        enemySpawnerModel.SetEnemyVisible();
    }
    
    public void OnStopSetEnemyVisible()
    {
        CancelInvoke("SetEnemyVisible");
    }

    private void Update()
    {
        if (!enemySpawnerModel.checkingFinishedForDeadEnemies)
        {
            enemySpawnerModel.IsDeadAllEnemies();
        }

        if (enemySpawnerModel.deadAllEnemies)
        {
            enemySpawnerModel.OpenWaveFinishPanel();
        }
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(EnemySpawnerEvent.StartWaveAndSetEnemyVisible, OnStartWaveAndSetEnemyVisible);
        dispatcher.RemoveListener(EnemySpawnerEvent.StopSetEnemyVisible, OnStopSetEnemyVisible);
    }
}
