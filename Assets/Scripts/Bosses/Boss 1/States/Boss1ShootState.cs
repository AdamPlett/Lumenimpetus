using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1ShootState : Boss1BaseState
{
    public Boss1ShootState(Boss1StateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        stateMachine.anim.SwitchAnimation(stateMachine.anim.ShootHash);

        stateMachine.isShooting = true;
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        stateMachine.isShooting = false;
        stateMachine.weapons.cannonTimer = 0f;
    }
}
