using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoConfigScriptObj", menuName = "Guns/Ammo Config", order = 0)]
public class AmmoConfig : ScriptableObject 
{
    [SerializeField] private Item mag = null;
    [SerializeField] private GameObject magObj = null;
    

    public bool CheckIfCanShoot()
    {
        if (mag != null)
        {
            return mag.TryRemoveAmmo();
        }
        return false;
    }

    public GameObject ReloadMag(GameObject newMag)
    {
        if (newMag == null)
        {
            GameObject Mag = magObj;
            mag = null;
            magObj = null;
            return Mag;
        }
        if (mag == null)
        {
            magObj = newMag;
            mag = magObj.GetComponent<Item>();
            return null;
        }
        GameObject oldMag = magObj;
        magObj = newMag;
        mag = magObj.GetComponent<Item>();
        return oldMag;
    }
}