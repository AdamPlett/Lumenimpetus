using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss1GrappleState : Boss1BaseState
{
    public Boss1GrappleState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private Vector3 startPos, targetPos;

    public override void Enter()
    {
        stateMachine.isGrappling = true;
        stateMachine.weapons.canGrapple = false;

        targetPos = stateMachine.weapons.grappleTarget.position;

        Debug.Log("Entering Grapple State");

        stateMachine.anim.SwitchAnimation(stateMachine.anim.SwingHash);

        gm.boss1.activeState = eB1.grappling;
    }

    public override void Tick()
    {
        PivotToPoint();

        if (stateMachine.weapons.swinging)
        {
            stateMachine.transform.position = Vector3.Lerp(stateMachine.transform.position, targetPos, Time.deltaTime * stateMachine.weapons.grappleSpeed);

            stateMachine.weapons.lineRender.SetPosition(0, stateMachine.weapons.lineRender.transform.position);
            stateMachine.weapons.lineRender.SetPosition(1, targetPos);

            if (Vector3.Distance(stateMachine.transform.position, targetPos) <= 15)
            {
                stateMachine.SwitchToMoveState();
            }
        }
    }

    public override void Exit()
    {
        stateMachine.isGrappling = false;
        stateMachine.weapons.grappleTimer = 0f;

        Debug.Log("Exiting Grapple State");

        stateMachine.weapons.EndSwing();
        stateMachine.weapons.DeactivateGrapple();
    }

    private void PivotToPoint()
    {
        Vector3 direction = targetPos - stateMachine.transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        stateMachine.RotateBoss(targetRotation);
    }
}
