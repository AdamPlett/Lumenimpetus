using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerMovementState
{
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerJumpState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }

    private float jumpBufferTimer=0;
    private float coyoteTimeTimer=0;
    private float timeBetweenJumps=0;

    public JumpStats jumpStats;
    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        input.jumpCancelled += stateMachine.SwitchToFallState;
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {
        if (stateMachine.movement.GetGrounded())
        {
            stateMachine.SwitchToMoveState();
        }

        jumpBufferTimer -= Time.deltaTime;
        coyoteTimeTimer -= Time.deltaTime;
        timeBetweenJumps -= Time.deltaTime;
    }

    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        jumpBufferTimer = jumpStats.jumpBuffer;
        coyoteTimeTimer = jumpStats.coyoteTime;
        timeBetweenJumps = jumpStats.jumpInputDelay;
    }
}
