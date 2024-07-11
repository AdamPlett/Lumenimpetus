using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CameraManager : MonoBehaviour
{
    public bool locked;
    public Camera mainCam;

    [Header("Camera Vectors")]
    public Vector3 cameraForward;
    public Vector3 cameraRight;

    [Header("Camera Variables")]
    public float sensX, sensY;
    public float mouseX, mouseY;
    public float xRotation, yRotation;

    [Header("Player Reference")]
    public PlayerMovementStateMachine player;

    public void Update()
    {
        if (!locked)
        {
            mouseX = player.controller.input.RetrieveLookInput().x * Time.deltaTime * sensX;
            mouseY = player.controller.input.RetrieveLookInput().y * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraForward = new Vector3(mainCam.transform.forward.x, 0, mainCam.transform.forward.z);
            cameraRight = new Vector3(mainCam.transform.right.x, 0, mainCam.transform.right.z);

            player.movement.RotatePlayer(Quaternion.Euler(0f, yRotation, 0f));
        }
    }

    public void LateUpdate()
    {
        if (!locked)
        {
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
}
