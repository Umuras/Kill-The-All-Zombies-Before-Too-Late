using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMediator : EventMediator
{
    [Inject]
    public BulletView view { get; set; }
    [Inject]
    public IObjectPoolingModel objectPoolingModel { get; set; }


    public override void OnRegister()
    {
        base.OnRegister();
    }

    private void OnEnable()
    {
        Debug.LogWarning("I'm active");
        Invoke(nameof(EnqueueCheck),0.5f);
    }

    private void EnqueueCheck()
    {
        if (view.isCalledByPooling)
        {
            Invoke(nameof(Enqueue), 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    private void Enqueue()
    {
        view.isCalledByPooling = false;
        objectPoolingModel.EnqueuePoolObject(gameObject);
    }


    public override void OnRemove()
    {
        base.OnRemove();
        Debug.LogWarning("I'm deactive");
    }
}
