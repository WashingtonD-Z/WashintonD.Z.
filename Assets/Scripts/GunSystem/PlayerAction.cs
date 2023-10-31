using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector gunSelector;

    [SerializeField] private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && gunSelector.activeGun != null && inventory.isOpened == false)
        {
            gunSelector.activeGun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            List<Item> mags = inventory.getItemByType(ItemType.Mag);
            Item bestMag = mags[0];
                
            foreach (Item mag in mags)
            {
                if (mag.currentItemStat > bestMag.currentItemStat)
                {
                    bestMag = mag;
                }
            }
            gunSelector.activeGun.Reload(bestMag);
            //inventory.SwapMag(gunSelector.activeGun.Reload());
        }
    }
}
