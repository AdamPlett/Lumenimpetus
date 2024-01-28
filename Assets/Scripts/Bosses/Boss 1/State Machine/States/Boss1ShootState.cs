using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss1ShootState : Boss1BaseState
{
    public Boss1ShootState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private GameObject bulletRef;

    private float stateTimer = 0f;

    public override void Enter()
    {
        stateMachine.anim.SwitchAnimation(stateMachine.anim.ShootHash);

        stateMachine.isShooting = true;

        gm.boss1.activeState = eB1.shooting;
    }

    public override void Tick()
    {
        stateTimer += Time.deltaTime;

        stateMachine.LookAtPlayer();

        if(stateTimer > 3f)
        {
            stateMachine.SwitchToMoveState();
        }
    }

    public override void Exit()
    {
        stateMachine.isShooting = false;
        stateMachine.weapons.cannonTimer = 0f;
    }
}
