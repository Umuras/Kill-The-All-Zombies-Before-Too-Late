using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUpsSpawnerModel
{
    void SpawnPowerUps(Transform powerUpsFolder, List<Transform> powerUpsSpawnPoints, List<GameObject> powerUps);
    void DeleteAllPowerUps(Transform powerUpsFolder);
}
