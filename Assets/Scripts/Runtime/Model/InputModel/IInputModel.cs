using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputModel
{
    InputActionAsset playerController { get; set; }
}
