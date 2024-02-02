using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WeaponData
{
    public WeaponProperties weapon;
}

[Serializable]
public struct WeaponProperties
{
    public string weaponName;
    public float shootRange;
    public float damagePower;
    public int magCapacity;
    public int magCount;
    public GameObject muzzle;
    public GameObject bullet;
}
