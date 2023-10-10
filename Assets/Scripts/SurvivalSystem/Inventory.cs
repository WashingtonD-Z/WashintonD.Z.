using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Canvas canvas;
    private bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOpened && Input.GetKeyDown(KeyCode.I))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            isOpened = true;
            canvas.enabled = true;
        }
        else if (isOpened && Input.GetKeyDown(KeyCode.I))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isOpened = false;
            canvas.enabled = false;
        }

    }
}
