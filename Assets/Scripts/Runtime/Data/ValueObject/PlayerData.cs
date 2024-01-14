using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public PlayerMovementData MovementData;
}
[Serializable]
public struct PlayerMovementData
{
    public float ForwardSpeed;
    public float JumpForce;
}