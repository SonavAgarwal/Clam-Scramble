using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;

public class ConnectionHandler : MonoBehaviour {

    public GameObject connectPanel;

    public void Host() {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.StartHost(Vector3.zero, Quaternion.identity);
        connectPanel.SetActive(false);
    }

    public void Join() {
        NetworkManager.Singleton.StartClient();
        connectPanel.SetActive(false);
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientID, NetworkManager.ConnectionApprovedDelegate callback) {
        Debug.Log("Approving a connection");
        callback(true, null, true, GetRandomSpawn(), Quaternion.identity);
    }

    Vector3 GetRandomSpawn() {
        float x = Random.Range(-4f, 4f);
        float y = 10f;
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
