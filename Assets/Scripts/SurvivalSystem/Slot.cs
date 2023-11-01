using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private bool hovered;

    public GameObject item;
    private Texture itemIcon;
    private Item itemScript;
    private PlayerStats playerStats;
    private Transform playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        hovered = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void  OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item == null)
            {
                Debug.Log("No item to use");
                return;
            }
            if (itemScript.type == ItemType.Food || itemScript.type == ItemType.Water)
            {
                playerStats.Consume(itemScript.type, itemScript.currentItemStat);
                item = null;
                itemIcon = null;
                this.GetComponent<RawImage>().texture = null;
                itemScript.Consumed();
                itemScript = null;
                Debug.Log("Item used");
            }
            if (itemScript.type == ItemType.Mag)
            {
                float ammoLeft = itemScript.AddAmmo(playerStats.lightAmmo);
                playerStats.lightAmmo = ammoLeft;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
        
    }

    public bool TryAddItem(GameObject itemToAdd)
    {
        if (item != null)
        {
            return false;
        }
        item = itemToAdd;
        itemScript = itemToAdd.GetComponent<Item>();
        itemIcon = itemScript.icon;
        itemScript.PickedUp();
        this.GetComponent<RawImage>().texture = itemIcon;
        return true;
    }

    public void AddPlayer(PlayerStats pStats, Transform camera)
    {
        playerStats = pStats;
        playerCamera = camera;
    }

    public void RemoveItem()
    {
        item = null;
        itemIcon = null;
        itemScript = null;
        this.GetComponent<RawImage>().texture = itemIcon;
    }

    public void DropItem()
    {
        item.transform.position = new Vector3(playerCamera.position.x, playerCamera.position.y, playerCamera.position.z);
        item.transform.position += playerCamera.forward * 2f;
        itemScript.Dropped();
        RemoveItem();
    }
}
