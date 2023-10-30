using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Pickup system")]
    [SerializeField] private float maxPickupRange;
    [SerializeField] private LayerMask whatIsPickup;     
    private GameObject pickup;
    [SerializeField] private KeyCode pickupKey = KeyCode.F;

    [Header("References")]
    public Canvas inventoryScreen;
    public GameObject slotHolder;
    public Transform playerCamera;
    public PlayerStats playerStats;

    
    private int slotCount;
    private Transform[] slots;
    
    private bool isOpened;
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
        pickup = LookForItem();
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
        for (int i = 0; i < slotCount; i++)
        {
            if (slots[i].GetComponent<Slot>().TryAddItem(pickup))
            {
                break;
            }
        }
    } 
}
