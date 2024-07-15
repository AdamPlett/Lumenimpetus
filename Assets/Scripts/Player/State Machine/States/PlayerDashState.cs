using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerDashState : PlayerMovementState
{    
    // Sets the state machine for this state, ensuring it is the same as the base state
    public PlayerDashState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }


    // Called once at the start of the state, after ending the previous state
    public override void Enter()
    {
        Debug.Log("Entering Dash State");

        dash.DashAction();
        dash.StartCooldown();
    }

    // Called continously throughout the state (update)
    public override void Tick()
    {
        if(!dash.dashing)
        {
            stateMachine.SwitchToMoveState();
        }
    }

    // Called once at the end of the state, before starting the next state
    public override void Exit()
    {
        Debug.Log("Exiting Dash State");

        dash.ResetDashTimer();
    }
}
