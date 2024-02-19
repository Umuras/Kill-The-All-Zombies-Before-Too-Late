using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandView : EventView
{
    [HideInInspector]
    public int enemyHitDamage = 15;
    [HideInInspector]
    public EnemyView enemyView;
}
