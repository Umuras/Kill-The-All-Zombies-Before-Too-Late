using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILootBoxModel
{
    public bool closeOnExit { get; set; }

    void EnterPlayerLootBoxArea();

    void ExitPlayerLootBoxArea();

    bool isPlayerAround { get; set; }

    void CloseBoxOperation(Animator animator);

    void Open(Animator animator);
}