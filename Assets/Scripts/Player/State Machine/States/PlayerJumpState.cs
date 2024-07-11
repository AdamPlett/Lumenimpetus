using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerMovementState
{
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerJumpState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }

    public JumpStats jumpStats;
    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        Debug.Log("Entering jump state");
        input.jumpCancelled += stateMachine.SwitchToFallState;
        jump.JumpAction();
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {

    }
        /*
        Call in update
        jumpBufferTimer = jumpStats.jumpBuffer;
        When grounded equals true
        coyoteTimeTimer = jumpStats.coyoteTime;
        When you start the jump action
        timeBetweenJumps = jumpStats.jumpInputDelay;
        */
    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        input.jumpCancelled -= stateMachine.SwitchToFallState;
        Debug.Log("Exiting jump state");
    }
}
