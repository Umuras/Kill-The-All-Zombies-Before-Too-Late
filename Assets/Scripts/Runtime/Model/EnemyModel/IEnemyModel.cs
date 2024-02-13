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
    ParticleSystem bloodyEffect { get; set; }
    ParticleSystem deathEffect { get; set; }
    ParticleSystem enemyHitEffect { get; set; }

    void DecreasingEnemyHealth(int weaponDamage);
}
