using System;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class HandleStand : NetworkBehaviour
{
    // Start is called before the first frame update

     bool hasPlayer = false;
    
    void Start()
    {

    }

    void OnTriggerEnter(Collider other) {
        if (hasPlayer) return;

        Debug.Log("stood");
        if (other.gameObject.tag == "Player")
        {

            hasPlayer = true;

            Debug.Log("player detected");
            Vector3 newPos = transform.position;
            newPos.y -= 0.2f;

            transform.position = newPos;
            var lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
            var clientID = other.gameObject.GetComponent<NetworkObject>().OwnerClientId;

            lobbyManager.AddReady(clientID);

         }
    }
    
    void OnTriggerExit(Collider other) {

        if (!hasPlayer) return;

        Debug.Log("unstood");
        if (other.gameObject.tag == "Player") {

            hasPlayer = false;

            Vector3 newPos = transform.position;
            newPos.y += 0.2f;
            transform.position = newPos;

            try
            {
                var networkingManager = GameObject.Find("NetworkingManager");
                var lobbyManager = networkingManager.GetComponent<LobbyManager>();
                var clientID = other.gameObject.GetComponent<NetworkObject>().OwnerClientId;
                lobbyManager.RemoveReady(clientID);

            } catch (Exception e)
            {

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
