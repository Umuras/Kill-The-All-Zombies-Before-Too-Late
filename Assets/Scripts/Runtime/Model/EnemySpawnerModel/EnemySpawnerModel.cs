using RSG;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerModel : IEnemySpawnerModel
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    [Inject]
    public IBundleModel bundleModel { get; set; }

    public List<Transform> enemySpawnPoints { get; set; }
    public Transform enemyFolder { get; set; }

    private float enemySpawnCoolDownTime = 5f;

    public void EnemySpawner()
    {
       bundleModel.AddressableInstantiateAndReach("Enemy", enemyFolder).Then((enemy) =>
       {
           int randomEnemyPoint = Random.Range(0, enemySpawnPoints.Count);
           enemy.transform.position = enemySpawnPoints[randomEnemyPoint].position;
           int randomCharacterNo = Random.Range(0, 52);
           enemy.GetComponent<EnemyView>().enemies[randomCharacterNo].SetActive(true);
           enemySpawnCoolDownTime = 5;
       });
    }
}
