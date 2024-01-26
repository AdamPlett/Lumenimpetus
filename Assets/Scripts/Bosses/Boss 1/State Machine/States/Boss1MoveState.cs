using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static GameManager;

public enum eDir { forward, backward, left, right }

public class Boss1MoveState : Boss1BaseState
{
    public Boss1MoveState(Boss1StateMachine stateMachine) : base(stateMachine) { }

    private Transform bossTransform, modelTransform;

    private eDir moveDir;

    private bool animationSet = false;

    public override void Enter()
    {
        bossTransform = stateMachine.transform;
        modelTransform = stateMachine.bossModel.transform;

        SetDirection();

        gm.boss1.activeState = eB1.moving;
        stateMachine.freeze = false;
    }

    public override void Tick()
    {
        if(!stateMachine.freeze)
        {
            if (animationSet)
            {
                stateMachine.LookAtPlayer();
                stateMachine.CheckForPlayer();

                MoveBoss();

                if (!stateMachine.CheckSeePlayer() && stateMachine.CheckAbovePlayer() && moveDir != eDir.backward)
                {
                    if (Physics.Raycast(stateMachine.transform.position, stateMachine.transform.up * -1f, out RaycastHit hitInfo))
                    {
                        if (hitInfo.transform.gameObject.tag.Equals("StandStill"))
                        {
                            stateMachine.freeze = true;
                        }
                        else
                        {
                            if (stateMachine.CheckSeeFloor(eDir.forward))
                            {
                                stateMachine.transform.position += stateMachine.transform.forward * Time.deltaTime * stateMachine.moveSpeed;
                            }
                            else
                            {
                                stateMachine.transform.position -= stateMachine.transform.forward * Time.deltaTime * stateMachine.moveSpeed;
                            }
                        }
                    }
                }
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
        float distance = stateMachine.GetDistanceToPlayer();

        eDir prevDir = moveDir;

        int randomInt;

        if(distance < 10f || distance > 40f)
        {
            randomInt = Random.Range(0, 3);
        }
        else
        {
            randomInt = Random.Range(0, 2);
        }

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
        }
        else
        {
            SetDirection();
        }
    }

    private void SetAnimation()
    {
        if (stateMachine.weapons.canMelee)
        {
            switch (moveDir)
            {
                case eDir.forward:
                    animationSet = true;
                    stateMachine.anim.SwitchAnimation(stateMachine.anim.WalkHash);
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

        if(stateMachine.CheckSeeFloor(moveDir))
        {
            stateMachine.transform.position += moveIncrement;
        }
        else
        {
            stateMachine.transform.position -= moveIncrement;

            if(moveDir == eDir.right)
            {
                if(stateMachine.CheckSeeFloor(eDir.forward))
                {
                    SetDirection();
                }
                else
                {
                    moveDir = eDir.left;
                }
            }
            else if (moveDir == eDir.left)
            {
                if (stateMachine.CheckSeeFloor(eDir.forward))
                {
                    SetDirection();
                }
                else
                {
                    moveDir = eDir.right;
                }
            }
            else if (moveDir == eDir.forward)
            {
                moveDir = eDir.backward;
            }
            else
            {
                SetDirection();
            }
        }
    }
}
