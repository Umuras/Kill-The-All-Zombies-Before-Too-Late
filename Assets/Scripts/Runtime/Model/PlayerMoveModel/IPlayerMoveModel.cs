using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMoveModel
{
    Transform orientation { get; set; }

    PlayerData playerData { get; set; }

    void MovePlayer(Rigidbody rb, Transform orientation);

    void InputPlayer(Rigidbody rigidbody, Transform transform);

    void SpeedControl(Rigidbody rigidbody);
    void Jump(Rigidbody rigidbody, Transform transform);
    void ResetJump();

    void GroundControl(Transform transform);

    void HandleDrag(Rigidbody rigidbody);
}
