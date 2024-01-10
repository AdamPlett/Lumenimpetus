using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1MeleeState : Boss1BaseState
{
    public Boss1MeleeState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    float attackTimer = 0f;

    public override void Enter()
    {
        SelectMeleeAttack();
    }

    public override void Tick()
    {
        Invoke("RevertToMoveState", 1f);
    }

    public override void Exit()
    {

    }

    private void RevertToMoveState()
    {
        stateMachine.SwitchState(new Boss1MoveState(stateMachine));
    }

    private void SelectMeleeAttack()
    {
        int randomInt = Random.Range(0, 3);

        if(randomInt == 0)
        {
            stateMachine.animator.SwitchAnimation(stateMachine.animator.MeleeHash1);
        }
        else if (randomInt == 1)
        {
            stateMachine.animator.SwitchAnimation(stateMachine.animator.MeleeHash2);
        }
        else if (randomInt == 2)
        {
            stateMachine.animator.SwitchAnimation(stateMachine.animator.MeleeHash3);
        }
    }
}
