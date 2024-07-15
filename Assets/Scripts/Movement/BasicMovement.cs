using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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

    [Header("Player Rotation")]
    public float yRotation;

    [Header("Misc")]
    public MovementStats moveStats;
    public PlayerMovementStateMachine stateMachine;

    public void FixedUpdate()
    {        
        CheckGrounded();
        AdjustSpeed();
    }

    public Vector3 CalculateMoveDirection()
    {
        airControlMultiplier = moveStats.airControlMult;

        float horizontalInput = stateMachine.controller.input.RetrieveMoveInput().x;
        float verticalInput = stateMachine.controller.input.RetrieveMoveInput().y;

        return stateMachine.cm.cameraForward.normalized * verticalInput + stateMachine.cm.cameraRight.normalized * horizontalInput;
    }

    public void RotatePlayer(Quaternion rotation)
    {
        stateMachine.rb.MoveRotation(rotation);
    }

    public void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + groundDetectionLength, groundLayer);

        if (grounded)
            stateMachine.jump.coyoteTimeTimer = stateMachine.jump.stats.coyoteTime;
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void MoveOnGround()
    {
        moveDirection = CalculateMoveDirection();

        if(stateMachine.rb.velocity.magnitude < stateMachine.movement.moveStats.maxSpeed)
        {
            stateMachine.rb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.Force);
        }
    }

    public void MoveInAir()
    {
        moveDirection = CalculateMoveDirection();

        stateMachine.rb.AddForce(moveDirection.normalized * currentSpeed * airControlMultiplier, ForceMode.Force);
    }

    #region Speed Control

    public void SetDesiredSpeed(float speed)
    {
        desiredSpeed = speed;
    }

    public void AdjustSpeed()
    {
        if(currentSpeed > desiredSpeed)
        {
            Decelerate();
        }
        else if(currentSpeed < desiredSpeed)
        {
            Accelerate();
        }
    }

    public void Accelerate()
    {
        if(GetGrounded())
        {
            // Ground Acceleration
            currentSpeed += moveStats.groundAcceleration * Time.deltaTime;
        }
        else
        {
            // Air Acceleration
            currentSpeed += moveStats.airAcceleration * Time.deltaTime;
        }

        if(currentSpeed > desiredSpeed)
        {
            currentSpeed = desiredSpeed;
        }
    }

    public void Decelerate()
    {
        if (GetGrounded())
        {
            // Ground Deceleration
            currentSpeed -= moveStats.groundDeceleration * Time.deltaTime;
        }
        else
        {
            // Air Deceleration
            currentSpeed -= moveStats.airDecelertaion * Time.deltaTime;
        }

        if (currentSpeed < desiredSpeed)
        {
            currentSpeed = desiredSpeed;
        }
    }

    #endregion
}
