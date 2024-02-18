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
    [Inject]
    public IUIPanelModel uIPanelModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public List<Transform> enemySpawnPoints { get; set; }
    public Transform enemyFolder { get; set; }

    public List<GameObject> aliveEnemies { get; set; }
    public List<GameObject> deadEnemies { get; set; }
    public int waveCount = 1;
    public bool deadAllEnemies { get; set; }
    public bool checkingFinishedForDeadEnemies { get; set; }

    private int enemyQuantityOfPerWave = 10;
    private int _index = 0;
    private bool isVisibleAllEnemies;
    private int _maximumEnemyQuantity = 200;

    
    public void InitializeEnemiesList()
    {
        aliveEnemies = new List<GameObject>();
        deadEnemies = new List<GameObject>();
    }

    public void EnemySpawner()
    {
        for (int i = 0; i < enemyQuantityOfPerWave; i++)
        {
            bundleModel.AddressableInstantiateAndReach("Enemy", enemyFolder).Then((enemy) =>
            {
                int randomEnemyPoint = Random.Range(0, enemySpawnPoints.Count);
                enemy.transform.position = enemySpawnPoints[randomEnemyPoint].position;
                int randomCharacterNo = Random.Range(0, 51);
                enemy.GetComponent<EnemyView>().enemies[randomCharacterNo].SetActive(true);
                enemy.gameObject.SetActive(false);
                aliveEnemies.Add(enemy);
            }).Then(() =>
            {
                playerAndWeaponUIModel.playerMissionLabel.text = $"Mission: Kill the all zombies \r\n Quantity of Zombies =  {enemyFolder.childCount}";
            });
        }
    }

    public void SetEnemyVisible()
    {
        enemyFolder.GetChild(_index++).gameObject.SetActive(true);

        if (_index == enemyQuantityOfPerWave)
        {
            _index = 0;
            dispatcher.Dispatch(EnemySpawnerEvent.StopSetEnemyVisible);
        }
    }

    public void StartNewWave()
    {
        if (waveCount != 1)
        {
            aliveEnemies.Clear();
            deadEnemies.Clear();
            DestroyAllEnemies();
            dispatcher.Dispatch(PowerUpsSpawnerEvent.DeleteAllPowerUps);

            if (enemyQuantityOfPerWave != _maximumEnemyQuantity)
            {
                enemyQuantityOfPerWave += 5;
            }
        }
        if (!playerAndWeaponUIModel.waveNumber.gameObject.activeInHierarchy)
        {
            playerAndWeaponUIModel.waveNumber.gameObject.SetActive(true);
        }
        playerAndWeaponUIModel.waveNumber.text = $"Wave {waveCount}";
        waveCount++;
        EnemySpawner();
        dispatcher.Dispatch(PowerUpsSpawnerEvent.StartSpawningPowerUps);
    }

    public void IsDeadAllEnemies()
    {
        if (deadEnemies.Count == enemyQuantityOfPerWave)
        {
            deadAllEnemies = true;
            checkingFinishedForDeadEnemies = true;
        }
    }

    public void OpenWaveFinishPanel()
    {
        deadAllEnemies = false;
        uIPanelModel.OpenPanel(2, PanelKeys.WAVEFINISHPANEL).Then(() =>
        {
            dispatcher.Dispatch(WaveFinishPanelEvent.WaveFinishCountdownStart);
        });
    }

    private void DestroyAllEnemies()
    {
        foreach (Transform enemy in enemyFolder)
        {
            Object.Destroy(enemy.gameObject);
        }
    }
}
