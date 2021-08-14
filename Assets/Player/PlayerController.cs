using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float gravityConstant = -0.1f;


    // public float lookSpeed;
    // public float jumpAmt;
    // public float maxVertLookAngle;
    // public GameObject camera;
    // public float sprint;
    // float horzMomentum = 1.1f;


    CharacterController characterController;

    Vector3 velocity;
    Vector3 acceleration;
    Vector3 gravity;

    void Start() {
        characterController = GetComponent<CharacterController>();

        gravity = new Vector3(0, gravityConstant, 0);
        velocity = new Vector3(0, 0, 0); // velocity
        acceleration = new Vector3(0, 0, 0); // acceleration

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.Tab)) {
                velocity += transform.forward * 2 * moveSpeed;
            } else {
                velocity += transform.forward * moveSpeed;
            }
        }

        if (Input.GetKey(KeyCode.S)) {
            velocity -= transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.A)) {
            velocity -= transform.right * moveSpeed;
        }

        if (Input.GetKey(KeyCode.D)) {
            velocity += transform.right * moveSpeed;
        }
    
        if (characterController.isGrounded && Input.GetKey(KeyCode.Space)) {
            velocity.y += 1f;
        }

        velocity += gravity;
        velocity += acceleration;

        if (characterController.isGrounded) {
            velocity.y = 0;
        }
        
        characterController.SimpleMove(velocity * Time.deltaTime);
    }

    // void FixedUpdate()
    // {




    //     v.x = v.x / horzMomentum;
    //     v.z = v.z / horzMomentum;
    //     // movement
    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         if (Input.GetKey(KeyCode.LeftShift))
    //         {
    //              v += transform.forward * moveSpeed;
    //         }
    //         else
    //         {
    //             v += transform.forward;
    //         }

    //     }
    //     else if (Input.GetKey(KeyCode.A))
    //     {
    //         v -= transform.right;
    //     }
    //     else if (Input.GetKey(KeyCode.S))
    //     {
    //         v -= transform.forward;
    //     }
    //     else if (Input.GetKey(KeyCode.D))
    //     {
    //         v += transform.right;
    //     }


    //     Debug.Log(cc);
    //     Debug.Log(cc.isGrounded);

    //         v = new Vector3(v.x, -0.01f, v.z);
    //     // jumping
    //     if (cc.isGrounded)
    //     {

    //         if (Input.GetKey(KeyCode.Space))
    //         {
    //             a = new Vector3(0, jumpAmt, 0);

    //         }
    //     }
    //     else 
    //     {
    //             // a = Physics.gravity;
    //     }
    //     v += a * Time.deltaTime;
    //     cc.Move(v * Time.deltaTime);
    // }
    // void Update()
    // {
    //     // clamp vert mouse look angle
    //     float mouseHorz = Input.GetAxis("Mouse X");
    //     float mouseVert = Input.GetAxis("Mouse Y");
    //     float eulerX = camera.transform.localEulerAngles.x;
    //     if (eulerX > 180f)
    //         eulerX = eulerX - 360f;
    //     if ((eulerX < -maxVertLookAngle && mouseVert > 0) ||
    //         (eulerX > maxVertLookAngle && mouseVert < 0))
    //     {
    //         mouseVert = 0.0f;
    //     }
    //     // mouse look
    //     if (!Mathf.Approximately(mouseHorz, 0.0f))
    //     {
    //         transform.Rotate(Vector3.up, mouseHorz * lookSpeed, Space.Self);
    //     }
    //     if (!Mathf.Approximately(mouseVert, 0.0f))
    //     {
    //         camera.transform.Rotate(Vector3.right, -mouseVert * lookSpeed, Space.Self);
    //     }
    // }

}
