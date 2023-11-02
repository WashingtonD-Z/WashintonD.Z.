using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Respawn : MonoBehaviour, IPointerClickHandler
{

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
        SceneManager.LoadScene((SceneManager.GetActiveScene()).buildIndex);
    }

}
