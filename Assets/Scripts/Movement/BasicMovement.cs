using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GameManager;

public class BasicMovement : MonoBehaviour
{
    #region Variables
    
    [Header("Movement Variables")]
    [SerializeField] private bool idle;
    [Space(5)]
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

    [Header("Misc")]
    public MovementStats moveStats;
    public PlayerMovementStateMachine stateMachine;

    #endregion

    public void FixedUpdate()
    {        
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
        if(stateMachine.groundCheck.GetGrounded())
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
        if (stateMachine.groundCheck.GetGrounded())
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
}
