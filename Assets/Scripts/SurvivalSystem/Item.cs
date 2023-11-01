using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Texture icon;
    [SerializeField] private Transform physicalItem;
    public ItemType type;
    public float statChange;

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

    public void Consumed()
    {
        Destroy(physicalItem.gameObject);
    }
}
