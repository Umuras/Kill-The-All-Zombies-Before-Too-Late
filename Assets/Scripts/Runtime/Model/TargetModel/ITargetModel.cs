using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetModel
{
    int targetQuantity { get; set; }
    void DecrasingTargetHealthAndKillTarget(TargetView targetView);
}
