using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HowToPlay : MonoBehaviour, IPointerClickHandler
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
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MainMenu.SetActive(false);
        HowToPlayMenu.SetActive(true);
    }
}
