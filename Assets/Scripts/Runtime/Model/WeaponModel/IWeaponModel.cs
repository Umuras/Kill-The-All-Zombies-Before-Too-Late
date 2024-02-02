using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponModel
{
    List<WeaponData> weaponData { get; set; }

    void RaycastForWeapon(Transform muzzlePosition);

    void FireBullet(Transform muzzlePosition, WeaponKeys weaponKey);
}
