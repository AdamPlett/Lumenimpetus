using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss2BaseState : State
{
    protected readonly Boss2StateMachine stateMachine;

    protected Boss2BaseState(Boss2StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
