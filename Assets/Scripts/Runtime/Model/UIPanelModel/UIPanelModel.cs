using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelModel : IUIPanelModel
{
    [Inject]
    public IBundleModel bundleModel { get; set; }

    public List<Transform> layers { get; set; }

    public void OpenPanel(int layerIndex, string addressable)
    {
        ClosePanel(layerIndex);

        bundleModel.AddressableInstantiate(addressable, layers[layerIndex].transform);
    }

    public void ClosePanel(int layerIndex)
    {
        if (layers[layerIndex].childCount > 0)
        {
            for (int i = 0; i < layers[layerIndex].childCount; i++)
            {
                Object.Destroy(layers[layerIndex].transform.GetChild(i).gameObject);
            }
        }
    }

    public void AllClosePanels()
    {
        foreach (Transform layer in layers)
        {
            if (layer.childCount > 0)
            {
                for (int i = 0; i < layer.childCount; i++)
                {
                    Object.Destroy(layer.transform.GetChild(i).gameObject);
                }
            }
        }
    }

   
}