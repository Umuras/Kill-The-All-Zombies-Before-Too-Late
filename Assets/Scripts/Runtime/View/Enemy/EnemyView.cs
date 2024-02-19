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
    public bool enemyIsDead;
    public ParticleSystem bloodyEffect;
    public ParticleSystem deathEffect;
    public ParticleSystem enemyHitEffect;
    public AudioSource enemyAudioSource;
    public AudioClip enemyAttackClip;
    public AudioClip enemyDeathClip;
    public AudioClip enemyHitClip;
    public List<GameObject> enemies = new List<GameObject>();
}
