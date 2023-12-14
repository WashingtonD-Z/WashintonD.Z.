using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Texture icon;
    [SerializeField] private Transform physicalItem;
    public ItemType type;
    //ItemStat means the relevant stat for the type, For food its how much it restores, for mags its how many bullets they can hold.
    public float maxItemStat;
    public float currentItemStat;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUp()
    {
        physicalItem.GetComponent<Collider>().enabled = false;
        physicalItem.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Dropped()
    {
        physicalItem.GetComponent<Collider>().enabled = true;
        physicalItem.GetComponent<MeshRenderer>().enabled = true;
    }

    public void Consumed()
    {
        Destroy(physicalItem.gameObject);
    }

    public float AddAmmo(float Bullets)
    {
        float missingAmmo = maxItemStat - currentItemStat;
        if (Bullets > missingAmmo)
        {
            Bullets -= missingAmmo;
            currentItemStat = maxItemStat;
            return Bullets;
        }
        else
        {
            currentItemStat += Bullets;
            return 0;
        }
    }

    public bool TryRemoveAmmo()
    {
        if (currentItemStat <= 0)
        {
            return false;
        }
        currentItemStat -= 1;
        return true;
    }
}
