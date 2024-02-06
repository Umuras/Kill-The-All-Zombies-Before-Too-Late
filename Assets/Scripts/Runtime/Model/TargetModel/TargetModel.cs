using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetModel : ITargetModel
{
    [Inject]
    public IWeaponModel weaponModel { get; set; }

    private float _standTurningZValue = -90f;

    private float _damageTextMoveDistance = 0.5f;
    private float _duration = 2f;

    public void DecrasingTargetHealthAndKillTarget(TargetView targetView)
    {
        if (targetView.targetHealth > 0)
        {   
            if (weaponModel.weaponIndex == (int)WeaponKeys.Pistol)
            {
                targetView.targetHealth -= weaponModel.pistolDamagePower;
                //if (targetView.IsStandTarget)
                //{
                //    GameObject damagePopUpGo = CreateDamagePopUpText(targetView);
                //    ShowDamagePower(targetView, weaponModel.pistolDamagePower, damagePopUpGo);
                //}
            }
            else if (weaponModel.weaponIndex == (int)WeaponKeys.Rifle)
            {
                targetView.targetHealth -= weaponModel.rifleDamagePower;
                //if (targetView.IsStandTarget)
                //{
                //    GameObject damagePopUpGo = CreateDamagePopUpText(targetView);
                //    ShowDamagePower(targetView, weaponModel.rifleDamagePower, damagePopUpGo);
                //}
            }

            if (targetView.targetHealth <= 0)
            {
                KillTargets(targetView);
            }
        }
        else
        {
            KillTargets(targetView);
        }    
    }

    private void KillTargets(TargetView targetView)
    {
        if (!targetView.IsStandTarget)
        {
            targetView.gameObject.SetActive(false);
        }
        else
        {
            Vector3 parentRotation = targetView.gameObject.transform.parent.rotation.eulerAngles;

            targetView.transform.parent.DORotate(new Vector3(parentRotation.x, parentRotation.y, _standTurningZValue), 1f);
        }
    }

    private void ShowDamagePower(TargetView targetView, int weaponDamagePower, GameObject damagePopUpTextGo)
    {
        targetView.damageText.text = weaponDamagePower.ToString();
        targetView.damageText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
        {
            targetView.damageText.DOFade(0, 35f).SetDelay(0.35f);
            targetView.damageText.rectTransform.DOAnchorPos3DY(1f, 0.65f).SetEase(Ease.Linear);
            Object.Destroy(damagePopUpTextGo, 1);
        });
    }

    private GameObject CreateDamagePopUpText(TargetView targetView)
    {
        GameObject damagePopUpText = Object.Instantiate(targetView.damageTextGo,
            targetView.transform);
        return damagePopUpText;
    }
}
