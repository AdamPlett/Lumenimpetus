using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState : State
{
    protected readonly PlayerMovementStateMachine stateMachine;

    // References - Just makes writing code easier
    protected InputController input;
    protected BasicMovement movement;
    protected Jump jump;
    protected Dash dash;

    protected WallDetector wallCheck;
    protected GroundDetector groundCheck;

    protected PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        // Set state machine
        this.stateMachine = stateMachine;

        // Set references to equal state machine
        input = stateMachine.controller.input;
        movement = stateMachine.movement;
        jump = stateMachine.jump;
        dash = stateMachine.dash;

        wallCheck = stateMachine.wallCheck;
        groundCheck = stateMachine.groundCheck;
    }
}
