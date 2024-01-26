using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Boss1PullState : Boss1BaseState
{
    public Boss1PullState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private bool animationSet = false;

    private float stateTimer = 0f;

    public override void Enter()
    {
        stateMachine.isGrappling = true;
        stateMachine.weapons.canGrapple = false;

        Debug.Log("Entering Pull State");

        stateMachine.anim.SwitchAnimation(stateMachine.anim.GrappleHash);

        gm.boss1.activeState = eB1.pulling;
    }

    public override void Tick()
    {
        stateTimer += Time.deltaTime;

        if (stateTimer > 5f)
        {
            stateMachine.SwitchToMoveState();
        }

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
                    stateMachine.playerRef.transform.position -= stateMachine.GetDirectionToPlayer() * Time.deltaTime * stateMachine.weapons.pullSpeed;

                    stateMachine.weapons.lineRender.SetPosition(0, stateMachine.weapons.lineRender.transform.position);
                    stateMachine.weapons.lineRender.SetPosition(1, stateMachine.playerRef.transform.position);

                    if (Vector3.Distance(stateMachine.playerRef.transform.position, stateMachine.transform.position) < 3f)
                    {
                        stateMachine.weapons.bulletScript.ResetBullet();
                        stateMachine.SwitchToMeleeState();
                    }
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
                    stateMachine.weapons.bulletScript.MoveBullet(2f);

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

        stateMachine.weapons.DeactivateGrapple();

        stateMachine.weapons.noHit = false;
        gm.pm.freeze = false;
        gm.boss1.weapons.pulling = false;

        Debug.Log("Exiting Pull State");
    }

    private void SetAnimation()
    {
        stateMachine.anim.SwitchAnimation(stateMachine.anim.PullHash);
        animationSet = true;
    }
}
