using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss1BaseState : State
{
    protected readonly Boss1StateMachine stateMachine;

    protected Boss1BaseState(Boss1StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
