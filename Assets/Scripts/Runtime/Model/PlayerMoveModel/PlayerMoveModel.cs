using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveModel : IPlayerMoveModel
{
    public InputActionAsset inputActions { get; set; }
    public PlayerData playerData { get; set; }
    public Transform orientation { get; set; }

    public CharacterController characterController { get; set; }

    public Transform groundCheck { get; set; }

    private string playerDataPath = "Data/CD_Player";

    private Vector2 _moveVector;
    private Vector3 _velocity;

    private bool _grounded;
    private bool _enterPlayer;

    private Vector3 _moveDirection;

    private string _inputMoveAction = "Move";
    private string _inputJumpAction = "Jump";

    private float _gravity = -9.8f;
    private int _mass = 10;

    [PostConstruct]
    public void GetPlayerData()
    {
        playerData = Resources.Load<CD_Player>(playerDataPath).playerData;
    }

    public void InputPlayer()
    {
        //Yeni Input Sistemindeki Player mapi üzerinden Move actiona eriþiyoruz
        _moveVector = inputActions.FindAction(_inputMoveAction).ReadValue<Vector2>();


        if (inputActions.FindAction(_inputJumpAction).triggered && _grounded)
        {
            Jump();
        }

    }

    public void MovePlayer(Transform transform)
    {
        //Karakterimizin hareket yönü belirlenmiþ oluyor.
        _moveDirection = transform.right * _moveVector.x + transform.forward * _moveVector.y;

        characterController.Move(_moveDirection * playerData.MovementData.moveSpeed * Time.deltaTime);
    }

    public void Gravity()
    {
        if (_velocity.y < 0 && characterController.isGrounded)
        {
            _velocity.y = -1;
        }
    }

    public void GravityForce()
    {
        _velocity.y += _gravity * _mass * Time.deltaTime;
        characterController.Move(_velocity * Time.deltaTime);
    }

    public void GroundControl()
    {
        _grounded = characterController.isGrounded;
    }

    public void Jump()
    {
        _velocity.y += Mathf.Sqrt(_gravity * -2 * playerData.MovementData.jumpForce);
    }

    public bool ThrowRaycast(Transform orientaion)
    {
        Ray ray = new Ray(orientation.transform.position, orientation.forward);

        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);

        if (Physics.Raycast(ray,out hit,5f))
        {
            if (hit.collider.gameObject.CompareTag("TreasureChest"))
            {
                float distance = Vector3.Distance(orientation.transform.position, hit.collider.transform.position);
                
                if (distance <= 2)
                {
                    _enterPlayer = true;
                    Debug.LogError(_enterPlayer);
                    return _enterPlayer;
                }
                else
                {
                    _enterPlayer = false;
                    Debug.LogError(_enterPlayer);
                    return _enterPlayer;
                }
            }
        }
        return false;
    }
}
