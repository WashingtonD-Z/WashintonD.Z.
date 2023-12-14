using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerGunSelector gunSelector;

    [SerializeField] private Inventory inventory;
    private float reloadCounter = 0;
    private float recoilCounter = 0;
    private bool isReloading = false;
    private bool isExperiencingRecoil = false;
    private bool canFire = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            reloadCounter += 1 * Time.deltaTime;
            if (reloadCounter >= gunSelector.activeGun.ammoConfig.reloadTime)
            {
                gunSelector.activeGun.EndReload();
                isReloading = false;
                reloadCounter = 0;
            }
        }
        if (isExperiencingRecoil)
        {
            recoilCounter += 1 * Time.deltaTime;
            if (recoilCounter >= gunSelector.activeGun.shootConfig.recoilTime)
            {
                gunSelector.activeGun.EndRecoil();
                isExperiencingRecoil = false;
                recoilCounter = 0;
            }
        }
        if (Input.GetMouseButton(0) && gunSelector.activeGun != null && inventory.isOpened == false)
        {
            canFire = gunSelector.activeGun.Shoot();
            if (canFire == true && isExperiencingRecoil == false)
            {
                isExperiencingRecoil = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isExperiencingRecoil && !isReloading)
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
