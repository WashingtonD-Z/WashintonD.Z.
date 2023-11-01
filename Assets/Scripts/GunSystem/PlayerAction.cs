using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector gunSelector;

    [SerializeField] private Inventory inventory;
    private float counter = 0;
    private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            counter += 1 *Time.deltaTime;
            if (counter >= gunSelector.activeGun.ammoConfig.reloadTime)
            {
                Debug.Log("hi");
                gunSelector.activeGun.EndReload();
                isReloading = false;
                counter = 0;
            }
        }
        if (Input.GetMouseButton(0) && gunSelector.activeGun != null && inventory.isOpened == false)
        {
            gunSelector.activeGun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            List<GameObject> mags = inventory.GetItemByType(ItemType.Mag);
            if(mags.Count == 0)
            {
                GameObject Mag = gunSelector.activeGun.Reload(null);
                if (Mag)
                {
                    inventory.AddItemToInventory(Mag);
                }
                return;
            }
            GameObject bestMag = mags[0];
                
            foreach (GameObject mag in mags)
            {
                if (mag.GetComponent<Item>().currentItemStat > bestMag.GetComponent<Item>().currentItemStat)
                {
                    bestMag = mag;
                }
            }
            GameObject oldMag = gunSelector.activeGun.Reload(bestMag);
            inventory.RemoveItemFromInventory(bestMag);
            if (oldMag == null)
            {
                return;
            }
            inventory.AddItemToInventory(oldMag);
        }

    }
}
