using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveView : EventView
{
    public Transform orientation;
    public CharacterController characterController;
    public InputActionAsset inputActions;
    public bool enterPlayer;
}
