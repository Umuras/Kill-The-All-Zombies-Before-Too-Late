using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerMoveModel
{
    CharacterController characterController { get; set; }
    Transform orientation { get; set; }

    Transform groundCheck { get; set; }

    PlayerData playerData { get; set; }

    void MovePlayer(Transform transform);

    void InputPlayer();

    void Jump();

    void GroundControl();

    void Gravity();

    void GravityForce();

    bool ThrowRaycast(Transform orientaion);
}
