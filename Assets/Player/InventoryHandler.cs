using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHandler : MonoBehaviour
{

    private GameObject handObject = null;
    private ItemHandler handItemHandler = null;
    [HideInInspector] public bool HoldingItem = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {

            DropHeld();

        }
    }

    public void Hold(GameObject item)
    {
        if (!HoldingItem)
        {
            handObject = item;
            handItemHandler = item.GetComponent<ItemHandler>();
            HoldingItem = true;

            handItemHandler.StartPlayerHold(gameObject);
        }
    }

    public void DropHeld()
    {
        if (HoldingItem)
        {
            handItemHandler.Drop();
            handObject = null;
            handItemHandler = null;
            HoldingItem = false;
        }
    }

    public GameObject GetHeld()
    {
        return handObject;
    }
}
