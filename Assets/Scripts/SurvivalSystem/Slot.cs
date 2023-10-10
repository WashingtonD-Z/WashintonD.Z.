using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private bool hovered;
    private bool used;

    private GameObject item;
    private Texture itemIcon;

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
}
