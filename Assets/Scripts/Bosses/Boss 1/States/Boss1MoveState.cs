using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum eCircleDir { none, left, right }

public class Boss1MoveState : Boss1BaseState
{
    public Boss1MoveState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private Transform bossTransform, modelTransform;
    private Vector3 playerPosRef;

    private eCircleDir circleDir;

    public override void Enter()
    {
        bossTransform = stateMachine.GetBossTransform();
        modelTransform = stateMachine.bossModel.transform;

        playerPosRef = stateMachine.playerRef.transform.position;

        SetCircleDirection();
    }

    public override void Tick()
    {
        if(stateMachine.health.GetCurrentPhase() == 1)
        {
            CirclePlayer();
        }
        else if (stateMachine.health.GetCurrentPhase() == 2)
        {

        }
        else if (stateMachine.health.GetCurrentPhase() == 3)
        {

        }
    }

    public override void Exit()
    {

    }

    private void SetCircleDirection()
    {
        int randomInt = Random.Range(0, 2);

        if(randomInt == 0)
        {
            circleDir = eCircleDir.left;
            stateMachine.animator.SwitchAnimation(stateMachine.animator.WalkHashL);
        }
        else if(randomInt == 1)
        {
            circleDir = eCircleDir.right;
            stateMachine.animator.SwitchAnimation(stateMachine.animator.WalkHashR);
        }
    }

    private void CirclePlayer()
    {
        // Have boss face player position
        bossTransform.LookAt(stateMachine.playerRef.transform);

        // Adjust rotation to ensure only y is affected (keeping boss level with ground)
        Quaternion rotation = bossTransform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        bossTransform.rotation = rotation;

        if(circleDir == eCircleDir.left)
        {
            bossTransform.position = bossTransform.position - bossTransform.right * stateMachine.moveSpeed * Time.deltaTime;
        }
        else
        {
            bossTransform.position = bossTransform.position + bossTransform.right * stateMachine.moveSpeed * Time.deltaTime;
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
