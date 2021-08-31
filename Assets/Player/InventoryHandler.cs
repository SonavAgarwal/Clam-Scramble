using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;

public class InventoryHandler : NetworkBehaviour
{

    private GameObject handObject = null;
    private ItemHandler handItemHandler = null;
    [HideInInspector] public bool HoldingItem = false;
    private Transform faceTransform;
    public Image crossHairImage;
    private bool hasTargetedGameObject = false;
    private GameObject targetedGameObject = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.Find("Camera/Face"));
        faceTransform = transform.Find("Camera/Face").transform;
        Debug.Log(faceTransform);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (hasTargetedGameObject)
        {

            crossHairImage.rectTransform.localScale = new Vector3(2, 2, 1);
        } else
        {

            crossHairImage.rectTransform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            DropHeld();
        }

        if (Input.GetKey(KeyCode.F))
        {
            if (targetedGameObject != null)
            {
                Hold(targetedGameObject);
            }
        }

        if (HoldingItem)
        {
            handItemHandler.MoveToHoldPosition();
        } 

    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (faceTransform != null && Physics.Raycast(faceTransform.position, faceTransform.rotation * Vector3.forward, out hit, 3))
        {
            if (hit.collider.gameObject.GetComponent<ItemHandler>() != null)
            {
                hasTargetedGameObject = true;
                targetedGameObject = hit.collider.gameObject;
            }
            else
            {
                hasTargetedGameObject = false;
                targetedGameObject = null;
            }

            //Debug.Log(targetedGameObject != null);
        } else
        {

            hasTargetedGameObject = false;
            targetedGameObject = null;
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
