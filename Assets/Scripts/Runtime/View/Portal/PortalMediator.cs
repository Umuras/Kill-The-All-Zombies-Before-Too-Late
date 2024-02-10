using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PortalEvent
{
    OpenPortal
}

public class PortalMediator : EventMediator
{
    [Inject]
    public PortalView view { get; set; }
    [Inject]
    public IBundleModel bundleModel { get; set; }
    [Inject]
    public IGameAreaModel gameAreaModel { get; set; }

    public override void OnRegister()
    {
        dispatcher.AddListener(PortalEvent.OpenPortal, OnOpenPortal);
        Init();
    }

    private void Init()
    {
        gameObject.SetActive(false);
    }

    private void OnOpenPortal()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            bundleModel.AddressableInstantiate("NewLevel", gameAreaModel.GameAreaTransform).Then(() =>
            {
                CharacterController playerCharacterController = other.gameObject.GetComponent<CharacterController>();
                playerCharacterController.enabled = false;
                //NewLevel Center Position
                playerCharacterController.gameObject.transform.position = new Vector3(53.05114f, 0, 39.10672f);
                playerCharacterController.enabled = true;
                Destroy(transform.parent.gameObject);
            });
        }
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(PortalEvent.OpenPortal, OnOpenPortal);
    }
}
