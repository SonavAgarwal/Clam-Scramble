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
        if (IsLocalPlayer) HandleAttack();
        
    }

    private void HandleAttack()
    {
        if (inventory.HoldingItem)
        {
            if (inventory.GetHeld().GetComponent<ItemHandler>().ItemType == "Weapon")
            {
                if (Input.GetMouseButton(0))
                {
                    inventory.GetHeld().GetComponent<GunHandler>().StartAttack();
            
                } else
                {
                    inventory.GetHeld().GetComponent<GunHandler>().StopAttack();
                }
            }
        }
    }
}
