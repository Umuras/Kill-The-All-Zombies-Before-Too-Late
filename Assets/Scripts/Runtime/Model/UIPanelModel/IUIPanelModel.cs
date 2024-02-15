using RSG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIPanelModel
{
    List<Transform> layers { get; set; }

    bool isOpenPanel { get; set; }
    IPromise OpenPanel(int layerIndex, string addressable);

    void ClosePanel(int layerIndex);

    void AllClosePanels();
}
