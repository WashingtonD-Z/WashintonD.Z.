using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Pickup system")]
    [SerializeField] private float maxPickupRange;
    [SerializeField] private LayerMask whatIsPickup;     
    [SerializeField] private KeyCode pickupKey = (KeyCode)KeyBinds.PickUpKey;
    [SerializeField] private KeyCode inventoryKey = (KeyCode)KeyBinds.InventoryKey;

    [Header("References")]
    public Canvas inventoryScreen;
    public GameObject slotHolder;
    public Transform playerCamera;
    public PlayerStats playerStats;
    public TMP_Text lightAmmoCounter;
    public TMP_Text pressF;

    
    private int slotCount;
    private Transform[] slots;
    
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        pressF.text = "Press " + pickupKey + " to pick up";
        slotCount = slotHolder.transform.childCount - 1;
        slots = new Transform[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i);
            slots[i].GetComponent<Slot>().AddPlayer(playerStats, playerCamera);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject pickup = LookForItem();
        if (pickup != null && Input.GetKeyDown(pickupKey))
        {
            AddItemToInventory(pickup);
        }

        if (!isOpened && Input.GetKeyDown(inventoryKey))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isOpened = true;
            inventoryScreen.enabled = true;
            playerCamera.GetComponent<PlayerCam>().enabled = false;
        }
        else if (isOpened && (Input.GetKeyDown(inventoryKey) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpened = false;
            inventoryScreen.enabled = false;
            playerCamera.GetComponent<PlayerCam>().enabled = true;
        }

        lightAmmoCounter.text = "Light Ammo: " + playerStats.lightAmmo;
         
    }

    private GameObject LookForItem()
    {
        RaycastHit raycastHit;
        Physics.Raycast(playerCamera.position, playerCamera.forward, out raycastHit, maxPickupRange, whatIsPickup);

        if (raycastHit.collider != null)
        {
            pressF.enabled = true;
            return raycastHit.transform.gameObject;
        }
        else
        {
            pressF.enabled = false;
            return null;
        }
    }

    public void AddItemToInventory(GameObject itemToAdd)
    {

        if (itemToAdd.TryGetComponent(out Ammo ammo))
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
            if (slots[i].GetComponent<Slot>().TryAddItem(itemToAdd))
            {
                break;
            }
        }
    }

    public List<GameObject> GetItemByType(ItemType type)
    {
        List<GameObject> itemList = new List<GameObject>();

        for (int i = 0; i < slotCount; i++)
        {
            GameObject item = slots[i].GetComponent<Slot>().item;
            if (item)
            {
                if (item.GetComponent<Item>().type == type)
                {
                    itemList.Add(item);
                }
            }
        }
        return itemList;
    }

    public void RemoveItemFromInventory(GameObject itemToRemove)
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject item = slots[i].GetComponent<Slot>().item;
            if (item)
            {
                if (item == itemToRemove)
                {
                    slots[i].GetComponent<Slot>().RemoveItem();
                }
            }
        }
    }
}
