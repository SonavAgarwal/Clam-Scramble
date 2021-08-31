using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using System;

public class ItemHandler : NetworkBehaviour
{
    [StringInList("Weapon", "Object")] 
    public string ItemType;
    public float HoldingScale = 1;
    private int location = 0; // 0 for world, 1 for player
    private GameObject player;
    private Transform handSlot;

    private int pickupCooldown = 0;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (pickupCooldown > 0) pickupCooldown--;
        if (location == 1 && player != null)
        {

            //MoveToHoldPosition();
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
        }
        else if (player == null)
        {
            location = 0;
        }
    }


    public void Drop()
    {
        transform.localScale = new Vector3(1, 1, 1);
        rb.isKinematic = false;
        rb.useGravity = true;
        location = 0;
        handSlot = null;
        toggleColliders(true);
        pickupCooldown = 25;
        rb.WakeUp();
        rb.AddForce(player.transform.forward * 200);
        player = null;
        //rb.AddForce(new Vector3(100, 100, 0));
    }

    public void StartPlayerHold(GameObject other)
    {
        rb.isKinematic = true;
        rb.useGravity = false;
        player = other;
        location = 1;
        handSlot = player.transform.Find("Camera/Hand");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.localScale = new Vector3(1, 1, 1) * HoldingScale;

        toggleColliders(false);
    }

    public void MoveToHoldPosition()
    {
        var playerHandPosition = handSlot.position;
        var zeroRotation = handSlot.rotation;
        //var playerHandPosition = Vector3.Lerp(transform.position, handSlot.position, Time.deltaTime * 30f);
        //var zeroRotation = Quaternion.Lerp(transform.rotation, handSlot.rotation, Time.deltaTime * 30f);
        transform.position = playerHandPosition;
        transform.rotation = zeroRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (location == 0)
        {
            if (other.tag == "Player" && pickupCooldown == 0)
            {
                //other.gameObject.GetComponent<InventoryHandler>().Hold(gameObject);
            }
        }
    }

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
        return location == 1;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
