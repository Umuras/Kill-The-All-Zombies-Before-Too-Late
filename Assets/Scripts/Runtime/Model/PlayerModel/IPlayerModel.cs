using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerModel
{
    int playerHealth { get; set; }
    GameObject player { get; set; }
}
