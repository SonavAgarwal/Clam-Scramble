using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class InputHandler : NetworkBehaviour, MotionModifier {

    CharacterController characterController;
    MotionHandler motionHandler;

    private float moveSpeed = 10 * 20;
    private float jumpSpeed = 15f;
    
    Vector3 velocity;
    Vector3 acceleration;

    public Vector3 Motion { get; set; }

    void Start() {
        if (IsLocalPlayer) {
            Debug.Log("input is being handled");

            characterController = GetComponent<CharacterController>();
            motionHandler = GetComponent<MotionHandler>();

            motionHandler.AddMotion(this);
        }
    }

    
    void Update() {
        if (IsLocalPlayer) {
            Move();
        }
    }

    void Move() {
 
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0f;
        right.y = 0f;

 
        float inAirModifier = characterController.isGrounded ? 1f : 0.3f;

        if (Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.Tab)) {
                velocity += forward * 1.3f * moveSpeed * inAirModifier * Time.deltaTime;
            } else {
                velocity += forward * moveSpeed * inAirModifier * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.S)) {
            velocity -= forward * moveSpeed * inAirModifier * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A)) {
            velocity -= right * 0.5f * moveSpeed * inAirModifier * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D)) {
            velocity += right * 0.5f * moveSpeed * inAirModifier * Time.deltaTime;
        }
    
        if (characterController.isGrounded) {
            velocity.y = 0;
        }

        if (characterController.isGrounded) {
            if (Input.GetKey(KeyCode.Space)) {
                velocity.y += jumpSpeed;
            }
        }

        if (characterController.isGrounded) {
            velocity.x = velocity.x * 0.985f ;
            velocity.z = velocity.z * 0.985f ;
        } else {
            velocity.x = velocity.x * 0.99f ;
            velocity.z = velocity.z * 0.99f ;
        }

 
        Motion = velocity;
    }

}
