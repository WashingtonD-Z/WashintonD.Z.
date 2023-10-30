using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "DamageConfig", menuName = "Guns/DamageConfig", order = 4)]
public class DamageConfig : ScriptableObject 
{
    public MinMaxCurve damageCurve;

    private void Reset()
    {
        damageCurve.mode = ParticleSystemCurveMode.Curve;
    }

    public int GetDamage(float Distance = 0)
    {
        return Math.CeilToInt(damageCurve.Evaluate(Distance, Random.value));
    }
}
