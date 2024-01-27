using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1DanceState : Boss1BaseState
{
    public Boss1DanceState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.activeState = eB1.dancing;

        stateMachine.anim.SwitchAnimation(stateMachine.anim.DanceHash);
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {

    }
}
