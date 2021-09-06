using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.Spawning;
using MLAPI.NetworkVariable;
using System;

public class ItemHandler : NetworkBehaviour
{
    //public float HoldingScale = 1;
    //private int location = 0; // 0 for world, 1 for player
    //private int pickupCooldown = 0;

    [StringInList("Weapon", "Object")] 
    public string ItemType;
    public float HoldingScale = 1f;

    private NetworkVariable<int> Location = new NetworkVariable<int>(0);
    private NetworkVariable<ulong> HoldingPlayerID = new NetworkVariable<ulong>(ulong.MaxValue);

    private GameObject player;
    private Transform handSlot;
    private Rigidbody rb;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        HoldingPlayerID.OnValueChanged += UpdateHoldingPlayer;
    }

    void UpdateHoldingPlayer(ulong previousClientID, ulong newClientID)
    {
        Debug.Log("Got Here");
        Debug.Log(previousClientID);
        Debug.Log(newClientID);
        if (newClientID == ulong.MaxValue)
        {
            Debug.Log("a");
            EndPlayerHold();
        }
        else
        {
            Debug.Log("b");
            StartPlayerHold(newClientID);
        }
    }

    public void SetHoldingPlayer(ulong clientID)
    {
        HoldingPlayerID.Value = clientID;
        //HoldingPlayerID.SetDirty(true);
    }
    public void ClearHoldingPlayer()
    {
        HoldingPlayerID.Value = ulong.MaxValue;
    }

    void FixedUpdate()
    {
        //if (pickupCooldown > 0) pickupCooldown--;
        if (Location.Value == 1 && player != null)
        {
            if (IsServer && IsOwnedByServer) 
            { 
                //MoveToHoldPosition();
            }
        }
        //else if (player == null)
        //{
        //    Location.Value = 0;
        //}
    }

    public GameObject GetPlayerGameObjectWithClientID(ulong clientID)
    {
        var playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject go in playerObjects)
        {
            if (go.GetComponent<NetworkObject>().OwnerClientId == clientID)
            {
                return go;
            }
        }
        return null;
    }

    public void StartPlayerHold(ulong clientID)
    {
        if (IsServer)
        {
            Location.Value = 1;
        }

        player = GetPlayerGameObjectWithClientID(clientID);


        rb.isKinematic = true;
        rb.useGravity = false;
        toggleColliders(false);

        handSlot = player.transform.Find("Camera/Hand");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetParent(handSlot);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1) * HoldingScale;
    }

    public void EndPlayerHold()
    {
        if (Location.Value == 0 || player == null) return;

        rb.isKinematic = false;
        rb.useGravity = true;
        toggleColliders(true);

        handSlot = null;
        //pickupCooldown = 25;

        transform.localScale = new Vector3(1, 1, 1);

        RaycastHit rayHit;
        if (player.GetComponent<InventoryHandler>().GetRaycastHitInReach(out rayHit))
        {
            transform.rotation = Quaternion.identity;
            var placePoint = rayHit.point;
            Vector3 surfaceNormal = rayHit.normal;
            placePoint += Vector3.Scale(GetComponent<Collider>().bounds.size, ClosestDirection(surfaceNormal));
            //placePoint.y += GetComponent<Collider>().bounds.size.y;
            transform.position = placePoint;   
        } else
        {
            rb.WakeUp();
            rb.AddForce(player.transform.forward * 200);
        }

        if (IsServer)
        {
            Location.Value = 0;
        }
        player = null;

        transform.SetParent(null);
    }

    // https://answers.unity.com/questions/617076/calculate-the-general-direction-of-a-vector.html
    private Vector3 ClosestDirection(Vector3 v) {
        Vector3[] compass = new Vector3[]{ Vector3.left, Vector3.right, Vector3.forward, Vector3.back, Vector3.up, Vector3.down };
        var maxDot = -Mathf.Infinity;
        var ret = Vector3.zero;
     
        foreach (Vector3 dir in compass) { 
            var t = Vector3.Dot(v, dir);
            if (t > maxDot) {
                ret = dir;
                maxDot = t;
            }
        }
 
     return ret;
    }


    //[ServerRpc]
    //public void DropServerRpc()
    //{
    //    transform.localScale = new Vector3(1, 1, 1);
    //    rb.isKinematic = false;
    //    rb.useGravity = true;
    //    location = 0;
    //    handSlot = null;
    //    toggleColliders(true);
    //    pickupCooldown = 25;
    //    rb.WakeUp();
    //    rb.AddForce(player.transform.forward * 200);
    //    player = null;
    //    //rb.AddForce(new Vector3(100, 100, 0));
    //    GetComponent<NetworkObject>().RemoveOwnership();
    //    Debug.Log("no more ownership");
    //}

    //[ServerRpc]
    //public void StartPlayerHoldServerRpc(ulong objectID, ulong clientID)
    //{

    //    var other = NetworkSpawnManager.SpawnedObjects[objectID].gameObject;

    //    GetComponent<NetworkObject>().ChangeOwnership(clientID);
    //    Debug.Log(clientID + " is the owner of " + objectID);

    //    rb.isKinematic = true;
    //    rb.useGravity = false;
    //    player = other;
    //    location = 1;
    //    handSlot = player.transform.Find("Camera/Hand");
    //    rb.velocity = Vector3.zero;
    //    rb.angularVelocity = Vector3.zero;

    //    transform.localScale = new Vector3(1, 1, 1) * HoldingScale;

    //    toggleColliders(false);
    //}

    

    public void MoveToHoldPosition()
    {
        if (handSlot != null)
        {
            //var playerHandPosition = Vector3.Lerp(transform.position, handSlot.position, Time.deltaTime * 30f);
            //var zeroRotation = Quaternion.Lerp(transform.rotation, handSlot.rotation, Time.deltaTime * 30f);
            var playerHandPosition = handSlot.position;
            var zeroRotation = handSlot.rotation;
            transform.position = playerHandPosition;
            transform.rotation = zeroRotation;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (location == 0)
    //    {
    //        if (other.tag == "Player" && pickupCooldown == 0)
    //        {
    //            //other.gameObject.GetComponent<InventoryHandler>().Hold(gameObject);
    //        }
    //    }
    //}

    private void toggleColliders(bool to)
    {
        var colliders = GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = to;
        }
    }

    public bool IsHeld()
    {
        return Location.Value == 1;
    }
    
    public int GetLocation()
    {
        return Location.Value;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
