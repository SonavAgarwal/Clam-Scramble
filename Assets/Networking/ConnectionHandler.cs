using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;

public class ConnectionHandler : NetworkBehaviour {

    public GameObject connectPanel;

    public delegate void ServerStart();
    public static event ServerStart OnServerStarted;

    // public delegate void GameStart();
    // public static event GameStart OnGameStarted;
 
    public void Host() {

        Debug.Log("Heyyy");

        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(GetRandomSpawn(), Quaternion.identity);
        connectPanel.SetActive(false);

        if (OnServerStarted != null)    
            OnServerStarted();

        // if (OnGameStarted != null)    
        //     OnGameStarted();
        
    }

    public void Join() {
        NetworkManager.Singleton.StartClient();
        connectPanel.SetActive(false);

        // if (OnGameStarted != null)    
        //     OnGameStarted();
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback) {
        Debug.Log("Approving a connection");
        callback(true, null, true, GetRandomSpawn(), Quaternion.identity);
    }

    Vector3 GetRandomSpawn() {
        float x = Random.Range(-4f, 4f);
        float y = 20f;
        float z = Random.Range(-4f, 4f);
        return new Vector3(x, y, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
