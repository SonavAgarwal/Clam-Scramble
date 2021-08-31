using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class KnockbackMotion : NetworkBehaviour, MotionModifier
{

    public Vector3 Motion { get; set; }
    [HideInInspector] public MotionHandler motionHandler { get; set; }

    public void SetKnockbackMotion(Vector3 motion)
    {
        Motion = motion;
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Debug.Log("fixedupdate");
        Motion = Motion * 0.3f;
        if (Motion.magnitude < 0.1f)
        {
            Debug.Log("2asdf");
            var motionHandlerSave = motionHandler;
            Debug.Log("Motion handler");
            Debug.Log(motionHandlerSave);
            motionHandler.RemoveMotion(this);
        }
    }
}
