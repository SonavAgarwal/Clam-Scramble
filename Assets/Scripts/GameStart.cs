using System;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (Screen.fullScreen) {
            Screen.fullScreen = false;
        } 
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
