using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class MotionHandler : NetworkBehaviour
{

    CharacterController characterController;

    private List<MotionModifier> motions = new List<MotionModifier>();

    void Start() {
        if (IsLocalPlayer) {
            Debug.Log("motion is being handled");
            characterController = GetComponent<CharacterController>();
        }
    }

    void FixedUpdate() {
        if (IsLocalPlayer) {
            Move();
        }
    }

    void Move() {
        Vector3 movement = Vector3.zero;


        foreach (MotionModifier motion in motions) {
            movement += motion.Motion;
        }    

        characterController.Move(movement * Time.deltaTime);
    }

    public void AddMotion(MotionModifier motion) {
        motions.Add(motion);
    }

    public void RemoveMotion(MotionModifier motion) {
        motions.Remove(motion);
    }
}
