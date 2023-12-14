using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerGunSelector : MonoBehaviour
{
    [SerializeField]
    private GunType gunType;
    [SerializeField]
    private Transform gunParent;
    [SerializeField]
    private List<GunScriptObj> guns;

    [Space]
    [Header("Filled during runtime")]
    public GunScriptObj activeGun;
    // Start is called before the first frame update
    void Start()
    {
        GunScriptObj gun = guns.Find(gun => gun.type == gunType);
        if (gun == null)
        {
            Debug.LogError($"No GunSriptObj found for Guntype: {gun}");
            return;
        }

        activeGun = gun;
        gun.Spawn(gunParent, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
