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
        //Mouse sol týka basýlý tutulduðunda çok hýzlý bir þekilde dequu iþlemi olduðu için 
        //Orada da enque iþlemi yapamadýðý hýzýna yetiþemeyip kuyruk doldurulamadan deque yapmaya kalkýnca hata veriyor.
        //Kuyruktan çýkarýlmýþ obje tekrar kuyruða eklenip transform deðerleri sýfýrlanýp
        //aktifliði kapatýlýyor.
        poolObject.transform.parent = poolParent.transform;
        poolObject.transform.localPosition = Vector3.zero;
        poolObject.transform.localEulerAngles = Vector3.zero;

        poolObject.gameObject.SetActive(false);

        poolableObjectList.Enqueue(poolObject);
    }


    public GameObject DequeuePoolableGameObject()
    {
        GameObject dequeuedPoolObject = poolableObjectList.Dequeue();
        //Eðer kuyruktan çektiðimiz GameObject aktif ise tekrar çekiyoruz.
        if (dequeuedPoolObject.activeSelf)
        {
            DequeuePoolableGameObject();
        }
        dequeuedPoolObject.SetActive(true);
        return dequeuedPoolObject;
    }
}
