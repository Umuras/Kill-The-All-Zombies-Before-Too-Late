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
            Debug.Log("I am in the PORTALLLLL!!!!");
        }
    }

    public override void OnRemove()
    {
        dispatcher.RemoveListener(PortalEvent.OpenPortal, OnOpenPortal);
    }
}
