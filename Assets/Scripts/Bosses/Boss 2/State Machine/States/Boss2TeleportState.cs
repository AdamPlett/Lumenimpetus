using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GameManager;

public class Boss2TeleportState : Boss2BaseState
{
    public Boss2TeleportState(Boss2StateMachine stateMachine) : base(stateMachine) { }

    private bool animationSet = false;

    public override void Enter()
    {
        gm.boss2.activeState = eB2.teleporting;
    }

    public override void Tick()
    {
        if(!stateMachine.freeze)
        {
            if (animationSet)
            {
                // Teleport or somethin
            }
            else
            {
                SetAnimation();
            }
        }
    }

    public override void Exit()
    {

    }

    private void SetAnimation()
    {
        //stateMachine.anim.SwitchAnimation();
        animationSet = true;
    }
}
