using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float ammoCount;
    public AmmoType type;
    [SerializeField] private GameObject physicalItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUp()
    {
        Destroy(physicalItem.gameObject);
    }
}
