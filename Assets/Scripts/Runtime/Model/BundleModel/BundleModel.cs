using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

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

    //public IPromise<T> AddressableLoad<T>(string addressableKey)
    //{
    //    Promise promise = new Promise();

    //    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(addressableKey);

    //    handle.Completed += asynchandle =>
    //    {
    //        if (asynchandle.Status == AsyncOperationStatus.Succeeded)
    //        {
    //            promise.Resolve();
    //        }
    //        else
    //        {
    //            promise.Reject(new Exception());
    //        }
    //    };


    //    return (IPromise<T>)promise;
    //}
}
