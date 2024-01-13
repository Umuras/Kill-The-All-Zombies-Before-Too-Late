using RSG;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public interface IBundleModel
{
    //IPromise<T> AddressableLoad<T>(string addressableKey);
    IPromise AddressableInstantiate(string addressableKey, Transform transform);
}
