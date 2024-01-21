using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SlamState : Boss1BaseState
{
    public Boss1SlamState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private bool animationSet;

    private Vector3 playerPos;

    public override void Enter()
    {
        stateMachine.isGrappling = true;
        stateMachine.weapons.canGrapple = false;

        Debug.Log("Entering Slam State");

        stateMachine.anim.SwitchAnimation(stateMachine.anim.Slam1Hash);
    }

    public override void Tick()
    {
        stateMachine.LookAtPlayer();

        if (stateMachine.weapons.noHit)
        {
            stateMachine.SwitchToMoveState();
        }
        else
        {
            if (stateMachine.weapons.pulling)
            {
                if (animationSet)
                {
                    playerPos = stateMachine.playerRef.transform.position;

                    if (stateMachine.playerRef.GetComponent<PlayerMovement>().grounded)
                    {
                        stateMachine.SwitchToMoveState();
                    }
                    else
                    {
                        playerPos.y -= Time.deltaTime * stateMachine.weapons.slamSpeed * 10f;

                        stateMachine.weapons.lineRender.SetPosition(0, stateMachine.weapons.lineRender.transform.position);
                        stateMachine.weapons.lineRender.SetPosition(1, stateMachine.playerRef.transform.position);
                    }

                    stateMachine.playerRef.transform.position = playerPos;
                }
                else
                {
                    SetAnimation();
                }
            }
            else
            {
                if (stateMachine.weapons.grappleBullet.activeSelf)
                {
                    stateMachine.weapons.bulletScript.MoveBullet(150f);

                    stateMachine.weapons.lineRender.SetPosition(0, stateMachine.weapons.lineRender.transform.position);
                    stateMachine.weapons.lineRender.SetPosition(1, stateMachine.weapons.grappleBullet.transform.position);
                }
            }
        }

    }

    public override void Exit()
    {
        stateMachine.isGrappling = false;
        stateMachine.weapons.grappleTimer = 0f;

        Debug.Log("Exiting Slam State");
    }

    private void SetAnimation()
    {
        stateMachine.anim.SwitchAnimation(stateMachine.anim.Slam2Hash);
        animationSet = true;
    }
}
