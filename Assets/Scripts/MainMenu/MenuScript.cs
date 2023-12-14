using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject HowToPlayMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HowToPlayMenu.SetActive(false);
            MainMenu.SetActive(true);
        }
    }
}
