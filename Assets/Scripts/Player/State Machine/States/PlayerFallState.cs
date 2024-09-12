using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerMovementState
{
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerFallState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }


    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        Debug.Log("Entering fall state");

        // Subscribe listeners
        input.dashPerformed += stateMachine.SwitchToDashState;
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {
        if (stateMachine.groundCheck.GetGrounded())
        {
            stateMachine.SwitchToMoveState();
        }

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
    }

    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        Debug.Log("Exiting fall state");

        // Unsubscribe listeners
        input.dashPerformed -= stateMachine.SwitchToDashState;
    }
}
