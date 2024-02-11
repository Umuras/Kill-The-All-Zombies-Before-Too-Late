using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEnemyModel
{
    NavMeshAgent agent { get; set; }
    Animator animator { get; set; }
    int enemyHealth { get; set; }
    int enemyDamage { get; set; }
    bool enemyIsDead { get; set; }

    void EnemyStateControl();

    void DecreasingEnemyHealth(int weaponDamage);
}
