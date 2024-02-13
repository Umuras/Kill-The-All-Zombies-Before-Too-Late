using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : EventView
{
    public NavMeshAgent agent;
    public Animator animator;
    public int enemyHealth;
    public int enemyDamage;
    public ParticleSystem bloodyEffect;
    public ParticleSystem deathEffect;
    public ParticleSystem enemyHitEffect;
}
