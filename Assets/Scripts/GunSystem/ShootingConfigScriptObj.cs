using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootingConfig", menuName = "Guns/ShootingConfig", order = 2)]
public class ShootingConfigScriptObj : ScriptableObject 
{
    public LayerMask hitMask;
    public Vector3 spread = new Vector3(0.1f,0.1f,0.1f);
    public float fireRate = 0.25f;
}

