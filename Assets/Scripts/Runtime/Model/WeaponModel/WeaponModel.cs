using DG.Tweening;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.Search;
using UnityEngine;

public class WeaponModel : IWeaponModel
{
    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public List<WeaponData> weaponData { get; set; }

    public int weaponIndex { get; set; }

    public float pistolDamagePower { get; set; }
    public float pistolMagCapacity { get; set; }
    public float totalPistolMagInside { get; set; }
    public float pistolMagCount { get; set; }
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


    [PostConstruct]
    public void GetWeaponData()
    {
        weaponData = Resources.Load<CD_Weapon>("Data/CD_Weapon").WeaponData;
    }

    public void InitializeWeaponsProperties()
    {
        pistolDamagePower = weaponData[(int)WeaponKeys.Pistol].weapon.damagePower;
        pistolMagCapacity = weaponData[(int)WeaponKeys.Pistol].weapon.magCapacity;
        pistolMagCount = weaponData[(int)WeaponKeys.Pistol].weapon.magCount;
        pistolShootRange = weaponData[(int)WeaponKeys.Pistol].weapon.shootRange;
        totalPistolMagInside = pistolMagCapacity;

        rifleDamagePower = weaponData[(int)WeaponKeys.Rifle].weapon.damagePower;
        rifleMagCapacity = weaponData[(int)WeaponKeys.Rifle].weapon.magCapacity;
        rifleMagCount = weaponData[(int)WeaponKeys.Rifle].weapon.magCount;
        rifleShootRange = weaponData[(int)WeaponKeys.Rifle].weapon.shootRange;
        totalRifleMagInside = rifleMagCapacity;

        MuzzleFlash = weaponData[(int)WeaponKeys.Pistol].weapon.muzzle;

        totalPistolAmmo = pistolMagCapacity * pistolMagCount;
        totalRifleAmmo = rifleMagCapacity * rifleMagCount;
    }

    public void RaycastForWeapon(Transform muzzlePosition, float shootRange)
    {
        Ray ray = new Ray(muzzlePosition.position, muzzlePosition.forward);

        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction*shootRange, color: Color.red);

        if (Physics.Raycast(ray, out hit, shootRange))
        {
            if (hit.collider.gameObject.CompareTag("Target"))
            {
                Debug.LogError("Hit Target!!!!");
            }
        }  
    }

    public void FireBullet(Transform muzzlePosition, WeaponKeys weaponKey)
    {
        GameObject bullet = objectPoolingModel.DequeuePoolableGameObject();
        bullet.transform.position = muzzlePosition.position;
        bullet.GetComponent<BulletView>().isCalledByPooling = true;
        bullet.GetComponent<Rigidbody>().AddForce(muzzlePosition.forward.normalized * 100f, ForceMode.Impulse);
        Debug.Log(bullet.transform.position);
    }

    public void Reload(AudioSource reloadAudioSource)
    {
        if (weaponIndex == (int)WeaponKeys.Pistol)
        {
            if (totalPistolMagInside == pistolMagCapacity || totalPistolAmmo == 0)
            {
                return;
            }
            else if (totalPistolAmmo == 0 && totalPistolMagInside == 0)
            {
                return;
            }
        }
        else
        {
            if (totalRifleMagInside == rifleMagCapacity || totalRifleAmmo == 0)
            {
                return;
            }
            else if (totalRifleAmmo == 0 && totalRifleMagInside == 0)
            {
                return;
            }
        }
        if (!reloadAudioSource.isPlaying)
        {
            reloadAudioSource.Play();
            ReloadAsync(reloadAudioSource);
        }
    }

    public async void ReloadAsync(AudioSource reloadAudioSource)
    {
      await WaitingReloadSound(reloadAudioSource);
    }

    private async Task WaitingReloadSound(AudioSource audioSource)
    {
        int clipLength = (int)audioSource.clip.length;
        if (weaponIndex == (int)WeaponKeys.Rifle)
        {
            clipLength += 1;
        }
        await Task.Delay(clipLength * 1000);
        if (weaponIndex == (int)WeaponKeys.Pistol)
        {
            playerAndWeaponUIModel.ReloadAmmo(totalWeaponMagInside: pistolMagCapacity, totalWeaponAmmo: totalPistolAmmo, weaponMagInside: totalPistolMagInside);
        }
        else
        {
            playerAndWeaponUIModel.ReloadAmmo(totalWeaponMagInside: rifleMagCapacity, totalWeaponAmmo: totalRifleAmmo, weaponMagInside: totalRifleMagInside);
        }
    }
}
