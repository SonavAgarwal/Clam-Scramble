using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class DebugHelpers : MonoBehaviour
{
    [ServerRpc]
    public static void LogMessageServerRpc(string message)
    {

        Debug.Log("ServerRpc: " + message);
    }
}
