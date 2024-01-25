using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerData
{
    public PlayerMovementData MovementData;
    public PlayerGroundData groundData;
}
[Serializable]
public struct PlayerMovementData
{
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
}

[Serializable]
public struct PlayerGroundData
{
    public float playerHeight;
    public LayerMask whatIsGround;
}