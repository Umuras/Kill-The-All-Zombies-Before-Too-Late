using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIPanelModel
{
    void OpenPanel(int layerIndex, string addressable);

    void ClosePanel(int layerIndex);

    void AllClosePanels();

    List<Transform> layers { get; set; }
}
