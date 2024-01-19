using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SlamState : Boss1BaseState
{
    public Boss1SlamState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.isGrappling = true;
        stateMachine.weapons.canGrapple = false;

        Debug.Log("Entering Slam State");
    }

    public override void Tick()
    {
        Vector3 playerPos = stateMachine.playerRef.transform.position;

        if (stateMachine.playerRef.GetComponent<PlayerMovement>().grounded)
        {
            stateMachine.SwitchToMoveState();
        }
        else
        {
            playerPos.y -= Time.deltaTime * stateMachine.weapons.slamSpeed;

            stateMachine.weapons.lineRender.SetPosition(0, stateMachine.weapons.lineRender.transform.position);
            stateMachine.weapons.lineRender.SetPosition(1, stateMachine.playerRef.transform.position);
        }

        stateMachine.playerRef.transform.position = playerPos;

    }

    public override void Exit()
    {
        stateMachine.isGrappling = false;
        stateMachine.weapons.grappleTimer = 0f;

        Debug.Log("Exiting Slam State");
    }
}
