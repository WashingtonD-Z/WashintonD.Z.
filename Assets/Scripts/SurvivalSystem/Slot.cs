using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private bool hovered;

    private GameObject item;
    private Texture itemIcon;
    private Item itemScript;

    // Start is called before the first frame update
    void Start()
    {
        hovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (item)
        {
            this.GetComponent<RawImage>().texture = itemIcon;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void  OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
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
        //itemScript.
        return true;
    }
}
