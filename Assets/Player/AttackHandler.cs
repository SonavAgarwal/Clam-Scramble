using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class AttackHandler : NetworkBehaviour
{

    //private GameObject weapon;
    private InventoryHandler inventory;

    void Start()
    {
        inventory = GetComponent<InventoryHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.HoldingItem)
        {
            Debug.Log("1");
            Debug.Log(inventory);
            Debug.Log(inventory.GetHeld());
            Debug.Log(inventory.GetHeld().GetComponent<ItemHandler>());
            Debug.Log(inventory.GetHeld().GetComponent<ItemHandler>().ItemType);
            Debug.Log(inventory.GetHeld().GetComponent<ItemHandler>().ItemType == "Weapon");
            if (inventory.GetHeld().GetComponent<ItemHandler>().ItemType == "Weapon")
            {
                Debug.Log("2");
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("3");
                    inventory.GetHeld().GetComponent<GunHandler>().StartShoot();
            
                } else
                {
                    inventory.GetHeld().GetComponent<GunHandler>().StopShoot();
                }
            }
        }
    }
}
