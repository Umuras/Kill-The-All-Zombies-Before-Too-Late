using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerView : EventView
{
    public List<Transform> enemySpawnPoints = new List<Transform>();
    public Transform enemyFolder;
}
