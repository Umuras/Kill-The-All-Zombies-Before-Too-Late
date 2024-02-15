using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerAndWeaponUIView : EventView
{
    public TextMeshProUGUI healtText;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI statusLabel;
    public Image weaponCrossHair;
    public TextMeshProUGUI playerMissionLabel;
    public TextMeshProUGUI gameTimeLabel;
    public TextMeshProUGUI multiplyTwoDamageLabel;
    public TextMeshProUGUI multiplyTwoSpeedLabel;
    [HideInInspector]
    public int gameStartInitialTime = 60;
}
