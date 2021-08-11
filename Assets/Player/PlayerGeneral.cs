using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PlayerGeneral : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer) {
            // GetComponent<HealthHandler>().Respawn();
            Debug.Log("Local Playr Started");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
