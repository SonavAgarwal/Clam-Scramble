using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using System;

public class ItemHandler : NetworkBehaviour
{
    [StringInList("Weapon", "Object")] 
    public string ItemType;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (pickupCooldown > 0) pickupCooldown--;
        if (location == 1 && player != null)
        {

            var playerHandPosition = Vector3.zero;
            var zeroRotation = Quaternion.identity;
            transform.localPosition = playerHandPosition;
            transform.localRotation = zeroRotation;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else if (player == null)
        {
            location = 0;
        }}


    public void Drop()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(player.transform.forward);
        player = null;
        location = 0;
        handSlot = null;
        transform.parent = null;
        toggleColliders(true);
        pickupCooldown = 40;
        rb.WakeUp();
    }

    public void StartPlayerHold(GameObject other)
    {
        rb.isKinematic = false;
        rb.useGravity = false;
        player = other;
        location = 1;
        handSlot = player.transform.Find("Camera/Hand");
        transform.SetParent(handSlot);
        var playerHandPosition = Vector3.zero;
        var zeroRotation = Quaternion.identity;
        transform.localPosition = playerHandPosition;
        transform.localRotation = zeroRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        toggleColliders(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (location == 0)
        {
            if (other.tag == "Player" && pickupCooldown == 0)
            {
                other.gameObject.GetComponent<InventoryHandler>().Hold(gameObject);
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
}
