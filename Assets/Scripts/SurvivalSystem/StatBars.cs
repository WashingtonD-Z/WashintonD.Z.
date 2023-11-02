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
    [SerializeField] private PlayerGunSelector playerGun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthHud.text = Mathf.Round(stats.health) + "/" + stats.maxHealth;
        hungerHud.text = Mathf.Round(stats.hunger) + "/" + stats.maxHunger;
        if (playerGun.activeGun.ammoConfig.mag)
        {
            ammoHud.text = playerGun.activeGun.ammoConfig.mag.currentItemStat + "/" + playerGun.activeGun.ammoConfig.mag.maxItemStat;
        }
        else
        {
            ammoHud.text = "0/0";
        }
    }
}
