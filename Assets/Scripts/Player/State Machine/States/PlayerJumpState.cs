using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerMovementState
{
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerJumpState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }

    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        Debug.Log("Entering jump state");

        jump.JumpAction();

        // Subscribe Listeners
        input.jumpCancelled += stateMachine.SwitchToFallState;
        input.dashPerformed += stateMachine.SwitchToDashState;
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {
        if (input.RetrieveMoveInput().sqrMagnitude > 0)
        {
            // Set desired speed
            movement.SetDesiredSpeed(movement.airSpeed);
        }
        else
        {
            // Player is idle - Don't move!
            movement.SetDesiredSpeed(0f);
        }

        // Move player
        stateMachine.airMovement.Move();

        if (stateMachine.rb.velocity.y <= 0)
        {
            stateMachine.SwitchToFallState();
        }
    }
   
    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        Debug.Log("Exiting jump state");

        // Unsubscribe Listeners
        input.jumpCancelled -= stateMachine.SwitchToFallState;
        input.dashPerformed -= stateMachine.SwitchToDashState;
    }
}
