using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private bool hovered;

    private GameObject item;
    private Texture itemIcon;
    private Item itemScript;
    private PlayerStats playerStats;

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
        if (item = null)
        {
            Debug.Log("No item to use");
            return;
        }
        if (itemScript.type == ItemType.Food || itemScript.type == ItemType.Water)
        {
            playerStats.Consume(itemScript.type, itemScript.statChange);
            item = null;
            itemIcon = null;
            this.GetComponent<RawImage>().texture = null;
            itemScript.Consumed();
            itemScript = null;
            Debug.Log("Item used");
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

    public void AddPlayer(PlayerStats pStats)
    {
        playerStats = pStats;
    }

    private void ConsumeItem()
    {

    }
}
