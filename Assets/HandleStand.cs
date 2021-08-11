using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleStand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("stood");
        if (other.gameObject.tag == "Player") {
            Debug.Log("player detected");
            Vector3 newPos = transform.position;
            newPos.y -= 0.2f;
            transform.position = newPos;
        }
    }
    
    private void OnTriggerExit(Collider other) {
        Debug.Log("unstood");
        if (other.gameObject.tag == "Player") {
            Vector3 newPos = transform.position;
            newPos.y += 0.2f;
            transform.position = newPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
