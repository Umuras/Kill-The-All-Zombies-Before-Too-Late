using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxView : View
{
    public string playerTag = "Player";
    public KeyCode keyCode = KeyCode.E;
    public bool bouncingBox = true;
    public bool closeOnExit;
    public Animator animator;
    private string bounceAnimationParameter = "bounce";

    protected override void Start()
    {
        BounceBox(bouncingBox);
    }

    private void BounceBox(bool bounceIt)
    {
        if (animator && bounceIt)
        {
            animator.SetBool(bounceAnimationParameter, bounceIt);
        }
    }
}
