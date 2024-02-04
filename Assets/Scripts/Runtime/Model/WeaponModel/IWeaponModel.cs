using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponModel
{
    public int weaponIndex { get; set; }
    public float pistolDamagePower { get; set; }
    public float pistolMagCapacity { get; set; }
    public float pistolMagCount { get; set; }
    public float totalPistolMagInside { get; set; }
    public float totalPistolAmmo { get; set; }
    public float pistolShootRange { get; set; }

    public float rifleDamagePower { get; set; }
    public float rifleMagCapacity { get; set; }
    public float rifleMagCount { get; set; }
    public float totalRifleMagInside { get; set; }
    public float totalRifleAmmo { get; set; }

    public float rifleShootRange { get; set; }

    public bool reloading { get; set; }

    public GameObject MuzzleFlash { get; set; }


    List<WeaponData> weaponData { get; set; }

    void RaycastForWeapon(Transform muzzlePosition, float shootRange);

    void FireBullet(Transform muzzlePosition, WeaponKeys weaponKey);

    void InitializeWeaponsProperties();

    void Reload(AudioSource reloadAudioSource);
}
