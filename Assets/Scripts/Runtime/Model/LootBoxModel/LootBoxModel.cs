using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class LootBoxModel : ILootBoxModel
{
    public bool closeOnExit { get; set; }
    private bool isOpen { get; set; }

    public bool isPlayerAround { get; set; }

    private string openAnimatorState = "Open";

    private string closeAnimatorState = "Close";

    public void Open(Animator animator)
    {
        if (isOpen)
        {
            return;
        }
        isOpen = true;

        if (animator)
        {
            animator.Play(openAnimatorState);
        }
    }

    private void Close(Animator animator)
    {
        if (!isOpen)
        {
            return;
        }
        isOpen = false;

        if (animator)
        {
            animator.Play(closeAnimatorState);
        }
    }

    public void EnterPlayerLootBoxArea()
    {
       isPlayerAround = true;     
    }

    public void ExitPlayerLootBoxArea()
    {
        isPlayerAround = false;
    }

    public void CloseBoxOperation(Animator animator)
    {
        if (closeOnExit)
        {
            Close(animator);
        }
    }
}
