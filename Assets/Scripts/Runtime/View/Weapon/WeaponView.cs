using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponView : EventView
{
    public List<GameObject> weaponList;
    public List<Transform> weaponMuzzleTransform;
    public AudioSource fireAudioSource;
    public AudioSource reloadAudioSource;
    public Animation fireAnimation;
}
