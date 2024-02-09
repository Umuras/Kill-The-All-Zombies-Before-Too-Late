using strange.extensions.mediation.impl;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;

public enum WeaponEvent
{
    DecreasingAmmo,
    AmmoFinished
}

public class WeaponMediator : EventMediator
{
    [Inject]
    public WeaponView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public override void OnRegister()
    {
        Cursor.lockState = CursorLockMode.Locked;
        weaponModel.InitializeWeaponsProperties(fireAudioSource: view.fireAudioSource, reloadAudioSource: view.reloadAudioSource, 
            fireAnimation: view.fireAnimation, firstWeapon: WeaponKeys.Pistol, weaponMuzzleTransform: view.weaponMuzzleTransform, 
            pistolParticle: view.pistolParticleSystem, rifleParticle: view.rifleParticleSystem);
        //weaponModel.weaponIndex = 0;
        //view.fireAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.fire;
        //view.reloadAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.realod;
        //view.fireAnimation.AddClip(weaponModel.weaponData[0].weapon.fireAnimationClip, WeaponKeys.Pistol.ToString());
        //view.fireAnimation.AddClip(weaponModel.weaponData[1].weapon.fireAnimationClip, WeaponKeys.Rifle.ToString());
    }

    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (!weaponModel.reloading)
                {
                    weaponModel.ChangeWeapon(view.weaponList, WeaponKeys.Pistol, view.fireAnimation, view.reloadAudioSource);
                }

                //view.weaponList[weaponModel.weaponIndex].SetActive(false);
                //weaponModel.weaponIndex = 0;
                //view.weaponList[weaponModel.weaponIndex].SetActive(true);
                //playerAndWeaponUIModel.ChangeWeaponAmmoText(weaponModel.totalPistolMagInside, weaponModel.totalPistolAmmo);
                //view.fireAnimation.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.fireAnimationClip;
                //view.reloadAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.realod;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (!weaponModel.reloading)
                {
                    weaponModel.ChangeWeapon(view.weaponList, WeaponKeys.Rifle, view.fireAnimation, view.reloadAudioSource);
                }
                //view.weaponList[weaponModel.weaponIndex].SetActive(false);
                //weaponModel.weaponIndex = 1;
                //view.weaponList[weaponModel.weaponIndex].SetActive(true);
                //playerAndWeaponUIModel.ChangeWeaponAmmoText(weaponModel.totalRifleMagInside, weaponModel.totalRifleAmmo);
                //view.fireAnimation.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.fireAnimationClip;
                //view.reloadAudioSource.clip = weaponModel.weaponData[weaponModel.weaponIndex].weapon.realod;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                weaponModel.Reload(view.reloadAudioSource);
            }

            if (!weaponModel.reloading)
            {
                dispatcher.Dispatch(FireWeaponEvent.FIREWEAPON);
                //if (Input.GetMouseButtonDown(0))
                //{
                //    if (weaponModel.totalPistolMagInside > 0)
                //    {
                //        if (!view.fireAnimation.isPlaying)
                //        {
                //            playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalPistolMagInside);
                //            weaponModel.RaycastForWeapon(view.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.pistolShootRange);
                //            view.pistolParticleSystem.Play();
                //            view.fireAnimation.Play(WeaponKeys.Pistol.ToString());
                //            view.fireAudioSource.Play();
                //        }
                //    }
                //}


                //if (Input.GetMouseButton(0))
                //{
                //    if (weaponModel.totalRifleMagInside > 0)
                //    {
                //        if (!view.fireAnimation.isPlaying)
                //        {
                //            playerAndWeaponUIModel.DecreasingAmmo(weaponModel.weaponIndex, --weaponModel.totalRifleMagInside);
                //            weaponModel.RaycastForWeapon(view.weaponMuzzleTransform[weaponModel.weaponIndex], weaponModel.rifleShootRange);
                //            view.rifleParticleSystem.Play();
                //            view.fireAnimation.Play(WeaponKeys.Rifle.ToString());
                //            view.fireAudioSource.Play();
                //        }
                //    }
                //}
            }
    }
}
