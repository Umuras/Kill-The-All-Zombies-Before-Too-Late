using DG.Tweening;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetModel : ITargetModel
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    [Inject]
    public IWeaponModel weaponModel { get; set; }
    [Inject]
    public IPlayerAndWeaponUIModel playerAndWeaponUIModel { get; set; }

    public int targetQuantity { get; set; }

    private float _standTurningZValue = -90f;

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
            targetView.gameObject.GetComponent<MeshCollider>().enabled = false;
            Vector3 parentRotation = targetView.gameObject.transform.parent.rotation.eulerAngles;

            targetView.transform.parent.DORotate(new Vector3(parentRotation.x, parentRotation.y, _standTurningZValue), 1f);
        }
        targetQuantity -= 1;
        playerAndWeaponUIModel.playerMissionLabel.text = "Mission: Destroy Targets\r\nTargets Quantity: " + targetQuantity;
        if (targetQuantity == 0)
        {
            Debug.Log("Mission Complete");
            dispatcher.Dispatch(PortalEvent.OpenPortal);
            playerAndWeaponUIModel.playerMissionLabel.text = "Mission: Walk to the Portal and enter the new area.";
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
