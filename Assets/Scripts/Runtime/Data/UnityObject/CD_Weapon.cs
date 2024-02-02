using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_Weapon", menuName = "FindingJob/CD_Weapon", order = 2)]
public class CD_Weapon : ScriptableObject
{
   public List<WeaponData> WeaponData;
}
