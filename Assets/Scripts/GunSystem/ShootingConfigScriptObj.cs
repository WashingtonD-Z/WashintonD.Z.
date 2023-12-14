using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootingConfig", menuName = "Guns/ShootingConfig", order = 2)]
public class ShootingConfigScriptObj : ScriptableObject 
{
    public LayerMask hitMask;
    [SerializeField] private float spread = 0.1f;
    public float fireRate = 0.25f;
    public float maxSpreadTime;
    public float recoveryTime = 1;
    public float recoilTime = 0.5f;

    public Vector3 GetSpread(float shootTime)
    {
        Vector3 spreadVector = Vector3.Lerp (
            Vector3.zero,
            new Vector3 (
                Random.Range (-spread, spread),
                Random.Range (-spread, spread),
                Random.Range (-spread, spread)
                ),
            Mathf.Clamp01(shootTime / maxSpreadTime)
        );

        return spreadVector;
    }
}

