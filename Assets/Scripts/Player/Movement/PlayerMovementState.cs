using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState : State
{
    protected PlayerIdleState idleState;
    protected PlayerMoveState moveState;
    protected PlayerCrouchState crouchState;
    protected PlayerSlideState slideState;
    protected PlayerJumpState jumpState;
    protected PlayerFallState fallState;
    protected PlayerDashState dashState;
    protected PlayerGrappleState grappleState;

    protected readonly PlayerMovementStateMachine stateMachine;

    protected PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    #region State Switchers

    private void SwitchToIdleState()
    {
        if(idleState == null)
        {
            idleState = new PlayerIdleState(stateMachine);
        }

        stateMachine.SwitchState(idleState);
    }

    private void SwitchToMoveState()
    {
        if (moveState == null)
        {
            moveState = new PlayerMoveState(stateMachine);
        }

        stateMachine.SwitchState(moveState);
    }

    private void SwitchToJumpState()
    {
        if (jumpState == null)
        {
            jumpState = new PlayerJumpState(stateMachine);
        }

        stateMachine.SwitchState(jumpState);
    }

    private void SwitchToFallState()
    {
        if (fallState == null)
        {
            fallState = new PlayerFallState(stateMachine);
        }

        stateMachine.SwitchState(fallState);
    }

    private void SwitchToCrouchState()
    {
        if (crouchState == null)
        {
            crouchState = new PlayerCrouchState(stateMachine);
        }

        stateMachine.SwitchState(crouchState);
    }

    private void SwitchToSlideState()
    {
        if (slideState == null)
        {
            slideState = new PlayerSlideState(stateMachine);
        }

        stateMachine.SwitchState(slideState);
    }

    private void SwitchToDashState()
    {
        if (dashState == null)
        {
            dashState = new PlayerDashState(stateMachine);
        }

        stateMachine.SwitchState(slideState);
    }

    private void SwitchToGrappleState()
    {
        if (grappleState == null)
        {
            grappleState = new PlayerGrappleState(stateMachine);
        }

        stateMachine.SwitchState(grappleState);
    }

    #endregion
}
