using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class WeaponMediator : EventMediator
{
    [Inject]
    public WeaponView view { get; set; }
    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }

    public override void OnRegister()
    {
        Cursor.lockState = CursorLockMode.Locked;
        view.weaponIndex = 0;
        objectPoolingModel.CreatePool(weaponModel.weaponData[view.weaponIndex].weapon.bullet, 20);
    }

    private void FixedUpdate()
    {
        if (weaponModel != null)
        {
            weaponModel.RaycastForWeapon(view.weaponMuzzleTransform[view.weaponIndex]);
        }

        if (Input.GetMouseButton(0))
        {
            weaponModel.FireBullet(view.weaponMuzzleTransform[view.weaponIndex], WeaponKeys.Pistol);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            view.weaponList[view.weaponIndex].SetActive(false);
            view.weaponIndex = 0;
            view.weaponList[view.weaponIndex].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            view.weaponList[view.weaponIndex].SetActive(false);
            view.weaponIndex = 1;
            view.weaponList[view.weaponIndex].SetActive(true);
        }

        //if (view.weaponIndex == (int)WeaponKeys.Pistol)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        foreach (WeaponData weaponData in weaponModel.weaponData)
        //        {
        //            if (weaponData.weapon.weaponName == WeaponKeys.Pistol.ToString())
        //            {
        //                weaponData.weapon.muzzle.SetActive(true);
        //            }
                    
        //        }
        //    }
        //}
    }

}
