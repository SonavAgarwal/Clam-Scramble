using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.SceneManagement;
using System.Linq;

public class LobbyManager : NetworkBehaviour
{

    public GameObject lobbyCamera;

    public GameObject cratePrefab, gunPrefab;

    List<ulong> clientsReady = new List<ulong>();


    void OnEnable() {
        ConnectionHandler.OnServerStarted += OnServerStarted;
    }
    void OnDisable() {
        ConnectionHandler.OnServerStarted -= OnServerStarted;
    }

    void OnServerStarted() {
        DeactivateCamera();
    }

    public void DeactivateCamera() {
        lobbyCamera.GetComponent<AudioListener>().enabled = false;
        lobbyCamera.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //GameObject go = Instantiate(cratePrefab, new Vector3(0, 100, 0), Quaternion.identity);
        //go.GetComponent<NetworkObject>().Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckReady()
    {
        Debug.Log(GameObject.Find("NetworkingManager"));
        var connectedClients = GameObject.Find("NetworkingManager").GetComponent<NetworkManager>().ConnectedClientsList.Select(netClient => netClient.ClientId);
        bool ready = new HashSet<ulong>(clientsReady).SetEquals(new HashSet<ulong>(connectedClients));

        Debug.Log("Ready: " + ready);

        if (ready)
        {
            NetworkSceneManager.SwitchScene("BoxArena");
        }
    }

    public void AddReady(ulong clientID) {
        clientsReady.Add(clientID);

        CheckReady();
    }

    public void RemoveReady(ulong clientID) {
        clientsReady.Remove(clientID);
        CheckReady();
    }
}
