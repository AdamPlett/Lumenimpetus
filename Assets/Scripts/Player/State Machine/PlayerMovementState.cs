using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState : State
{
    protected readonly PlayerMovementStateMachine stateMachine;

    protected InputController input;

    protected PlayerMovement movement;
    protected Jump jump;

    protected PlayerMovementState(PlayerMovementStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        input = stateMachine.controller.input;

        movement = stateMachine.movement;
        jump = stateMachine.jump;
    }
}
