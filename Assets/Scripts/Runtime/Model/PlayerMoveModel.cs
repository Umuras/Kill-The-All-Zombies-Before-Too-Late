using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveModel : IPlayerMoveModel
{
    public PlayerData playerData { get; set; }
    public Transform orientation { get; set; }

    private string playerDataPath = "Data/CD_Player";

    private float _horizontalInput;
    private float _verticalInput;

    private bool _readyToJump;
    private bool _grounded;

    private Vector3 _moveDirection;

    private MonoBehaviour mono = new MonoBehaviour();

    [PostConstruct]
    public void GetPlayerData()
    {
        playerData = Resources.Load<CD_Player>(playerDataPath).playerData;
    }

    public void InputPlayer(Rigidbody rigidbody, Transform transform)
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && _readyToJump && _grounded)
        {
            _readyToJump = false;

            Jump(rigidbody, transform);

            mono.Invoke(nameof(ResetJump), playerData.MovementData.jumpCooldown);
        }

    }

   

    public void MovePlayer(Rigidbody rb, Transform orientation)
    {
        //calculate movement direction
        _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;

        //on ground
        if (_grounded)
        {
            rb.AddForce(_moveDirection.normalized * playerData.MovementData.moveSpeed * 10f, ForceMode.Force);
        } //in air
        else if (!_grounded)
        {
            rb.AddForce(_moveDirection.normalized * playerData.MovementData.moveSpeed * 10f * playerData.MovementData.airMultiplier, ForceMode.Force);
        }
    }

    public void SpeedControl(Rigidbody rigidbody)
    {
        Vector3 flatVelocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);

        //limit velocity if needed
        if (flatVelocity.magnitude > playerData.MovementData.moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * playerData.MovementData.moveSpeed;
            rigidbody.velocity = new Vector3(limitedVelocity.x, rigidbody.velocity.y, limitedVelocity.z);
        }
    }

    public void Jump(Rigidbody rigidbody, Transform transform)
    {
        //reset y velocity
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);

        rigidbody.AddForce(transform.up * playerData.MovementData.jumpForce, ForceMode.Impulse);
    }

    public void ResetJump()
    {
        _readyToJump = true;
    }

    public void GroundControl(Transform transform)
    {
        _grounded = Physics.Raycast(transform.position, Vector3.down, playerData.groundData.playerHeight * 0.5f + 0.2f, playerData.groundData.whatIsGround);
    }

    public void HandleDrag(Rigidbody rigidbody)
    {
        if (_grounded)
        {
            rigidbody.drag = playerData.MovementData.groundDrag;
        }
        else
        {
            rigidbody.drag = 0;
        }
    }
}
