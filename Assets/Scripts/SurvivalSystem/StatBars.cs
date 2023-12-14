using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatBars : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private TMP_Text healthHud;
    [SerializeField] private TMP_Text hungerHud;
    [SerializeField] private TMP_Text ammoHud;
    [SerializeField] private TMP_Text magHud;
    [SerializeField] private TMP_Text reloadReminder;
    [SerializeField] private PlayerGunSelector playerGun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthHud.text = Mathf.Round(stats.currentHealth) + "/" + stats.maxHealth;
        hungerHud.text = Mathf.Round(stats.hunger) + "/" + stats.maxHunger;
        if (playerGun.activeGun.ammoConfig.mag)
        {
            magHud.text = "Magazine Loaded";
            ammoHud.text = playerGun.activeGun.ammoConfig.mag.currentItemStat + "/" + playerGun.activeGun.ammoConfig.mag.maxItemStat;
            if (playerGun.activeGun.ammoConfig.mag.currentItemStat == 0)
            {
                reloadReminder.enabled = true;
            }
            else
            {
                reloadReminder.enabled = false;
            }
        }
        else
        {
            magHud.text = "No Magazine";
            ammoHud.text = "0/0";
            reloadReminder.enabled = true;
        }        
    }
}
