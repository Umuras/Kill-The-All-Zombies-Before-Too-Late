using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponModel
{
    public int weaponIndex { get; set; }
    public int pistolDamagePower { get; set; }
    public int pistolMagCapacity { get; set; }
    public int pistolMagCount { get; set; }
    public int totalPistolMagInside { get; set; }
    public int totalPistolAmmo { get; set; }
    public int pistolShootRange { get; set; }
    public ParticleSystem pistolParticleSystem { get; set; }

    public int rifleDamagePower { get; set; }
    public int rifleMagCapacity { get; set; }
    public int rifleMagCount { get; set; }
    public int totalRifleMagInside { get; set; }
    public int totalRifleAmmo { get; set; }
    public int rifleShootRange { get; set; }
    public ParticleSystem rifleParticleSystem { get; set; }
    public bool reloading { get; set; }
    public Animation fireAnimation { get; set; }
    public AudioSource fireAudioSource { get; set; }
    public List<Transform> weaponMuzzleTransform { get; set; }
    List<WeaponData> weaponData { get; set; }

    void RaycastForWeapon(Transform muzzlePosition, float shootRange);

    void FireBullet(Transform muzzlePosition, WeaponKeys weaponKey);

    void InitializeWeaponsProperties(AudioSource fireAudioSource, AudioSource reloadAudioSource, Animation fireAnimation, WeaponKeys firstWeapon, List<Transform> weaponMuzzleTransform, ParticleSystem pistolParticle, ParticleSystem rifleParticle);

    void Reload(AudioSource reloadAudioSource);

    void ChangeWeapon(List<GameObject> weaponList, WeaponKeys weaponKey, Animation fireAnimation, AudioSource reloadAudioSource);
}
