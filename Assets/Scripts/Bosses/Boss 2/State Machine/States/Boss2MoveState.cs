using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GameManager;

public class Boss2MoveState : Boss2BaseState
{
    public Boss2MoveState(Boss2StateMachine stateMachine) : base(stateMachine) { }

    private Transform bossTransform, modelTransform;

    private eDir moveDir;

    private bool animationSet = false;

    public override void Enter()
    {
        bossTransform = stateMachine.transform;
        modelTransform = stateMachine.bossModel.transform;

        SetDirection();

        gm.boss2.activeState = eB2.moving;
        stateMachine.freeze = false;
    }

    public override void Tick()
    {
        if(!stateMachine.freeze)
        {
            if (animationSet)
            {
                // Move or somethin
            }
            else
            {
                SetDirection();
            }
        }
    }

    public override void Exit()
    {

    }

    private void SetDirection()
    {
        eDir prevDir = moveDir;

        int randomInt = Random.Range(0, 3);

        if (randomInt == 0)
        {
            moveDir = eDir.left;
        }
        else if (randomInt == 1)
        {
            moveDir = eDir.right;
        }
        else if (randomInt == 2)
        {
            moveDir = eDir.forward;
        }

        if(moveDir != prevDir)
        {
            SetAnimation();
                        stateMachine.moveDirection = moveDir;
        }
        else
        {
            SetDirection();
        }
    }

    private void SetAnimation()
    {
        switch (moveDir)
        {
            case eDir.forward:
                animationSet = true;
                stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHashF);
                break;

            case eDir.backward:
                animationSet = true;
                stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHashB);
                break;

            case eDir.right:
                animationSet = true;
                stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHashR);
                break;

            case eDir.left:
                animationSet = true;
                stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHashL);
                break;
        }
    }

    public void MoveBoss()
    {
        Vector3 moveIncrement = Vector3.zero;
        
        switch(moveDir)
        {
            case eDir.forward:
                moveIncrement = stateMachine.transform.forward * stateMachine.moveSpeed * Time.deltaTime;
                break;

            case eDir.backward:
                moveIncrement = stateMachine.transform.forward * stateMachine.moveSpeed * Time.deltaTime * -1f;
                break;

            case eDir.right:
                //moveIncrement = stateMachine.transform.right * stateMachine.moveSpeed * Time.deltaTime;
                moveIncrement = (stateMachine.transform.right * stateMachine.moveSpeed * Time.deltaTime) + (stateMachine.transform.forward * stateMachine.moveSpeed * Time.deltaTime);
                break;

            case eDir.left:
                //moveIncrement = stateMachine.transform.right * stateMachine.moveSpeed * Time.deltaTime * -1f;
                moveIncrement = (stateMachine.transform.right * stateMachine.moveSpeed * Time.deltaTime * -1f) + (stateMachine.transform.forward * stateMachine.moveSpeed * Time.deltaTime);
                break;
        }
    }
}
