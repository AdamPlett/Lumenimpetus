using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class BasicMovement : MonoBehaviour
{
    [SerializeField] private bool idle;

    [Header("Ground Detection")]
    [SerializeField] private bool grounded;
    [SerializeField] private LayerMask groundLayer;
    [Space(5)]
    [SerializeField] private float playerHeight;
    [SerializeField] private float groundDetectionLength;
    
    [Header("Movement Variables")]
    public float currentSpeed;
    public float desiredSpeed;
    [Space(5)]
    public Vector3 lookDirection;
    public Vector3 moveDirection;
    public Vector3 velocity;
    [Space(5)]
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float wallRunSpeed;
    [Space(5)]
    public float groundDrag;
    public float airDrag;
    public float airControlMultiplier;
    [Space(5)]
    public MovementStats moveStats;

    public PlayerMovementStateMachine stateMachine;

    public void FixedUpdate()
    {        
        CheckGrounded();
    }

    public Vector3 CalculateLookDirection()
    {
        Vector3 cameraForward = new(stateMachine.playerCam.transform.forward.x, 0, stateMachine.playerCam.transform.forward.z);
        Vector3 cameraRight = new(stateMachine.playerCam.transform.right.x, 0, stateMachine.playerCam.transform.right.z);

        return cameraForward.normalized + cameraRight.normalized;
    }

    public Vector3 CalculateMoveDirection()
    {
        Vector3 cameraForward = new(stateMachine.playerCam.transform.forward.x, 0, stateMachine.playerCam.transform.forward.z);
        Vector3 cameraRight = new(stateMachine.playerCam.transform.right.x, 0, stateMachine.playerCam.transform.right.z);

        float horizontalInput = stateMachine.controller.input.RetrieveMoveInput().x;
        float verticalInput = stateMachine.controller.input.RetrieveMoveInput().y;

        return cameraForward.normalized * horizontalInput + cameraRight.normalized * verticalInput;
    }

    public void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + groundDetectionLength, groundLayer);
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void MoveOnGround()
    {
        moveDirection = CalculateMoveDirection();

        stateMachine.rb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.VelocityChange);
    }

    public void MoveInAir()
    {
        moveDirection = CalculateMoveDirection();

        stateMachine.rb.AddForce(moveDirection.normalized * currentSpeed * airControlMultiplier, ForceMode.VelocityChange);
    }
}
