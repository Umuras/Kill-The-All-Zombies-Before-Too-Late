using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawnerModel : IPowerUpsSpawnerModel
{
    public void SpawnPowerUps(Transform powerUpsFolder, List<Transform> powerUpsSpawnPoints, List<GameObject> powerUps)
    {
        for (int i = 0; i < powerUpsSpawnPoints.Count; i++)
        {
            int randomPowerUp = Random.Range(0, powerUps.Count);
            GameObject powerUp = Object.Instantiate(powerUps[randomPowerUp], powerUpsFolder);
            powerUp.transform.position = powerUpsSpawnPoints[i].position;
        }
    }

    public void DeleteAllPowerUps(Transform powerUpsFolder)
    {
        foreach (Transform powerUp in powerUpsFolder)
        {
            Object.Destroy(powerUp.gameObject);
        }
    }
}
