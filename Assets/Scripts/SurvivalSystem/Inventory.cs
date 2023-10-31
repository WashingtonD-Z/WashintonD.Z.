using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Pickup system")]
    [SerializeField] private float maxPickupRange;
    [SerializeField] private LayerMask whatIsPickup;     
    [SerializeField] private KeyCode pickupKey = KeyCode.F;

    [Header("References")]
    public Canvas inventoryScreen;
    public GameObject slotHolder;
    public Transform playerCamera;
    public PlayerStats playerStats;
    public TMP_Text lightAmmoCounter;

    
    private int slotCount;
    private Transform[] slots;
    
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        slotCount = slotHolder.transform.childCount;
        slots = new Transform[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i);
            slots[i].GetComponent<Slot>().AddPlayer(playerStats);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject pickup = LookForItem();
        if (pickup != null && Input.GetKeyDown(pickupKey))
        {
            PickupItem(pickup);
        }

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

        lightAmmoCounter.text = "Light Ammo: " + playerStats.lightAmmo;
         
    }

    private GameObject LookForItem()
    {
        RaycastHit raycastHit;
        Physics.Raycast(playerCamera.position, playerCamera.forward, out raycastHit, maxPickupRange, whatIsPickup);

        if (raycastHit.collider != null)
        {
            return raycastHit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void PickupItem(GameObject pickup)
    {

        if (pickup.TryGetComponent(out Ammo ammo))
        {
            if (ammo.type == AmmoType.LightAmmo)
            {
                playerStats.lightAmmo += ammo.ammoCount;
                ammo.PickedUp();
                return;
            }
        }
        for (int i = 0; i < slotCount; i++)
        {
            if (slots[i].GetComponent<Slot>().TryAddItem(pickup))
            {
                break;
            }
        }
    }

    public List<Item> getItemByType(ItemType type)
    {
        List<Item> itemList = new List<Item>();

        for (int i = 0; i < slotCount; i++)
        {
            slots[i].GetComponent<Slot>().TryGetComponent(out Item item);
            if (item)
            {
                if (item.type == type)
                {
                    itemList.Add(item);
                }
            }
        }
        return itemList;
    }
}
