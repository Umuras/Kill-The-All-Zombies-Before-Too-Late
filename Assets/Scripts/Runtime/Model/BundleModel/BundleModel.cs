using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BundleModel : IBundleModel
{
    public IPromise AddressableInstantiate(string addressableKey, Transform transform)
    {
        Promise promise = new Promise();
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.InstantiateAsync(addressableKey,transform);

        asyncOperationHandle.Completed += handle =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                promise.Resolve();
            }
            else
            {
                promise.Reject(new Exception());
            }
        };

        return promise;
    }

    public IPromise<GameObject> AddressableInstantiateAndReach(string addressableKey, Transform transform)
    {
        Promise<GameObject> promise = new Promise<GameObject>();
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.InstantiateAsync(addressableKey, transform);

        asyncOperationHandle.Completed += handle =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                promise.Resolve(handle.Result);
            }
            else
            {
                promise.Reject(new Exception());
            }
        };

        return promise;
    }

    public IPromise<InputActionAsset> AddressableLoadInputActionAsset(string inputActionAssetAdress)
    {
        Promise<InputActionAsset> promise = new Promise<InputActionAsset>();

        AsyncOperationHandle<InputActionAsset> handle = Addressables.LoadAssetAsync<InputActionAsset>(inputActionAssetAdress);

        handle.Completed += asynchandle =>
        {
            if (asynchandle.Status == AsyncOperationStatus.Succeeded)
            {
                promise.Resolve(asynchandle.Result);
            }
            else
            {
                promise.Reject(new Exception());
            }
        };


        return promise;
    }
}
