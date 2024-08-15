using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerMovementState
{
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerMoveState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }


    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        Debug.Log("Entering 'Move State'");
        
        // Subscribe Listeners
        input.jumpPerformed += stateMachine.SwitchToJumpState;
        input.dashPerformed += stateMachine.SwitchToDashState;
        input.crouchPerformed += stateMachine.SwitchToCrouchState;
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {        
        if(input.RetrieveMoveInput().sqrMagnitude > 0)
        {
            if (input.RetrieveSprintInput() > 0)
            {
                // Set desired speed to sprint speed
                movement.SetDesiredSpeed(movement.sprintSpeed);
            }
            else
            {
                // Set desired speed to walk speed
                movement.SetDesiredSpeed(movement.walkSpeed);
            }

            // Move player
            stateMachine.groundMovement.Move();
        }
        else
        {
            // Player is idle - Don't move!
            movement.SetDesiredSpeed(0f);
        }
    }

    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        Debug.Log("Exiting 'Move State'");

        // Unsubscribe Listeners
        input.jumpPerformed -= stateMachine.SwitchToJumpState;
        input.dashPerformed -= stateMachine.SwitchToDashState;
        input.crouchPerformed -= stateMachine.SwitchToCrouchState;
    }
}
