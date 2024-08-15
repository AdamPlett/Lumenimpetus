using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GameManager;

public class BasicMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] private bool idle;
    
    [Header("Movement Variables")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float desiredSpeed;
    [Space(5)]
    [SerializeField] private Vector3 velocity;
    [Space(5)]
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;
    public float wallRunSpeed;
    public float airSpeed;
    [Space(5)]
    public float groundDrag;
    public float airDrag;
    public float airControlMultiplier;

    [Header("Ground Detection")]
    [SerializeField] private bool grounded;
    [Space(5)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDetectionLength;

    private RaycastHit groundHit;

    [Header("Slope Detection")]
    [SerializeField] private bool onSlope;
    [SerializeField] private float slopeAngle;
    [Space(5)]
    [SerializeField] private float minSlopeAngle;
    [SerializeField] private float maxSlopeAngle;

    private RaycastHit slopeHit;

    [Header("Wall Detection")]
    [SerializeField] private bool facingWall;
    [SerializeField] private bool wallToRight, wallToLeft;
    [Space(5)]
    private LayerMask wallLayer;
    [Space(5)]
    
    private RaycastHit wallHit;

    [Header("Misc")]
    public MovementStats moveStats;
    public PlayerMovementStateMachine stateMachine;

    #endregion

    public void FixedUpdate()
    {        
        CheckGrounded();
        CheckOnSlope();
        AdjustSpeed();
        UpdateVelocity();
    }

    public void MovePlayer(Vector3 moveDirection)
    {
        
        
        if (stateMachine.rb.velocity.magnitude < moveStats.maxSpeed)
        {
            stateMachine.rb.AddForce(moveDirection.normalized * currentSpeed, ForceMode.Force);
        }
    }

    public void RotatePlayer(Quaternion rotation)
    {
        stateMachine.rb.MoveRotation(rotation);
    }

    #region Speed Control

    public void SetDesiredSpeed(float speed)
    {
        desiredSpeed = speed;
    }

    private void AdjustSpeed()
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

    private void Accelerate()
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

    private void Decelerate()
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

    #region Velocity

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    private void UpdateVelocity()
    {
        velocity = stateMachine.rb.velocity;
    }

    #endregion

    #region Ground Detection

    public bool GetGrounded()
    {
        return grounded;
    }

    private void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, out groundHit, playerHeight * 0.5f + groundDetectionLength, groundLayer);

        if (grounded)
        {
            stateMachine.jump.coyoteTimeTimer = stateMachine.jump.stats.coyoteTime;
            stateMachine.jump.jumpPhase = 0;
        }
    }

    #endregion

    #region Slope Detection

    public bool GetOnSlope()
    {
        return onSlope;
    }

    private void CheckOnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + groundDetectionLength, groundLayer))
        {
            slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);

            onSlope = slopeAngle < maxSlopeAngle && slopeAngle != 0;
        }

        onSlope = false;
    }

    #endregion

    #region Wall Detection
    #endregion
}
