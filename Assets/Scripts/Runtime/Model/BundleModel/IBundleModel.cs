using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IBundleModel
{
    IPromise<InputActionAsset> AddressableLoadInputActionAsset(string inputActionAssetAdress);
    IPromise AddressableInstantiate(string addressableKey, Transform transform);
    IPromise<GameObject> AddressableInstantiateAndReach(string addressableKey, Transform transform);
}
