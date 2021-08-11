using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable; 
using MLAPI.Messaging;

public class MapStart : MonoBehaviour 
{
    private NetworkVariableInt islandSeed = new NetworkVariableInt(-10);
    // Start is called before the first frame update
    
    // void OnEnable() {
    //     ConnectionHandler.BeforeGameStarted += BeforeGameStarted;
    //     ConnectionHandler.OnGameStarted += OnGameStarted;
    // }
    
    // void OnDisable() {
    //     ConnectionHandler.BeforeGameStarted -= BeforeGameStarted;
    //     ConnectionHandler.OnGameStarted -= OnGameStarted;
    // }

    // void BeforeGameStarted() {
    //     Debug.Log("here");
    //     Debug.Log("here2");
    //     if (islandSeed.Value < 0) {
    //         islandSeed.Value = UnityEngine.Random.Range(0, 1000);
    //     }
    //     PrintSeedServerRpc(islandSeed.Value + "");
    //  }
    
    // void OnGameStarted() { 
    //     PrintSeedServerRpc(islandSeed.Value + " is seed");
    //     GetComponent<MapGenerator>().MakeMap(islandSeed.Value);
    //     PrintSeedServerRpc(islandSeed.Value + " is seed");
    // }

    // void PrintSeedServerRpc(string seed) {
    //     // Debug.Log(seed);
    //     Debug.Log("seed: " + seed);
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
