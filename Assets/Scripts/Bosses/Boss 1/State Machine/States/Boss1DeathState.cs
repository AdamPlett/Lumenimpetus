using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1DeathState : Boss1BaseState
{
    public Boss1DeathState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.activeState = eB1.dead;

        stateMachine.TriggerDeathSequence();
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {

    }

}
