using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Canvas inventoryScreen;
    public GameObject slotHolder;

    private int slotCount;
    private Transform[] slots;
    
    private bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        slotCount = slotHolder.transform.childCount;

        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened && Input.GetKeyDown(KeyCode.I))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isOpened = true;
            inventoryScreen.enabled = true;
        }
        else if (isOpened && Input.GetKeyDown(KeyCode.I))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpened = false;
            inventoryScreen.enabled = false;
        }

    }


    public void pickupItem()
    {

    } 
}
