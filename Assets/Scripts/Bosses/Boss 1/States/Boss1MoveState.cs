using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum eDir { none, left, right }

public class Boss1MoveState : Boss1BaseState
{
    public Boss1MoveState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private Transform bossTransform, modelTransform;

    private eDir circleDir;

    private bool animationSet = false;

    public override void Enter()
    {
        bossTransform = stateMachine.transform;
        modelTransform = stateMachine.bossModel.transform;
    }

    public override void Tick()
    {
        if(animationSet)
        {
            stateMachine.CheckForPlayer();

            if (stateMachine.health.GetCurrentPhase() == 1)
            {
                stateMachine.LookAtPlayer();

                if (stateMachine.CheckFacingPlayer())
                {
                    CirclePlayer();
                }
            }
            else if (stateMachine.health.GetCurrentPhase() == 2)
            {

            }
            else if (stateMachine.health.GetCurrentPhase() == 3)
            {

            }
        }
        else
        {
            SetAnimation();
        }
    }

    public override void Exit()
    {

    }

    private void SetAnimation()
    {
        if (stateMachine.weapons.canMelee)
        {
            int randomInt = Random.Range(0, 2);

            if (randomInt == 0)
            {
                circleDir = eDir.left;
                stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHashL);
                animationSet = true;
            }
            else if (randomInt == 1)
            {
                circleDir = eDir.right;
                stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHashR);
                animationSet = true;
            }
        }
    }

    private void CirclePlayer()
    {
        if (circleDir == eDir.left)
        {
            bossTransform.position = bossTransform.position - bossTransform.right.normalized * stateMachine.moveSpeed * Time.deltaTime;
        }
        else if (circleDir == eDir.right)
        {
            bossTransform.position = bossTransform.position + bossTransform.right.normalized * stateMachine.moveSpeed * Time.deltaTime;
        }
    }

    /*
    private void CirclePoint(Vector3 centerPoint, float circleRadius)
    {
        bossTransform.position = bossTransform.position + bossTransform.forward * stateMachine.moveSpeed * Time.deltaTime;
        
        Vector3 centerVector = (bossTransform.position - centerPoint).normalized;

        bossTransform.position = centerPoint + centerVector * circleRadius;

        if(circleDir == eCircleDir.left)
        {
            bossTransform.forward = new Vector3(centerVector.z, 0f, -centerVector.x);
        }
        else if(circleDir == eCircleDir.right)
        {
            bossTransform.forward = new Vector3(-centerVector.z, 0f, centerVector.x);
        }
    }
    */
}
