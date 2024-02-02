using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingModel : IObjectPoolingModel
{
    public Transform poolParent { get; set; }
    public Queue<GameObject> poolableObjectList { get; set; }

    public void CreatePool(GameObject poolGameObject, int poolObjectQuantity)
    {
        poolableObjectList = new Queue<GameObject>();
        for (int i = 0; i < poolObjectQuantity; i++)
        {
            GameObject poolObject = Object.Instantiate(poolGameObject, poolParent.transform, true);
            poolObject.SetActive(false);

            poolableObjectList.Enqueue(poolObject);
        }
    }

    public void EnqueuePoolObject(GameObject poolObject)
    {
        //Mouse sol t�ka bas�l� tutuldu�unda �ok h�zl� bir �ekilde dequu i�lemi oldu�u i�in 
        //Orada da enque i�lemi yapamad��� h�z�na yeti�emeyip kuyruk doldurulamadan deque yapmaya kalk�nca hata veriyor.
        //Kuyruktan ��kar�lm�� obje tekrar kuyru�a eklenip transform de�erleri s�f�rlan�p
        //aktifli�i kapat�l�yor.
        poolObject.transform.parent = poolParent.transform;
        poolObject.transform.localPosition = Vector3.zero;
        poolObject.transform.localEulerAngles = Vector3.zero;

        poolObject.gameObject.SetActive(false);

        poolableObjectList.Enqueue(poolObject);
    }


    public GameObject DequeuePoolableGameObject()
    {
        GameObject dequeuedPoolObject = poolableObjectList.Dequeue();
        //E�er kuyruktan �ekti�imiz GameObject aktif ise tekrar �ekiyoruz.
        if (dequeuedPoolObject.activeSelf)
        {
            DequeuePoolableGameObject();
        }
        dequeuedPoolObject.SetActive(true);
        return dequeuedPoolObject;
    }
}
