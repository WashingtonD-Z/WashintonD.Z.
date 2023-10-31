using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoConfigScriptObj", menuName = "Guns/Ammo Config", order = 0)]
public class AmmoConfig : ScriptableObject 
{
    [SerializeField] private Item mag = null;
    

    public bool CheckIfCanShoot()
    {
        if (mag != null)
        {
            return mag.TryRemoveAmmo();
        }
        return false;
    }

    public Item ReloadMag(Item newMag)
    {
        Item oldMag = mag;
        mag = newMag;
        return oldMag;
    }
}