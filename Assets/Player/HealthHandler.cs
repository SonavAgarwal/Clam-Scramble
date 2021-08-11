using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class HealthHandler : NetworkBehaviour {
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn() {
        RespawnClientRpc();
    }

    [ClientRpc]
    void RespawnClientRpc() {
        try {
            CharacterController cc = GetComponent<CharacterController>();
            GetComponent<GravityHandler>().ResetGravity();
            cc.enabled = false;
            transform.position = new Vector3(0f, 20f, 0f);
            cc.enabled = true;
            Debug.Log("moved polayer");
        } catch (Exception e) {
            ShowErrorServerRpc(e.Message);
        }
    }

    [ServerRpc]
    void ShowErrorServerRpc(string s) {
        Debug.Log("exception");
        Debug.Log(s);
    }
}
