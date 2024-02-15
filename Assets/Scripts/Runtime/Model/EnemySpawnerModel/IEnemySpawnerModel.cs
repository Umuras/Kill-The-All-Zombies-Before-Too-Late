using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemySpawnerModel
{
    List<Transform> enemySpawnPoints { get; set; }

    Transform enemyFolder { get; set; }
    void EnemySpawner();
}
