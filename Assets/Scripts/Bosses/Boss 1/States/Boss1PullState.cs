using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1PullState : Boss1BaseState
{
    public Boss1PullState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.isGrappling = true;
        stateMachine.weapons.canGrapple = false;

        Debug.Log("Entering Pull State");

        stateMachine.weapons.lineRender.enabled = true;
    }

    public override void Tick()
    {
        stateMachine.playerRef.transform.position = Vector3.Lerp(stateMachine.playerRef.transform.position, stateMachine.transform.position, Time.deltaTime);

        stateMachine.weapons.lineRender.SetPosition(0, stateMachine.weapons.lineRender.transform.position);
        stateMachine.weapons.lineRender.SetPosition(1, stateMachine.playerRef.transform.position);

        if (Vector3.Distance(stateMachine.playerRef.transform.position, stateMachine.transform.position) < 3f)
        {
            stateMachine.SwitchToMeleeState();
        }
    }

    public override void Exit()
    {
        stateMachine.isGrappling = false;
        stateMachine.weapons.grappleTimer = 0f;

        Debug.Log("Exiting Pull State");

        stateMachine.weapons.lineRender.enabled = false;
    }
}
