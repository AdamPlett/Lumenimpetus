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
        input.crouchPerformed += stateMachine.SwitchToCrouchState;
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {
        if(input.RetrieveMoveInput().sqrMagnitude > 0)
        {
            stateMachine.movement.MoveOnGround();
        }
        else
        {
            // Is idle - no move
        }
    }

    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        Debug.Log("Exiting 'Move State'");
    }
}
