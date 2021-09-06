using System;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine.UI;
using MLAPI.Spawning;
using MLAPI.Prototyping;

public class InventoryHandler : NetworkBehaviour
{

    private NetworkVariable<ulong> HoldingObjectID = new NetworkVariable<ulong>(ulong.MaxValue);

    [HideInInspector] public bool HoldingItem = false;
    private ItemHandler handItemHandler = null;
    private GameObject heldObject = null;

    private Transform faceTransform;
    private GameObject targetedGameObject = null;
    private bool hasTargetedGameObject = false;
    public Image crossHairImage;

    public float GrabReach = 4f;
    public float PlaceReach = 6f;


    // Start is called before the first frame update
    void Start()
    {
        faceTransform = transform.Find("Camera/Face").transform;

    }

    private void OnEnable()
    {
        HoldingObjectID.OnValueChanged += UpdateHoldingObject;
        
    }

    void UpdateHoldingObject(ulong previousObjectID, ulong newObjectID)
    {
        Debug.Log("reached updateHoldingObject");
        if (ulong.MaxValue == newObjectID)
        {
            HoldingItem = false;
            heldObject = null;
            handItemHandler = null; 
            Debug.Log(HoldingItem);
        } else
        {
            heldObject = NetworkSpawnManager.SpawnedObjects[newObjectID].gameObject;
            handItemHandler = heldObject.GetComponent<ItemHandler>();
            HoldingItem = true; 
            Debug.Log(HoldingItem);
        }
    }
    public void SetHoldingObject(ulong newObjectID)
    {
        HoldingObjectID.Value = newObjectID;
    }

    public void ClearHoldingObject()
    {
        HoldingObjectID.Value = ulong.MaxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (IsLocalPlayer)
        {
            DoUpdate();
        }

    }

    void DoUpdate()
    {
        if (hasTargetedGameObject)
        {
            crossHairImage.rectTransform.localScale = new Vector3(2, 2, 1);
        }
        else
        {
            crossHairImage.rectTransform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Drop();
        }

        if (Input.GetKey(KeyCode.F))
        {
            PickUp();
        }
    }

    void FixedUpdate()
    {
        if (IsLocalPlayer)
        {

        
            RaycastHit hit;
            if (faceTransform != null && Physics.Raycast(faceTransform.position, faceTransform.rotation * Vector3.forward, out hit, GrabReach))
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
            } else
            {

                hasTargetedGameObject = false;
                targetedGameObject = null;
            }
        }
    }

    public void PickUp()
    {
        if (targetedGameObject != null && !HoldingItem)
        {
            var objectID = targetedGameObject.GetComponent<NetworkObject>().NetworkObjectId;
            HoldServerRpc(objectID, NetworkManager.Singleton.LocalClientId);
        }
    }

    public void Drop()
    {
        if (HoldingItem)
        {
            DropServerRpc(HoldingObjectID.Value, NetworkManager.Singleton.LocalClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void HoldServerRpc(ulong objectID, ulong clientID)
    {
        var other = NetworkSpawnManager.SpawnedObjects[objectID].gameObject;
        other.GetComponent<NetworkObject>().ChangeOwnership(clientID);
        //other.GetComponent<NetworkTransform>().enabled = false;
        other.GetComponent<ItemHandler>().SetHoldingPlayer(clientID);
        NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<InventoryHandler>().SetHoldingObject(objectID);
    }

    [ServerRpc(RequireOwnership = false)]
    public void DropServerRpc(ulong objectID, ulong clientID)
    {
        var other = NetworkSpawnManager.SpawnedObjects[objectID].gameObject;
        other.GetComponent<NetworkObject>().RemoveOwnership();
        //other.GetComponent<NetworkTransform>().enabled = true;
        other.GetComponent<ItemHandler>().ClearHoldingPlayer();
        NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<InventoryHandler>().ClearHoldingObject();
    }

    //public void Hold(GameObject item)
    //{
    //    LogMessageServerRpc("Hold called: " + (!HoldingItem));
    //    if (!HoldingItem)
    //    {
    //        LogMessageServerRpc("Tried to hold an item");
    //        handObject = item;
    //        handItemHandler = item.GetComponent<ItemHandler>();
    //        HoldingItem = true;

    //        handItemHandler.StartPlayerHoldServerRpc(gameObject.GetComponent<NetworkObject>().NetworkObjectId, NetworkManager.LocalClientId);
    //    }
    //}

    //public void DropHeld()
    //{
    //    if (HoldingItem)
    //    {
    //        LogMessageServerRpc("Tried to hold an item");
    //        handItemHandler.DropServerRpc();
    //        handObject = null;
    //        handItemHandler = null;
    //        HoldingItem = false;
    //    }
    //}

    //private void OnLevelWasLoaded(int level)
    //{
    //    DropHeld();
    //}

    public bool GetRaycastHitInReach(out RaycastHit hit)
    {
        if (Physics.Raycast(faceTransform.position, faceTransform.rotation * Vector3.forward, out hit, PlaceReach))
        {
            return true;
        }
        return false;
    }

    public GameObject GetHeld()
    {
        return heldObject;
    }

    [ServerRpc]
    public void LogMessageServerRpc(string message)
    {

        Debug.Log("ServerRpc: " + message);
    }
}
