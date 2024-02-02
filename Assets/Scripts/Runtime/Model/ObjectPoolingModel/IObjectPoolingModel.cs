using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolingModel
{
    Transform poolParent { get; set; }
    Queue<GameObject> poolableObjectList { get; set; }
    void CreatePool(GameObject poolGameObject, int poolObjectQuantity);
    void EnqueuePoolObject(GameObject poolObject);
    GameObject DequeuePoolableGameObject();
}
