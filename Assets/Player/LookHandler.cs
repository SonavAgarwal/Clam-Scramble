using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class LookHandler : NetworkBehaviour {

    private float mouseSensitivity = 5f;
    private float clampAngle = 89.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    public Transform cameraTransform;
    public Transform camera3Transform;
    public Transform camera2Transform;

    private int currentCamera = 0; // 0 for first person, 1 for 3rd person, 2 for 2nd person
    private static int perspectiveChangeCooldownTime = 150;
    private int perspectiveChangeCooldown = perspectiveChangeCooldownTime;

    void Start () {
        setCamera3Active(false);  
        setCamera2Active(false);  

        if (!IsLocalPlayer) {
            setCamera1Active(false);
            Debug.Log("The foreign player is disabled");
        } else {
            Debug.Log("Local player is looking");
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 rot = transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
        }
    }

    private void setCamera1Active(bool active) {
        cameraTransform.GetComponent<AudioListener>().enabled = active;
        cameraTransform.GetComponent<Camera>().enabled = active; 
    }

    private void setCamera3Active(bool active) {
        camera3Transform.GetComponent<AudioListener>().enabled = active;
        camera3Transform.GetComponent<Camera>().enabled = active; 
    }

    private void setCamera2Active(bool active) {
        camera2Transform.GetComponent<AudioListener>().enabled = active;
        camera2Transform.GetComponent<Camera>().enabled = active; 
    }

    void Update() {
        if (IsLocalPlayer) {
            Look();
            AllowEscape();
            CheckPerspective();
        }
    }

    void Look ()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity;
        rotX += mouseY * mouseSensitivity;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
        transform.rotation = localRotation;
        Quaternion cameraRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        cameraTransform.rotation = cameraRotation;

        // transform.rotation.x = rotX;
        // transform.rotation.y = rotY;
    }

    void CheckPerspective() {
        if (Input.GetKey(KeyCode.R) && perspectiveChangeCooldown <= 0) { 
            currentCamera += 1;
            currentCamera = currentCamera % 3;
            if (currentCamera == 0) {
                setCamera1Active(true);
                setCamera3Active(false);
                setCamera2Active(false);
            } else if (currentCamera == 1) {
                setCamera1Active(false);
                setCamera3Active(true);
                setCamera2Active(false);
            } else if (currentCamera == 2) {
                setCamera1Active(false);
                setCamera3Active(false);
                setCamera2Active(true);

            }

            perspectiveChangeCooldown = perspectiveChangeCooldownTime;
        }

        if (perspectiveChangeCooldown > 0) perspectiveChangeCooldown--;
    }
    
    void AllowEscape() {
        if (Input.GetKey(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetMouseButton(0)) {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
 }
