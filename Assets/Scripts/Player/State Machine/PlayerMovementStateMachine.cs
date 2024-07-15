using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateMachine : StateMachine
{
    [Header("Player Components")]
    public Controller controller;
    public Rigidbody rb;

    [Header("Player Camera")]
    public Camera playerCam;
    public CameraManager cm;

    [Header("Movement Scripts")]
    public BasicMovement movement;
    public Jump jump;
    public Dash dash;

    // Movement States
    private PlayerIdleState idleState;
    private PlayerMoveState moveState;
    private PlayerCrouchState crouchState;
    private PlayerSlideState slideState;
    private PlayerJumpState jumpState;
    private PlayerFallState fallState;
    private PlayerDashState dashState;
    private PlayerGrappleState grappleState;
    private PlayerWallRunState wallRunState;
    private PlayerWallJumpState wallJumpState;

    private void Start()
    {
        SwitchToMoveState();
    }

    #region State Switchers

    public void SwitchToIdleState()
    {
        if (idleState == null)
        {
            idleState = new PlayerIdleState(this);
        }

        SwitchState(idleState);
    }

    public void SwitchToMoveState()
    {
        if (moveState == null)
        {
            moveState = new PlayerMoveState(this);
        }

        SwitchState(moveState);
    }

    public void SwitchToJumpState()
    {
        if (jumpState == null)
        {
            jumpState = new PlayerJumpState(this);
        }

        if (jump.jumpInputDelayTimer < 0)
        {
            jump.desiredJump |= controller.input.RetrieveJumpInput();

            if (jump.desiredJump)
            {
                jump.jumpInputDelayTimer = jump.stats.jumpInputDelay;
                jump.jumpBufferTimer = jump.stats.jumpBuffer;
            }

            if (jump.jumpBufferTimer > 0)
            {
                jump.desiredJump = false;

                Debug.Log("JumpBufferTimer: "+jump.jumpBufferTimer);
                Debug.Log("CoyoteTimeTimer: "+jump.coyoteTimeTimer);
                Debug.Log("TimeBetweenJumps: " + jump.timeBetweenJumps);

                if ((jump.jumpBufferTimer > 0 && jump.coyoteTimeTimer > 0 && jump.timeBetweenJumps < 0) || (jump.jumpPhase < jump.stats.doubleJumps && jump.timeBetweenJumps < 0))
                {
                    SwitchState(jumpState);
                }
            }
        }
    }

    public void SwitchToFallState()
    {
        if (fallState == null)
        {
            fallState = new PlayerFallState(this);
        }

        SwitchState(fallState);
    }

    public void SwitchToCrouchState()
    {
        if (crouchState == null)
        {
            crouchState = new PlayerCrouchState(this);
        }

        SwitchState(crouchState);
    }

    public void SwitchToSlideState()
    {
        if (slideState == null)
        {
            slideState = new PlayerSlideState(this);
        }

        SwitchState(slideState);
    }

    public void SwitchToDashState()
    {
        if (dashState == null)
        {
            dashState = new PlayerDashState(this);
        }

        SwitchState(dashState);
    }

    public void SwitchToGrappleState()
    {
        if (grappleState == null)
        {
            grappleState = new PlayerGrappleState(this);
        }

        SwitchState(grappleState);
    }

    public void SwitchToWallRunState()
    {
        if (wallRunState == null)
        {
            wallRunState = new PlayerWallRunState(this);
        }

        SwitchState(wallRunState);
    }

    public void SwitchToWallJumpState()
    {
        if (wallJumpState == null)
        {
            wallJumpState = new PlayerWallJumpState(this);
        }

        SwitchState(wallJumpState);
    }

    #endregion
}
