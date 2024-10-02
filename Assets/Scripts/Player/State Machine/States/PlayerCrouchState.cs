using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerMovementState
{
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerCrouchState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }


    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        Debug.Log("Entering 'Crouch State'");

        stateMachine.crouch.Crouch();

        // Subscribe Listeners
        input.crouchPerformed += stateMachine.SwitchToMoveState;
        input.jumpPerformed += stateMachine.SwitchToJumpState;
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {

    }

    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        Debug.Log("Exiting 'Crouch State'");

        stateMachine.crouch.Uncrouch();

        // Unsubscribe Listeners
        input.crouchPerformed -= stateMachine.SwitchToMoveState;
        input.jumpPerformed -= stateMachine.SwitchToJumpState;
    }
}
