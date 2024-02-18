using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnerModel
{
    List<Transform> enemySpawnPoints { get; set; }
    public List<GameObject> aliveEnemies { get; set; }
    public List<GameObject> deadEnemies { get; set; }
    Transform enemyFolder { get; set; }
    bool deadAllEnemies { get; set; }
    bool checkingFinishedForDeadEnemies { get; set; }
    void EnemySpawner();
    void SetEnemyVisible();
    void IsDeadAllEnemies();
    void StartNewWave();
    void InitializeEnemiesList();
    void OpenWaveFinishPanel();
}
