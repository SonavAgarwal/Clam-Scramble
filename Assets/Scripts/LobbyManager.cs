using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{

    public GameObject lobbyCamera;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
