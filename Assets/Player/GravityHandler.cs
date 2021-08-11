using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class GravityHandler : NetworkBehaviour, MotionModifier {

    CharacterController characterController;
    MotionHandler motionHandler;

    private float gravity = -16f;
     
    Vector3 velocity;
 
    public Vector3 Motion { get; set; }

    // Start is called before the first frame update
    void Start() {
        if (IsLocalPlayer) {
            characterController = GetComponent<CharacterController>();
            motionHandler = GetComponent<MotionHandler>();

            motionHandler.AddMotion(this);

            velocity = Vector3.zero;
        }
    }

    
    void FixedUpdate() {
        if (IsLocalPlayer) {
            Move();
        }
    }

    // Update is called once per frame
    void Move() {
        velocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded) {
            velocity = Vector3.zero;
        }

        Motion = velocity;
    }

    public void ResetGravity() {
        velocity.y = 0;
    }
}
